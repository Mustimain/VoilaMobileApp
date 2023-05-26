using System;
using VoilaMobileApp.Src.Models;

namespace VoilaMobileApp.Src.Services.Interfaces
{
    public interface IOrderService
    {
        Task<bool> AddOrderAsync(Models.Order order);
        Task<List<Order>> GetAllOrdersAsync();
        Task<List<OrderDetail>> GetAllOrdersDetailAsync();


    }
}

