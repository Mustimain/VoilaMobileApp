using System;
using System.Windows.Input;
using CommunityToolkit.Maui.Alerts;
using Microsoft.Maui.Animations;
using VoilaMobileApp.Src.Base;
using VoilaMobileApp.Src.Models;
using VoilaMobileApp.Src.Models.Enums;
using VoilaMobileApp.Src.Services.Interfaces;
using VoilaMobileApp.Src.Utilts.Results;
using VoilaMobileApp.Src.Views.PaymentViews;
using VoilaMobileApp.Src.Views.ProfileViews.GiftCardViews;
using Toast = CommunityToolkit.Maui.Alerts.Toast;

namespace VoilaMobileApp.Src.ViewModels.ProfileVM.GiftVM
{
    public class GiftCardAddViewModel : BaseViewModel, IPageLifecycleAware, INavigatedAware
    {
        private readonly IGiftService _giftService;
        public GiftCardAddViewModel(INavigationService navigationService, IGiftService giftService) : base(navigationService)
        {
            _giftService = giftService;
        }

        private double _giftAmount;
        public double GiftAmount
        {
            get
            {
                return _giftAmount;
            }
            set
            {
                _giftAmount = value; RaisePropertyChanged();
            }
        }
        private string _giftCode;
        public string GiftCode
        {
            get
            {
                return _giftCode;
            }
            set
            {
                _giftCode = value; RaisePropertyChanged();
            }
        }
        private DateTime _giftLastUseDate = DateTime.Now;
        public DateTime GiftLastUseDate
        {
            get
            {
                return _giftLastUseDate;
            }
            set
            {
                _giftLastUseDate = value; RaisePropertyChanged();
            }
        }

        private string _pageTitle;
        public string PageTitle
        {
            get
            {
                return _pageTitle;
            }
            set
            {
                _pageTitle = value; RaisePropertyChanged();
            }
        }





        public ICommand SaveCommand
        {
            get
            {
                return new Command(async () =>
                {
                    var newGiftCard = new GiftCard
                    {
                        Id = Guid.NewGuid().ToString(),
                        CustomerId = Utilts.CustomerInfo.CurrentCustomer.Id,
                        GiftAmount = GiftAmount,
                        GiftCode = GiftCode,
                        IsUsed = GiftStatusType.Kullanılabilir,
                        LastUseDate = GiftLastUseDate
                    };

                    var validateGiftCard = GiftCardValidate(newGiftCard);

                    if (validateGiftCard.Status)
                    {
                        var navParam = new NavigationParameters
                        {
                            {"buyGiftCard",newGiftCard }
                        };
                        await _navigationService.NavigateAsync(nameof(GiftCardPaymentPage), navParam);
                    }
                    else
                    {
                        await Toast.Make(validateGiftCard.Message).Show();

                    }

                });
            }
        }

        private Result GiftCardValidate(GiftCard giftCard)
        {
            if (string.IsNullOrEmpty(GiftCode) ||
                string.IsNullOrEmpty(GiftAmount.ToString())
                )
            {
                return new Result(false, "Alanlar boş bırakılamaz.");
            }
            if (GiftLastUseDate <= DateTime.Now)
            {
                return new Result(false, "Tarih bugünden küçük olamaz.");
            }

            if (GiftAmount < 0 || GiftAmount > 1000)
            {
                return new Result(false, "Hediye Tutarı 0 dan küçük olamaz.");
            }

            if (GiftCode.Length < 5 || GiftCode.Length > 15)
            {
                return new Result(false, "Hediye Kodunun uzunlugu 5 den küçük 15 den büyük olamaz.");

            }

            return new Result(true);
        }
    }
}

