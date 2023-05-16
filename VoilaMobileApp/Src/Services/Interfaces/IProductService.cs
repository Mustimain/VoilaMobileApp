using System;
using VoilaMobileApp.Src.Models;

namespace VoilaMobileApp.Src.Services.Interfaces
{
    public interface IProductService
    {
        Task<List<Product>> GetAllProducts();
        Task<List<Product>> GetProductsByCategoryId(string categoryId);


    }
}

