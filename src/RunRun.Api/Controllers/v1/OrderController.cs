using Microsoft.AspNetCore.Mvc;
using RunRun.Api.Models.v1;
using RunRun.Api.Services.v1;

namespace RunRun.Api.Controllers.v1
{
    [ApiController]
    [Route("v1/order")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [Route("{orderId}")]
        [HttpGet]        
        public async Task<ActionResult<Order>> Get([FromRoute] Guid orderId)
        {
            return Ok(await _orderService.GetOrder(orderId));
        }

        [Route("{orderId}")]
        [HttpPost]
        public async Task<ActionResult<Order>> Post([FromRoute] Guid orderId)
        {
            return Created(orderId.ToString(), await _orderService.SaveOrder(new Order { Id = orderId }));
        }
    }
}
