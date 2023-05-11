using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Android.Webkit;
using VoilaMobileApp.Src.Base;
using VoilaMobileApp.Src.Models;
using VoilaMobileApp.Src.Models.GroupLists;
using VoilaMobileApp.Src.Services.Interfaces;
using VoilaMobileApp.Src.Views.MenuViews;

namespace VoilaMobileApp.Src.ViewModels.MenuVM
{
    public class MenuPageViewModel : BaseViewModel, IPageLifecycleAware, INavigatedAware
    {
        private readonly ICategoryService _categoryService;
        public MenuPageViewModel(INavigationService navigationService, ICategoryService categoryService) : base(navigationService)
        {
            _categoryService = categoryService;
        }


        private ObservableCollection<CategoryGroup> _categories = new ObservableCollection<CategoryGroup>();
        public ObservableCollection<CategoryGroup> Categories { get { return _categories; } set { _categories = value; RaisePropertyChanged(); } }


        private Category _selectionCategory;
        public Category SelectionCategory
        {
            get
            {
                return _selectionCategory;
            }
            set
            {
                _selectionCategory = value; RaisePropertyChanged();
            }
        }

        public async void OnAppearing()
        {
            Categories.Clear();
            var foodCategoryList = await _categoryService.GetAllFoodCategories();
            var drinkCategoryList = await _categoryService.GetAllDrinkCategories();

            Categories.Add(new CategoryGroup("İçecekler", drinkCategoryList));
            Categories.Add(new CategoryGroup("Yiyecekler", foodCategoryList));

        }

        public ICommand ProductDetailCommand
        {
            get
            {
                return new Command(async () =>
                {
                    if (SelectionCategory != null)
                    {
                        var navParam = new NavigationParameters
                        {
                            {"SelectionCategory",SelectionCategory }
                        };
                        await _navigationService.NavigateAsync(nameof(ProductsPage), navParam);

                    }
                });
            }
        }
    }
}

