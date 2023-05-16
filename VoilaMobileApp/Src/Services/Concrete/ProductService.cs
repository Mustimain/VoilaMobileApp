using System;
using VoilaMobileApp.Src.Database.DbContext;
using VoilaMobileApp.Src.Models;
using VoilaMobileApp.Src.Services.Interfaces;

namespace VoilaMobileApp.Src.Services.Concrete
{
    public class ProductService : IProductService
    {
        public ProductService()
        {
        }

        public async Task<List<Product>> GetAllProducts()
        {
            try
            {
                var client = DbContext.StartFirebaseClient();

                var dataResult = (await client.Child("Products").OnceAsync<Product>()).Select(obj => new Product
                {
                    Id = obj.Key,
                    Name = obj.Object.Name,
                    CategoryId = obj.Object.CategoryId,
                    Description = obj.Object.Description,
                    Price = obj.Object.Price
                }).ToList();



                return dataResult;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        public async Task<List<Product>> GetProductsByCategoryId(string categoryId)
        {
            var allCategory = await GetAllProducts();
            var result = allCategory.Where(prdct => prdct.CategoryId == categoryId).ToList();
            return result;
        }
    }
}

