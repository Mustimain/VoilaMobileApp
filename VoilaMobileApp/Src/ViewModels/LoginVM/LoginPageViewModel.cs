using System;
using System.Windows.Input;
using VoilaMobileApp.Src.Base;
using VoilaMobileApp.Src.Views;
using VoilaMobileApp.Src.Views.HomeNavigation;

namespace VoilaMobileApp.Src.ViewModels
{
    public class LoginPageViewModel : BaseViewModel
    {
        public LoginPageViewModel(INavigationService navigationService) : base(navigationService)
        {
        }

        public ICommand RegisterCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await _navigationService.NavigateAsync(nameof(HomeNavigationPage));

                });
            }
        }
    }
}

