using System;
using Android.Locations;
using Firebase.Database;
using Firebase.Database.Query;
using Newtonsoft.Json;
using VoilaMobileApp.Src.Database.DbContext;
using VoilaMobileApp.Src.Models;
using VoilaMobileApp.Src.Services.Interfaces;
using static Google.Cloud.Firestore.V1.StructuredQuery.Types;

namespace VoilaMobileApp.Src.Services.Concrete
{
    public class OrderService : IOrderService
    {

        private readonly FirebaseClient client;
        private readonly IProductService _productService;
        private readonly IOrderClaimService _orderClaimService;
        public OrderService(IProductService productService, IOrderClaimService orderClaimService)
        {
            client = DbContext.StartFirebaseClient();
            _productService = productService;
            _orderClaimService = orderClaimService;
        }

        public async Task<bool> AddOrderAsync(Models.Order order)
        {
            try
            {
                await client.Child("Orders").Child(Utilts.CustomerInfo.CurrentCustomer.Id).PostAsync(JsonConvert.SerializeObject(order));
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public async Task<List<Models.Order>> GetAllOrdersAsync()
        {
            try
            {
                var result = (await client.Child("Orders").Child(Utilts.CustomerInfo.CurrentCustomer.Id)
                    .OnceAsync<Models.Order>()).Select(ord => new Models.Order
                    {
                        Id = ord.Object.Id,
                        OrderStatus = ord.Object.OrderStatus,
                        AddressId = ord.Object.AddressId,
                        CustomerId = ord.Object.CustomerId,
                        GiftCode = ord.Object.GiftCode,
                        OrderDate = ord.Object.OrderDate,
                        TotalPrice = ord.Object.TotalPrice

                    }).Where(order => order.CustomerId == Utilts.CustomerInfo.CurrentCustomer.Id).ToList();


                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<OrderDetail>> GetAllOrdersDetailAsync()
        {
            var allOrders = await GetAllOrdersAsync();
            var allOrderDetList = new List<OrderDetail>();
            foreach (var order in allOrders)
            {
                var allProducts = await _orderClaimService.GetOrderClaimDetail(order.Id);
                var orderDet = new OrderDetail
                {
                    BasketProductModelList = allProducts,
                    Order = order

                };
                allOrderDetList.Add(orderDet);
            }

            return allOrderDetList;
        }
    }
}

