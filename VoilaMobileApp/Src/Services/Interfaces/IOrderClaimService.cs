using System;
using VoilaMobileApp.Src.Models;

namespace VoilaMobileApp.Src.Services.Interfaces
{
    public interface IOrderClaimService
    {
        Task<bool> AddOrderClaim(OrderClaim orderClaim);
        Task<List<OrderClaim>> GetOrderClaimsByOrderId(string orderId);
        Task<List<BasketProductModel>> GetOrderClaimDetail(string orderId);

    }
}

