using Microsoft.AspNetCore.Mvc;
using RunRun.Api.Models.RequestModels.V1;
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

        [Route("{orderId}", Name = "GetOrder")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Order))]
        public async Task<ActionResult<Order>> GetOrder([FromRoute] Guid orderId)
        {
            return Ok(await _orderService.GetOrder(orderId));
        }

        [HttpPost]
        [Route("", Name = "CreateOrder")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Order))]
        public async Task<ActionResult<Order>> CreateOrder([FromBody] OrderRequest orderRequest)
        {
            var order = await _orderService.CreateOrder(orderRequest);
            return Created(order.Id.ToString(), order);
        }
    }
}
