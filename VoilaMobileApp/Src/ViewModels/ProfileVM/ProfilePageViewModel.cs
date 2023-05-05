using System;
using System.Windows.Input;
using VoilaMobileApp.Src.Base;
using VoilaMobileApp.Src.Views;
using VoilaMobileApp.Src.Views.GiftViews;
using VoilaMobileApp.Src.Views.ProfileViews;
using VoilaMobileApp.Src.Views.ProfileViews.HelpViews;
using VoilaMobileApp.Src.Views.ProfileViews.MyAddressViews;
using VoilaMobileApp.Src.Views.ProfileViews.MyOrderViews;

namespace VoilaMobileApp.Src.ViewModels.ProfileVM
{
    public class ProfilePageViewModel : BaseViewModel
    {

        public ProfilePageViewModel(INavigationService navigationService) : base(navigationService)
        {
        }



        public ICommand ProfileEditCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await _navigationService.NavigateAsync(nameof(EditProfilePage));

                });
            }
        }

        public ICommand AddressDetailCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await _navigationService.NavigateAsync(nameof(MyAddressPage));


                });
            }
        }

        public ICommand OrderDetailCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await _navigationService.NavigateAsync(nameof(MyOrdersPage));

                });
            }
        }

        public ICommand GiftDetailCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await _navigationService.NavigateAsync(nameof(GiftPage));
                });
            }
        }

        public ICommand HelpCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await _navigationService.NavigateAsync(nameof(HelpPage));
                });
            }
        }

        public ICommand LogoutCommand
        {
            get
            {
                return new Command(async () =>
                {

                    await _navigationService.GoBackAsync();
                });
            }
        }

        public async void OnAppearing()
        {
        }



    }
}

