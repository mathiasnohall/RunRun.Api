using Microsoft.AspNetCore.Mvc;

namespace RunRun.Api.Controllers
{
    [ApiController]
    [Route("/")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class StatusController : ControllerBase
    {

        [HttpGet]
        public ActionResult Get()
        {
            return Ok("RunRun API");
        }
    }
}
