using System;
using System.Windows.Input;
using CommunityToolkit.Maui.Alerts;
using VoilaMobileApp.Src.Base;
using VoilaMobileApp.Src.Services.Interfaces;
using VoilaMobileApp.Src.Utilts.Results;

namespace VoilaMobileApp.Src.ViewModels.ProfileVM.Address
{
    public class AddressAddPageViewModel : BaseViewModel
    {
        private readonly IAddressService _addressService;

        public AddressAddPageViewModel(INavigationService navigationService, IAddressService addressService) : base(navigationService)
        {
            _addressService = addressService;
        }

        private string _addressTitle;
        public string AddressTitle
        {
            get
            {
                return _addressTitle;
            }
            set
            {
                _addressTitle = value; RaisePropertyChanged();
            }
        }
        private string _city;
        public string City
        {
            get
            {
                return _city;
            }
            set
            {
                _city = value; RaisePropertyChanged();
            }
        }
        private string _district;
        public string District
        {
            get
            {
                return _district;
            }
            set
            {
                _district = value; RaisePropertyChanged();
            }
        }

        private string _openAddress;
        public string OpenAddress
        {
            get
            {
                return _openAddress;
            }
            set
            {
                _openAddress = value; RaisePropertyChanged();
            }
        }

        public ICommand AddAddressCommand
        {
            get
            {
                return new Command(async () =>
                {
                    var newAddress = new Models.Address
                    {
                        Id = Guid.NewGuid().ToString(),
                        AddressTitle = AddressTitle,
                        City = City,
                        CustomerId = Utilts.CustomerInfo.CurrentCustomer.Id,
                        District = District,
                        OpenAddress = OpenAddress

                    };

                    var res = AddressValidate(newAddress);

                    if (res.Status)
                    {
                        var result = await _addressService.AddAddressAsync(newAddress);

                        if (result)
                        {
                            await Toast.Make("Kayıt başarıyla gerçekleşti").Show();
                        }
                        else
                        {
                            await Toast.Make("Sistemsel bir hata oluştu tekrar deneyin.").Show();

                        }
                    }
                    else
                    {
                        await Toast.Make(res.Message).Show();

                    }


                });
            }
        }

        private Result AddressValidate(Models.Address address)
        {
            if (string.IsNullOrEmpty(address.AddressTitle)
                || string.IsNullOrEmpty(address.City)
                || string.IsNullOrEmpty(address.District)
                || string.IsNullOrEmpty(address.OpenAddress)

                )
            {
                return new Result(false, "Bilgiler boş bırakılamaz.");

            }
            if (address.AddressTitle.Length <= 3
                || address.City.Length <= 3
                || address.District.Length <= 3
                || address.OpenAddress.Length <= 3
                )
            {
                return new Result(false, "İlgili alanlar minimum 3 karakter olmalıdır.");

            }

            if (address.AddressTitle.Length >= 400
                || address.City.Length >= 400
                || address.District.Length >= 400
                || address.OpenAddress.Length >= 400
                )
            {
                return new Result(false, "İlgili alanlar maximum 400 karakter olmalıdır.");

            }

            return new Result(true);
        }
    }
}

