using RunRun.Api.Models.RequestModels.V1;
using RunRun.Api.Models.v1;
using RunRun.Api.Repositories.v1;
using System.Net.Mail;
using System.Text.Json;

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
        private readonly ISmtpClient _smtpClient;

        public OrderService(IOrderRepository orderRepository, ISmtpClient smtpClient)
        {
            _orderRepository = orderRepository;
            _smtpClient = smtpClient;
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
                    PostalCode = orderRequest.PostalCode,
                    Email = orderRequest.Email,
                    Phone = orderRequest.Phone,
                }
            };
            order = await _orderRepository.Create(order);
            await SendEmailConfirmation(order);
            await SendPlacedOrderNotification(order);
            return order;
        }

        private async Task SendPlacedOrderNotification(Order order)
        {
            await _smtpClient.SendAsync(new MailMessage("noreply@runruntarget.com", "edward.dahllof@gmail.com")
            {
                Subject = "RunRun Order",
                Body = JsonSerializer.Serialize(order)
            });
        }

        private async Task SendEmailConfirmation(Order order)
        {
            await _smtpClient.SendAsync(new MailMessage("noreply@runruntarget.com", order.Customer.Email) 
            { 
                Subject = "Orderbekräftelse",
                Body = "tack för din order! Vi kommer skicka din order så for betalningen har kommit in." 
            });
        }

        public async Task<Order> GetOrder(Guid id)
        {
            return await _orderRepository.Read(id);
        }
    }
}