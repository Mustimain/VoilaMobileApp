using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Microsoft.Maui.Controls.Compatibility.Platform.Android;
using Prism.Navigation;
using VoilaMobileApp.Src.Base;
using VoilaMobileApp.Src.Models;
using VoilaMobileApp.Src.Models.Enums;
using VoilaMobileApp.Src.Services.Interfaces;
using VoilaMobileApp.Src.Utilts.Results;
using VoilaMobileApp.Src.Views;

namespace VoilaMobileApp.Src.ViewModels
{
    public class RegisterPageViewModel : BaseViewModel, IPageLifecycleAware
    {
        private readonly ICustomerService _customerService;
        private readonly IPageDialogService _pageDialogService;
        public RegisterPageViewModel(INavigationService navigationService, ICustomerService customerService, IPageDialogService pageDialogService) : base(navigationService)
        {
            _customerService = customerService;
            _pageDialogService = pageDialogService;
        }

        #region LifecycleMethods

        public void OnAppearing()
        {
            GenderList.Add(GenderType.Erkek);
            GenderList.Add(GenderType.Kadın);
            GenderList.Add(GenderType.Diğer);
        }

        #endregion

        #region Properties

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

        private string _password;
        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value; RaisePropertyChanged();
            }
        }

        private string _rePassword;
        public string RePassword
        {
            get
            {
                return _rePassword;
            }
            set
            {
                _rePassword = value; RaisePropertyChanged();
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

        private GenderType _gender = GenderType.Diğer;
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

        #region Commands


        public ICommand BackCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await _navigationService.GoBackAsync();

                });
            }
        }


        public ICommand RegisterCommand
        {
            get
            {
                return new Command(async () =>
                {
                    var newCustomer = new Customer
                    {
                        Id = Guid.NewGuid().ToString(),
                        FirstName = FirstName,
                        LastName = LastName,
                        Email = Email,
                        Password = Password,
                        BirthDate = BirthDate,
                        Gender = Gender,
                        PhoneNumber = PhoneNumber,
                        RegisterDate = DateTime.Now,
                        EmailVerification = false,
                        PhoneVerification = false
                    };


                    var res = CustomerValidation(newCustomer);

                    if (res.Status == true)
                    {
                        var checkMail = await _customerService.CheckEmailAsync(Email);

                        if (!checkMail)
                        {
                            var result = await _customerService.RegisterCustomerAsync(newCustomer);

                            if (result)
                            {
                                await _pageDialogService.DisplayAlertAsync("Başarılı", "Başarıyla kayıt oldunuz", "tamam");
                                await _navigationService.NavigateAsync(nameof(LoginPage));
                            }
                            else
                            {
                                await _pageDialogService.DisplayAlertAsync("Başarısız", "Sistemsel bir hata oluştu daha sonra tekrar deneyiniz.", "tamam");

                            }
                        }
                        else
                        {
                            await _pageDialogService.DisplayAlertAsync("Başarısız", "Email adresi mevcut, başka mail adresi ile tekrar deneyiniz", "tamam");

                        }

                    }
                    else
                    {
                        await _pageDialogService.DisplayAlertAsync("Başarısız", res.Message, "tamam");

                    }


                });
            }
        }

        #endregion

        #region Methods

        private Result CustomerValidation(Customer customer)
        {
            if (string.IsNullOrEmpty(customer.FirstName) ||
                string.IsNullOrEmpty(customer.LastName) ||
                string.IsNullOrEmpty(customer.Email) ||
                string.IsNullOrEmpty(customer.Password) ||
                string.IsNullOrEmpty(RePassword) ||
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

            if (customer.Password != RePassword)
            {
                return new Result(false, "Şifreler uyuşmuyor. Kontrol ettikten sonra tekrar Deneyin");

            }


            return new Result(true);
        }

        #endregion
    }
}

