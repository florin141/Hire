using System;
using System.Threading.Tasks;
using Hire.Services.Models;

namespace Hire.Services.Orders
{
    public interface IOrderService
    {
        Task<Order> GetOrderByIdAsync(int orderId);

        Task<PagedResults<Order>> GetAllOrdersAsync(PagingOptions pagingOptions);

        Task<PagedResults<Order>> GetOrdersAsync(int userId, PagingOptions pagingOptions);

        Task<int> BeginOrderAsync(int userId);

        Task<int> LeaseAsync(int orderId, int rentalId, DateTimeOffset start, DateTimeOffset end);
        
        Task<int> ReleaseAsync(int orderId, int rentalId, ReleaseVehicleForm releaseForm);

        Task<int> CompleteOrderAsync(int orderId);
    }
}
