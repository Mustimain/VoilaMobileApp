using System;
using System.Windows.Input;
using CommunityToolkit.Maui.Alerts;
using VoilaMobileApp.Src.Base;
using VoilaMobileApp.Src.Services.Interfaces;
using VoilaMobileApp.Src.Utilts.Results;

namespace VoilaMobileApp.Src.ViewModels.ForgotPasswordVM
{
    public class ForgotPasswordPageViewModel : BaseViewModel
    {
        private readonly IPageDialogService _pageDialogService;
        private readonly ICustomerService _customerService;
        public ForgotPasswordPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService, ICustomerService customerService) : base(navigationService)
        {
            _pageDialogService = pageDialogService;
            _customerService = customerService;
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

        public ICommand SendCommand
        {
            get
            {
                return new Command(async () =>
                {
                    if (!string.IsNullOrEmpty(Email))
                    {

                        var checkMail = await _customerService.CheckEmailAsync(Email);
                        if (checkMail)
                        {
                            var checkPass = CheckPassword(Password, RePassword);
                            if (checkPass.Status)
                            {
                                var customer = await _customerService.GetCustomerByEmailAsync(Email);
                                if (customer != null)
                                {
                                    customer.Password = Password;
                                    var res = await _customerService.UpdateCustomerAsync(customer);
                                    if (res)
                                    {
                                        await Toast.Make("Şifre başarıyla değiştirilmiştir").Show();
                                        await _navigationService.GoBackAsync();
                                    }
                                    else
                                    {
                                        await _pageDialogService.DisplayAlertAsync("Hata", "Sistemsel Hata oluştu daha sonra tekrar deneyiniz", "Tamam");

                                    }
                                }
                                else
                                {
                                    await _pageDialogService.DisplayAlertAsync("Hata", "Sistemsel Hata oluştu daha sonra tekrar deneyiniz", "Tamam");

                                }
                            }
                            else
                            {
                                await _pageDialogService.DisplayAlertAsync("Hata", checkPass.Message, "Tamam");

                            }

                        }
                        else
                        {
                            await _pageDialogService.DisplayAlertAsync("Hata", "Email adresi hatalı.", "Tamam");

                        }
                    }
                    else
                    {
                        await _pageDialogService.DisplayAlertAsync("Hata", "Email Adresi boş bırakılamaz", "Tamam");

                    }
                });
            }
        }

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

        private Result CheckPassword(string pass, string rePass)
        {
            if (string.IsNullOrEmpty(pass) && string.IsNullOrEmpty(rePass))
            {
                return new Result(false, "Şifre ve Şifre tekrarı boş bırakılamaz");
            }

            if (pass.Length < 7 || pass.Length > 25 || rePass.Length < 7 || rePass.Length > 25)
            {
                return new Result(false, "Şifre uzunlukları 7 ve 25 karakter arasında olmalıdır.");

            }
            if (pass != rePass)
            {
                return new Result(false, "Şifreler eşleşmiyor.");

            }

            return new Result(true);
        }
    }
}

