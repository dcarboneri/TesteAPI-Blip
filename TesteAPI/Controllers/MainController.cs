using TesteAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;
using TesteAPI.Models;

namespace TesteAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MainController : ControllerBase
{
    private readonly IMainService _mainService;

    public MainController(IMainService mainService)
    {
        _mainService = mainService;
    }

    [HttpGet("repositorios")]
    public async Task<IActionResult> GetRepositories()
    {
        try
        {
            List<RepositoryModel> dados = await _mainService.GetOldestRepositoriesAsync("takenet");
            return Ok(dados);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }

    }
}
