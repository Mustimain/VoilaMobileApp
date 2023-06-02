using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using VoilaMobileApp.Src.Base;
using VoilaMobileApp.Src.Models;
using VoilaMobileApp.Src.Models.Enums;
using VoilaMobileApp.Src.Services.Interfaces;
using VoilaMobileApp.Src.Utilts.Results;

namespace VoilaMobileApp.Src.ViewModels.ProfileVM.EditProfile
{
    public class EditProfilePageViewModel : BaseViewModel, IPageLifecycleAware, INavigationAware
    {
        private readonly ICustomerService _customerService;
        public EditProfilePageViewModel(INavigationService navigationService, ICustomerService customerService) : base(navigationService)
        {
            _customerService = customerService;
        }

        #region Properties
        private Customer _currentCustomer;
        public Customer CurrentCustomer
        {
            get
            {
                return _currentCustomer;
            }
            set
            {
                _currentCustomer = value; RaisePropertyChanged();
            }
        }


        private string _firstName;
        public string FirstName
        {
            get
            {
                return _firstName;
            }
            set
            {
                _firstName = value; RaisePropertyChanged();
            }
        }

        private string _lastName;
        public string LastName
        {
            get
            {
                return _lastName;
            }
            set
            {
                _lastName = value; RaisePropertyChanged();
            }
        }

        private string _email;
        public string Email
        {
            get
            {
                return _email;
            }
            set
            {
                _email = value; RaisePropertyChanged();
            }
        }

        private string _phoneNumber;
        public string PhoneNumber
        {
            get
            {
                return _phoneNumber;
            }
            set
            {
                _phoneNumber = value; RaisePropertyChanged();
            }
        }

        private DateTime _birthDate = DateTime.Now;
        public DateTime BirthDate
        {
            get
            {
                return _birthDate;
            }
            set
            {
                _birthDate = value; RaisePropertyChanged();
            }
        }

        private GenderType _gender;
        public GenderType Gender
        {
            get
            {
                return _gender;
            }
            set
            {
                _gender = value; RaisePropertyChanged();
            }
        }

        #endregion

        private ObservableCollection<GenderType> _genderList = new ObservableCollection<GenderType>();
        public ObservableCollection<GenderType> GenderList
        {
            get
            {
                return _genderList;
            }
            set
            {
                _genderList = value; RaisePropertyChanged();
            }
        }

        public void OnAppearing()
        {
            GenderList.Add(GenderType.Erkek);
            GenderList.Add(GenderType.Kadın);
            GenderList.Add(GenderType.Diğer);

            if (Utilts.CustomerInfo.CurrentCustomer != null)
            {
                CurrentCustomer = Utilts.CustomerInfo.CurrentCustomer;
                FirstName = CurrentCustomer.FirstName;
                LastName = CurrentCustomer.LastName;
                Email = CurrentCustomer.Email;
                BirthDate = CurrentCustomer.BirthDate;
                Gender = CurrentCustomer.Gender;
                PhoneNumber = CurrentCustomer.PhoneNumber;


            }
        }

        public void OnNavigatedTo(INavigationParameters navigationParameters)
        {

        }

        public ICommand UpdateProfileCommand
        {
            get
            {
                return new Command(async () =>
                {
                    var newCust = new Customer
                    {
                        Id = Utilts.CustomerInfo.CurrentCustomer.Id,
                        FirstName = FirstName,
                        LastName = LastName,
                        PhoneNumber = PhoneNumber,
                        Email = Email,
                        BirthDate = BirthDate,
                        Gender = Gender,
                        EmailVerification = false,
                        Password = Utilts.CustomerInfo.CurrentCustomer.Password,
                        PhoneVerification = false,
                        RegisterDate = Utilts.CustomerInfo.CurrentCustomer.RegisterDate

                    };

                    var validateCustomer = CustomerValidation(newCust);
                    if (validateCustomer.Status)
                    {
                        var result = await _customerService.UpdateCustomerAsync(newCust);

                        if (result)
                        {
                            await Toast.Make("Bilgileriniz başarıyla güncellendi.").Show();
                            var customer = await _customerService.GetCustomerByEmailAsync(CurrentCustomer.Email);
                            Utilts.CustomerInfo.CurrentCustomer = customer;
                        }
                        else
                        {
                            await Toast.Make("Bilgileriniz güncellenemedi.").Show();

                        }
                    }
                    else
                    {
                        await Toast.Make(validateCustomer.Message, ToastDuration.Long).Show();

                    }

                });
            }
        }

        private Result CustomerValidation(Customer customer)
        {
            if (string.IsNullOrEmpty(customer.FirstName) ||
                string.IsNullOrEmpty(customer.LastName) ||
                string.IsNullOrEmpty(customer.Email) ||
                string.IsNullOrEmpty(customer.Password) ||
                string.IsNullOrEmpty(customer.PhoneNumber)
                )
            {
                return new Result(false, "Bilgiler boş bırakılamaz.");

            }

            if (customer.Password.Length < 7 || customer.Password.Length >= 16)
            {
                return new Result(false, "Şifre 7 karakterden küçük 16 karakterden büyük olamaz.");
            }

            if (DateTime.Now.Year - customer.BirthDate.Year < 18)
            {
                return new Result(false, "18 yaşından küçükler kayıt olamaz.");

            }
            if (customer.PhoneNumber.Length != 11)
            {
                return new Result(false, "Lütfen telefon numaranızın başına 0 koyarak tekrar deneyin.");

            }



            return new Result(true);
        }

    }
}

