using RunRun.Api.Models.v1;
using System.Collections.Concurrent;

namespace RunRun.Api.Repositories.v1
{
    public interface IOrderRepository
    {
        Task<Order> Create(Order order);
        Task<Order> Read(Guid orderId);
        Task<Order> Update(Order order);
        Task<Order> Delete(Order order);
    }

    public class OrderRepository : IOrderRepository
    {
        private static readonly ConcurrentDictionary<Guid, Order> _orders = new();

        public Task<Order> Create(Order order)
        {
            if(_orders.TryAdd(order.Id, order))
            {
                return Task.FromResult(order);
            }
            return Task.FromResult(default(Order));
        }

        public Task<Order> Delete(Order order)
        {
            throw new NotImplementedException();
        }

        public Task<Order> Read(Guid orderId)
        {
            _ = _orders.TryGetValue(orderId, out var order);
            return Task.FromResult<Order>(order);
        }

        public Task<Order> Update(Order order)
        {
            throw new NotImplementedException();
        }
    }
}
