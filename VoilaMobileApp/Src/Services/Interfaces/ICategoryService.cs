using System;
using VoilaMobileApp.Src.Models;

namespace VoilaMobileApp.Src.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<List<Category>> GetAllCategories();
        Task<List<Category>> GetAllFoodCategories();
        Task<List<Category>> GetAllDrinkCategories();


    }
}

