using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using VoilaMobileApp.Src.Base;
using VoilaMobileApp.Src.Services.Interfaces;
using VoilaMobileApp.Src.Views.ProfileViews.GiftCardViews;

namespace VoilaMobileApp.Src.ViewModels.GiftVM
{
    public class GiftPageViewModel : BaseViewModel, INavigationAware, IPageLifecycleAware
    {
        private readonly IGiftService _giftService;
        public GiftPageViewModel(INavigationService navigationService, IGiftService giftService) : base(navigationService)
        {
            _giftService = giftService;
        }

        private ObservableCollection<Models.GiftCard> _giftCardList = new ObservableCollection<Models.GiftCard>();
        public ObservableCollection<Models.GiftCard> GiftCardList
        {
            get
            {
                return _giftCardList;
            }
            set
            {
                _giftCardList = value; RaisePropertyChanged();
            }
        }

        public async void OnAppearing()
        {
            GiftCardList.Clear();
            var giftList = await _giftService.GetAllMyGiftCards();
            giftList.ForEach(gft => GiftCardList.Add(gft));
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

