using System;
using Firebase.Database;
using Firebase.Database.Query;
using Newtonsoft.Json;
using VoilaMobileApp.Src.Database.DbContext;
using VoilaMobileApp.Src.Models;
using VoilaMobileApp.Src.Services.Interfaces;
using static Google.Cloud.Firestore.V1.StructuredQuery.Types;

namespace VoilaMobileApp.Src.Services.Concrete
{
    public class OrderClaimService : IOrderClaimService
    {
        private readonly FirebaseClient client;
        private readonly IProductService _productService;
        public OrderClaimService(IProductService productService)
        {
            client = DbContext.StartFirebaseClient();
            _productService = productService;
        }

        public async Task<bool> AddOrderClaim(OrderClaim orderClaim)
        {
            try
            {
                await client.Child("OrderClaims").PostAsync(JsonConvert.SerializeObject(orderClaim));
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<List<OrderClaim>> GetOrderClaimsByOrderId(string orderId)
        {
            try
            {
                var result = (await client.Child("OrderClaims")
                    .OnceAsync<OrderClaim>()).Select(clm => new OrderClaim
                    {
                        Id = clm.Object.Id,
                        NumberOfProduct = clm.Object.NumberOfProduct,
                        OrderId = clm.Object.OrderId,
                        ProductId = clm.Object.ProductId

                    }).Where(ordClm => ordClm.OrderId == orderId).ToList();


                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<BasketProductModel>> GetOrderClaimDetail(string orderId)
        {
            try
            {
                var allOrderClaim = await GetOrderClaimsByOrderId(orderId);
                var allProducts = await _productService.GetAllProducts();
                var basketProductList = new List<BasketProductModel>();
                foreach (var orderClaim in allOrderClaim)
                {
                    foreach (var product in allProducts)
                    {
                        if (orderClaim.ProductId == product.Id)
                        {
                            var basketModel = new BasketProductModel
                            {
                                Product = product,
                                ProductCount = orderClaim.NumberOfProduct,
                            };
                            basketProductList.Add(basketModel);
                        }
                    }
                }

                return basketProductList;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}

