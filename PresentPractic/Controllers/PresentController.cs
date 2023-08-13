using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PresentPractic.Services;

namespace PresentPractic.Controllers;

[Route("[controller]/[action]")]
public class PresentController : Controller
{
    private IPresentService service;

    private IMapper mapperService;
    
    public PresentController(IPresentService _service , IMapper _mapperService )
    {
        mapperService = _mapperService;
        service = _service;
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> ChangePresentStatus(string? presentId)
    {
        return Json( await service.ChangePresentStatusAsync(presentId));
    }
    
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> AddNewPresent(string? userLogin,string? presentName,string? presentDescription)
    {
        var reseponse = await service.AddNewPresentAsync(userLogin,presentName,presentDescription);

        return (reseponse is not null) ? Json(reseponse) : BadRequest();
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> DeletePresent(string? presentId)
    {
        var response = await service.DeletePresentAsync(presentId);

        return (response) ? Ok() : BadRequest();
    }
    
}