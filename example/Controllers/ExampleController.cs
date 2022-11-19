using Microsoft.AspNetCore.Mvc;

namespace DotEnv.Core.Extensions.Microsoft.DependencyInjection.Example.Controllers;
[ApiController]
[Route("[controller]")]
public class ExampleController : ControllerBase
{
    private readonly AppSettings _settings;

    public ExampleController(AppSettings settings)
    {
        _settings = settings;
    }

    [HttpGet]
    public string? Get()
        => _settings.Summaries;
}
