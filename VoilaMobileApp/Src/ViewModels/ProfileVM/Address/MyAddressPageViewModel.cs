using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using VoilaMobileApp.Src.Models;
using VoilaMobileApp.Src.Base;
using VoilaMobileApp.Src.Views.ProfileViews.MyAddressViews;
using VoilaMobileApp.Src.Views.ProfileViews.MyAddressViews.AddressAddViews;
using VoilaMobileApp.Src.Services.Interfaces;

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

        public MyAddressPageViewModel(INavigationService navigationService, IAddressService addressService) : base(navigationService)
        {
            _addressService = addressService;
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

    }
}

