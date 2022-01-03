using RunRun.Api.Models.RequestModels.V1;
using RunRun.Api.Models.v1;
using RunRun.Api.Repositories.v1;

namespace RunRun.Api.Services.v1
{
    public interface IOrderService
    {
        Task<Order> GetOrder(Guid id);
        Task<Order> CreateOrder(OrderRequest orderRequest);
    }

    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<Order> CreateOrder(OrderRequest orderRequest)
        {
            var order = new Order
            {
                Id = Guid.NewGuid(),
                Created = DateTime.UtcNow,
                Customer = new Customer
                {
                    Address = orderRequest.Address,
                    City = orderRequest.City,
                    FirstName = orderRequest.FirstName,
                    LastName = orderRequest.LastName,
                    PostalCode = orderRequest.PostalCode
                }
            };
            return await _orderRepository.Create(order);
        }

        public async Task<Order> GetOrder(Guid id)
        {
            return await _orderRepository.Read(id);
        }
    }
}