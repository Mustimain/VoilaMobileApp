using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Android.Webkit;
using VoilaMobileApp.Src.Base;
using VoilaMobileApp.Src.Models;
using VoilaMobileApp.Src.Services.Interfaces;

namespace VoilaMobileApp.Src.ViewModels.MenuVM
{
    public class ProductsPageViewModel : BaseViewModel, IPageLifecycleAware, INavigatedAware, IInitialize
    {
        private ObservableCollection<Product> _productsList = new ObservableCollection<Product>();
        public ObservableCollection<Product> ProductsList { get { return _productsList; } set { _productsList = value; RaisePropertyChanged(); } }

        private readonly IProductService _productService;
        public ProductsPageViewModel(INavigationService navigationService, IProductService productService) : base(navigationService)
        {
            _productService = productService;
        }


        public async void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters.ContainsKey("SelectionCategory"))
            {
                var category = parameters.GetValue<Category>("SelectionCategory");
                var result = await _productService.GetProductsByCategoryId(category.Id);
                result.ForEach(prdct => ProductsList.Add(prdct));
            }
        }


        public ICommand ProductAddToBasketCommand
        {
            get
            {
                return new Command(async (object Item) =>
                {
                    var prdct = Item as Product;
                    if (prdct != null)
                    {
                        if (Utilts.BasketList.GlobalBasketList.Any(prod => prod.Product.Id == prdct.Id))
                        {
                            Utilts.BasketList.GlobalBasketList.Where(pr => pr.Product.Id == prdct.Id).Select(p => { p.ProductCount = p.ProductCount + 1; return p; }).ToList();
                        }
                        else
                        {
                            Utilts.BasketList.GlobalBasketList.Add(new BasketProductModel
                            {
                                Product = prdct,
                                ProductCount = 1
                            });

                        }



                    }
                });
            }
        }
    }
}

