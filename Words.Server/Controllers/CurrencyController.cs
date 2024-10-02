using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Words.Server.Models;
using Words.Server.Services;

namespace Words.Server.Controllers;

[EnableCors("LocalPolicy")]
[ApiController]
[Route("[controller]")]
public class CurrencyController(ICurrencyService currencyService) : ControllerBase
{
    [HttpGet]
    public ActionResult<CurrencyResponse> GetWords([Required] double number)
    {
        var result = currencyService.GetWords(number);
        return Ok(new CurrencyResponse
        {
            Words = result
        });
    }
}
