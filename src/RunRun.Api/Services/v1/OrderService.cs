using RunRun.Api.Models.v1;
using RunRun.Api.Repositories.v1;

namespace RunRun.Api.Services.v1
{
    public interface IOrderService
    {
        Task<Order> GetOrder(Guid id);
        Task<Order> SaveOrder(Order order);
    }

    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<Order> GetOrder(Guid id)
        {
            return await _orderRepository.Read(id);
        }

        public async Task<Order> SaveOrder(Order order)
        {
            return await _orderRepository.Create(order);
        }
    }
}