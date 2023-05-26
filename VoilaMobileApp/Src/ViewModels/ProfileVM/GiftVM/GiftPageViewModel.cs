using System;
using System.Windows.Input;
using VoilaMobileApp.Src.Base;
using VoilaMobileApp.Src.Views.ProfileViews.GiftCardViews;

namespace VoilaMobileApp.Src.ViewModels.GiftVM
{
    public class GiftPageViewModel : BaseViewModel
    {
        public GiftPageViewModel(INavigationService navigationService) : base(navigationService)
        {
        }



        public ICommand AddGiftCardCommand
        {

            get
            {
                return new Command(async () =>
                {
                    await _navigationService.NavigateAsync(nameof(GiftCardAddPage));
                });
            }
        }
    }
}

