using System;
using System.Windows.Input;
using Android.Media.Audiofx;
using VoilaMobileApp.Src.Base;
using VoilaMobileApp.Src.Services.Interfaces;
using VoilaMobileApp.Src.Utilts.Results;
using VoilaMobileApp.Src.Views;
using VoilaMobileApp.Src.Views.HomeNavigation;

namespace VoilaMobileApp.Src.ViewModels
{
    public class LoginPageViewModel : BaseViewModel
    {
        private readonly ICustomerService _customerService;
        private readonly IPageDialogService _pageDialogService;
        public LoginPageViewModel(INavigationService navigationService, ICustomerService customerService, IPageDialogService pageDialogService) : base(navigationService)
        {
            _customerService = customerService;
            _pageDialogService = pageDialogService;
        }

        #region Properties

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

        #endregion



        #region Commands
        public ICommand LoginCommand
        {
            get
            {
                return new Command(async () =>
                {
                    if (!string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Password))
                    {
                        var res = EmailAndPasswordValidation(Email, Password);

                        if (res.Status == true)
                        {
                            var result = await _customerService.LoginCusomterAsync(Email, Password);
                            if (result)
                            {
                                await _navigationService.NavigateAsync(nameof(HomeNavigationPage));

                            }
                            else
                            {
                                await _pageDialogService.DisplayAlertAsync("Hata", "Girilen bilgiler eksik veya yanlış. Tekrar deneyin.", "Tamam");

                            }
                        }
                        else
                        {
                            await _pageDialogService.DisplayAlertAsync("Hata", res.Message, "Tamam");

                        }
                    }
                    else
                    {
                        await _pageDialogService.DisplayAlertAsync("Hata", "Email ve şifre boş bırakılamaz.", "Tamam");

                    }


                });
            }
        }
        public ICommand RegisterCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await _navigationService.NavigateAsync(nameof(RegisterPage));

                });
            }
        }

        public ICommand ForgotCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await _navigationService.NavigateAsync(nameof(RegisterPage));

                });
            }
        }

        #endregion

        #region Methods

        private Result EmailAndPasswordValidation(string email, string password)
        {

            if (password.Length < 7 || password.Length >= 16)
            {
                return new Result(false, "Şifrenin karakter uzunluğu 7 den küçük 16 dan fazla olamaz.");
            }



            return new Result(true);
        }

        #endregion
    }
}

