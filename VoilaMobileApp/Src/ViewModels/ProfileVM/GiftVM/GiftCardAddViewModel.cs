using System;
using System.Windows.Input;
using VoilaMobileApp.Src.Base;

namespace VoilaMobileApp.Src.ViewModels.ProfileVM.GiftVM
{
    public class GiftCardAddViewModel : BaseViewModel
    {
        public GiftCardAddViewModel(INavigationService navigationService) : base(navigationService)
        {
        }

        public ICommand SaveCommand
        {

            get
            {
                return new Command(async () =>
                {

                });
            }
        }
    }
}

