using System;
using System.Linq;
using System.Text.Json;
using Firebase.Database;
using Firebase.Database.Query;
using Google.Cloud.Firestore;
using Newtonsoft.Json;
using VoilaMobileApp.Src.Database.DbContext;
using VoilaMobileApp.Src.Models;
using VoilaMobileApp.Src.Services.Interfaces;
using static Android.Graphics.ImageDecoder;

namespace VoilaMobileApp.Src.Services.Concrete
{
    public class CategoryService : ICategoryService
    {


        public CategoryService()
        {
        }

        public async Task<List<Category>> GetAllCategories()
        {
            try
            {
                var client = DbContext.StartFirebaseClient();

                var dataResult = (await client.Child("Categories").OnceAsync<Category>()).Select(obj => new Category
                {
                    Id = obj.Object.Id,
                    Name = obj.Object.Name,
                    CategoryType = obj.Object.CategoryType
                }).ToList();



                return dataResult;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }

        }

        public async Task<List<Category>> GetAllDrinkCategories()
        {
            var data = await GetAllCategories();
            var result = data.Where(ctg => ctg.CategoryType == Models.Enums.CategoryType.İçecek).ToList();
            return result;
        }

        public async Task<List<Category>> GetAllFoodCategories()
        {
            var data = await GetAllCategories();
            var result = data.Where(ctg => ctg.CategoryType == Models.Enums.CategoryType.Yiyecek).ToList();
            return result;
        }
    }
}

