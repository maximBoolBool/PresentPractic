using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PresentPractic.Models.DbModels;
using PresentPractic.Models.DTOModels;
using PresentPractic.Services;

namespace PresentPractic.Controllers;

[Route("[controller]/[action]")]
public class UserController : Controller
{
    private readonly IUserServices services;

    private readonly IMapper mapper;

    public UserController(IUserServices _services,IMapper _mapper)
    {
        services = _services;
        mapper = _mapper;
    }
    
    [HttpGet]
    public async Task<IActionResult> RegistrateNewUser(string? newLogin, string? newPassword)
    {
        var response = await services.AddNewUserAsync(newLogin,newPassword);
        
        return (response)? Json(await services.GenerateTokenAsync(newLogin,newPassword)) : BadRequest();
    }

    [HttpGet]
    public async Task<IActionResult> Authorize(string? login, string? password)
    {
        var response = await services.AuthorizeAsync(login,password);

        if (response is null)
            return BadRequest();
        
        var controllerResponse = new
        {
            jwtToken = await services.GenerateTokenAsync(login,password),
            User = mapper.Map<User,DTOUser>(response),
        };
        
        
        return Json(controllerResponse);
    }
    
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> ChangeUserPassword(string? userLogin ,string? userOldPassword,string? userNewPassword)
    {
        var response = await services.ChangeUserPasswordAsync(userLogin,userOldPassword,userNewPassword);

        return Json(response);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> ChangeUserLogin(string? userOldLogin,string? userNewLogin,string? userPassword)
    {
        var response = await services.ChangeUserLoginAsync(userOldLogin,userNewLogin,userPassword);

        return Json(response);
    }
}