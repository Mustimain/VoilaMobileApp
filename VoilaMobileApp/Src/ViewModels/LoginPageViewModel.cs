using System;
using System.Windows.Input;
using VoilaMobileApp.Src.Base;
using VoilaMobileApp.Src.Views;

namespace VoilaMobileApp.Src.ViewModels
{
    public class LoginPageViewModel : BaseViewModel
    {
        public LoginPageViewModel(INavigationService navigationService) : base(navigationService)
        {
        }

        public ICommand ButtonClick
        {
            get
            {
                return new Command(async () =>
                {
                    await _navigationService.NavigateAsync(nameof(RegisterPage));

                });
            }
        }
    }
}

