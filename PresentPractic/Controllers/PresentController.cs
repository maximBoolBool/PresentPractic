using Microsoft.AspNetCore.Mvc;
using PresentPractic.Services;

namespace PresentPractic.Controllers;


[Route("[controller]/[action]")]
public class PresentController : Controller
{
    private IPresentService service;

    public PresentController(IPresentService _service)
    {
        service = _service;
    }

    [HttpGet]
    public async Task<IActionResult> ChangePresentStatus(string? presentId)
    {
        return Json( await service.ChangePresentStatusAsync(presentId));
    }
    
    [HttpPost]
    public async Task<IActionResult> AddNewPresent(string? userId,string? presentName,string? presentDescription)
    {
        var reseponse = await service.AddNewPresentAsync(userId,presentName,presentDescription);

        return (reseponse is not null) ? Json(reseponse) : BadRequest();
    }
    
}