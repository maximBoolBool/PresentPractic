using Microsoft.AspNetCore.Mvc;
using PresentPractic.Services;

namespace PresentPractic.Controllers;

[Route("[controller]/[action]")]
public class UserController : Controller
{
    private readonly IUserServices services;

    public UserController(IUserServices _services)
    {
        services = _services;
    }
    
    [HttpGet]
    public async Task<IActionResult> RegistrateNewUser(string? newLogin, string? newPassword)
    {
        var response = await services.AddNewUserAsync(newLogin,newPassword);

        return (response)? Json(response) : BadRequest();
    }

    [HttpGet]
    public async Task<IActionResult> Authorize(string? login, string? password)
    {
        var response = await services.AuthorizeAsync(login,password);

        return (response is not null )? Json(response) : BadRequest();
    }
}