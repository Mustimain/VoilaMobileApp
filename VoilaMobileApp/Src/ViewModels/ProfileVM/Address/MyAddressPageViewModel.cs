using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using VoilaMobileApp.Src.Models;
using VoilaMobileApp.Src.Base;
using VoilaMobileApp.Src.Views.ProfileViews.MyAddressViews;
using VoilaMobileApp.Src.Views.ProfileViews.MyAddressViews.AddressAddViews;
using VoilaMobileApp.Src.Services.Interfaces;
using CommunityToolkit.Maui.Alerts;

namespace VoilaMobileApp.Src.ViewModels.ProfileVM.Address
{
    public class MyAddressPageViewModel : BaseViewModel, IPageLifecycleAware
    {
        private ObservableCollection<Models.Address> _addressList = new ObservableCollection<Models.Address>();
        public ObservableCollection<Models.Address> AddressList
        {
            get
            {
                return _addressList;
            }
            set
            {
                _addressList = value; RaisePropertyChanged();
            }
        }

        private readonly IAddressService _addressService;
        private readonly IPageDialogService _pageDialogService;
        public MyAddressPageViewModel(INavigationService navigationService, IAddressService addressService, IPageDialogService pageDialogService) : base(navigationService)
        {
            _addressService = addressService;
            _pageDialogService = pageDialogService;
        }

        public async void OnAppearing()
        {
            AddressList.Clear();
            var result = await _addressService.GetAllAddressAsync();

            if (result != null)
            {
                result.ForEach(ad => AddressList.Add(ad));
            }
        }


        public ICommand AddressAddCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await _navigationService.NavigateAsync(nameof(AddressAddPage));
                });
            }
        }

        public ICommand UpdateAddressCommand
        {

            get
            {
                return new Command(async (object swipeAddress) =>
                {
                    var updateAddress = swipeAddress as Models.Address;
                    var navParam = new NavigationParameters
                    {
                        {"UpdateAddress",updateAddress }
                    };
                    await _navigationService.NavigateAsync(nameof(AddressAddPage), navParam);
                });
            }
        }

        public ICommand DeleteAddressCommand
        {

            get
            {
                return new Command(async (object swipeAddress) =>
                {
                    var deleteAddress = swipeAddress as Models.Address;
                    var result = await _pageDialogService.DisplayAlertAsync("Bilgi", "Seçilen adresi silmek istediğine emin misin?", "Evet", "Hayır");

                    if (result)
                    {
                        var serviceRes = await _addressService.DeleteAddressAsync(deleteAddress.Id);
                        if (serviceRes)
                        {
                            await Toast.Make("Adres başarıyla silindi").Show();
                            RefreshAddress();
                        }
                        else
                        {
                            await Toast.Make("Adres silinemedi.").Show();

                        }
                    }
                });

            }
        }

        private async void RefreshAddress()
        {
            AddressList.Clear();
            var result = await _addressService.GetAllAddressAsync();

            if (result != null)
            {
                result.ForEach(ad => AddressList.Add(ad));
            }
        }

    }
}

