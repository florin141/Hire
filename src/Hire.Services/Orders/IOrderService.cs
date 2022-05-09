using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Hire.Services.Models;

namespace Hire.Services.Orders
{
    public interface IOrderService
    {
        Task<Order> GetOrderByIdAsync(int orderId);

        Task<ICollection<Order>> GetOrdersAsync(int userId);

        Task<int> BeginOrderAsync(int userId);

        Task<int> LeaseAsync(int orderId, int rentalId, DateTimeOffset start, DateTimeOffset end);
        
        Task<int> ReleaseAsync(int orderId, int rentalId);

        Task<int> CompleteOrderAsync(int userId);
    }
}
