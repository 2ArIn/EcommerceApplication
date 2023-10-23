using EcommerceApplication.Models;

namespace EcommerceApplication.Data.Services
{
    public interface IOrdersService
    {
        Task StoreOrderAsync(List<ShoppingCartItem> items, string userID, string userEmailAddress);

        Task<List<Order>> GetOrderByUserIdAsync(string userId);
    }
}
