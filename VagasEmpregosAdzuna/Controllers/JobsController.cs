using VagasEmpregosAdzuna.Models;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("jobs")]
public class JobsController : ControllerBase
{
    private readonly IJobProvider _jobProvider;

    public JobsController(IJobProvider jobProvider)
    {
        _jobProvider = jobProvider;
    }

    [HttpGet]
    public async Task<IActionResult> Search(string q)
    {
        var jobs = await _jobProvider.Search(q);
        return Ok(jobs);
    }
}