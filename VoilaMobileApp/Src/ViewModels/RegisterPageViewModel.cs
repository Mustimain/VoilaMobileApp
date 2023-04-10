using System;
using System.Windows.Input;
using Prism.Navigation;
using VoilaMobileApp.Src.Base;

namespace VoilaMobileApp.Src.ViewModels
{
    public class RegisterPageViewModel : BaseViewModel
    {
        public RegisterPageViewModel(INavigationService navigationService) : base(navigationService)
        {
        }


        public ICommand ButtonClick
        {
            get
            {
                return new Command(async () =>
                {
                    await _navigationService.GoBackAsync();

                });
            }
        }
    }
}

