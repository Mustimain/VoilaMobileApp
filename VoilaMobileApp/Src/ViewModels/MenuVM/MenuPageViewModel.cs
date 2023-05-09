using System;
using System.Collections.ObjectModel;
using Android.Webkit;
using VoilaMobileApp.Src.Base;
using VoilaMobileApp.Src.Models;
using VoilaMobileApp.Src.Models.GroupLists;
using VoilaMobileApp.Src.Services.Interfaces;

namespace VoilaMobileApp.Src.ViewModels.MenuVM
{
    public class MenuPageViewModel : BaseViewModel, IPageLifecycleAware, INavigatedAware
    {
        private ObservableCollection<CategoryGroup> _categories = new ObservableCollection<CategoryGroup>();
        public ObservableCollection<CategoryGroup> Categories { get { return _categories; } set { _categories = value; RaisePropertyChanged(); } }

        private readonly ICategoryService _categoryService;
        public MenuPageViewModel(INavigationService navigationService, ICategoryService categoryService) : base(navigationService)
        {
            _categoryService = categoryService;
        }


        public async void OnAppearing()
        {
            Categories.Clear();
            var foodCategoryList = await _categoryService.GetAllFoodCategories();
            var drinkCategoryList = await _categoryService.GetAllDrinkCategories();

            Categories.Add(new CategoryGroup("İçecekler", drinkCategoryList));
            Categories.Add(new CategoryGroup("Yiyecekler", foodCategoryList));

        }
    }
}

