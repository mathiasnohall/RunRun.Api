using Microsoft.AspNetCore.Mvc;
using RunRun.Api.Models.v1;

namespace RunRun.Api.Controllers.v1
{
    [ApiController]
    [Route("v1/order")]
    public class OrderController : ControllerBase
    {
        [Route("{orderId}")]
        [HttpGet]        
        public ActionResult<Order> Get([FromRoute] Guid orderId)
        {
            return Ok(new Order { Id = orderId });
        }
    }
}
