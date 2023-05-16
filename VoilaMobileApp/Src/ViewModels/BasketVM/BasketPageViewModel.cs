using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Android.Webkit;
using VoilaMobileApp.Src.Base;
using VoilaMobileApp.Src.Models;

namespace VoilaMobileApp.Src.ViewModels.BasketVM
{
    public class BasketPageViewModel : BaseViewModel, INavigationAware, IPageLifecycleAware
    {

        private ObservableCollection<BasketProductModel> _basketList = new ObservableCollection<BasketProductModel>();
        public ObservableCollection<BasketProductModel> BasketList { get { return _basketList; } set { _basketList = value; RaisePropertyChanged(); } }


        private double _totalBasketPrice = 0;
        public double TotalBasketPrice
        {
            get
            {
                return _totalBasketPrice;
            }
            set
            {
                _totalBasketPrice = value; RaisePropertyChanged();
            }
        }

        public BasketPageViewModel(INavigationService navigationService) : base(navigationService)
        {
        }

        public async void OnAppearing()
        {
            if (Utilts.BasketList.GlobalBasketList.Count > 0)
            {
                BasketList.Clear();
                Utilts.BasketList.GlobalBasketList.ForEach(prdct => BasketList.Add(prdct));
                TotalBasketPrice = 0;

                foreach (var basketProd in Utilts.BasketList.GlobalBasketList)
                {
                    TotalBasketPrice = (double)(TotalBasketPrice + (basketProd.ProductCount * basketProd.Product.Price));
                }


            }
        }

        public ICommand AddProductCommand
        {
            get
            {
                return new Command(async (object Item) =>
                {
                    var currProdcut = Item as BasketProductModel;
                    var selectProduct = Utilts.BasketList.GlobalBasketList.Where(pr => pr.Product.Id == currProdcut.Product.Id).FirstOrDefault();

                    if (selectProduct.ProductCount <= 100)
                    {
                        Utilts.BasketList.GlobalBasketList.Where(pr => pr.Product.Id == currProdcut.Product.Id).Select(p => { p.ProductCount = p.ProductCount + 1; return p; }).ToList();
                        RefreshData();
                    }
                    else
                    {
                        return;
                    }

                });
            }
        }

        public ICommand SubProductCommand
        {
            get
            {
                return new Command(async (object Item) =>
                {
                    var currProdcut = Item as BasketProductModel;
                    var selectProduct = Utilts.BasketList.GlobalBasketList.Where(pr => pr.Product.Id == currProdcut.Product.Id).FirstOrDefault();

                    if (selectProduct.ProductCount == 1)
                    {
                        Utilts.BasketList.GlobalBasketList.Remove(selectProduct);
                        RefreshData();
                    }
                    else
                    {
                        Utilts.BasketList.GlobalBasketList.Where(pr => pr.Product.Id == currProdcut.Product.Id).Select(p => { p.ProductCount = p.ProductCount + -1; return p; }).ToList();
                        RefreshData();
                    }

                });
            }
        }

        public ICommand RefreshDataCommand
        {
            get
            {
                return new Command(async () =>
                {
                    RefreshData();

                });
            }
        }


        private void RefreshData()
        {

            BasketList.Clear();
            Utilts.BasketList.GlobalBasketList.ForEach(prdct => BasketList.Add(prdct));
            TotalBasketPrice = 0;
            foreach (var basketProd in Utilts.BasketList.GlobalBasketList)
            {
                TotalBasketPrice = (int)(TotalBasketPrice + (basketProd.ProductCount * basketProd.Product.Price));
            }

        }

    }
}

