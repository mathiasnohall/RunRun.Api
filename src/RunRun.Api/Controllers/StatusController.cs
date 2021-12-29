﻿using Microsoft.AspNetCore.Mvc;

namespace RunRun.Api.Controllers
{
    [ApiController]
    [Route("/")]
    public class StatusController : ControllerBase
    {

        [HttpGet]
        public ActionResult Get()
        {
            return Ok("ok");
        }
    }
}
