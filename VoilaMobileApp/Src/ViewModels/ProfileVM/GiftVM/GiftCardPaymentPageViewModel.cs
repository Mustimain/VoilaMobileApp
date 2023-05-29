using System;
using System.Windows.Input;
using CommunityToolkit.Maui.Alerts;
using VoilaMobileApp.Src.Base;
using VoilaMobileApp.Src.Models;
using VoilaMobileApp.Src.Services.Interfaces;
using VoilaMobileApp.Src.Utilts.Results;
using VoilaMobileApp.Src.Views.HomeViews;

namespace VoilaMobileApp.Src.ViewModels.ProfileVM.GiftVM
{
    public class GiftCardPaymentPageViewModel : BaseViewModel, IPageLifecycleAware, INavigationAware
    {
        private readonly IGiftService _giftService;
        public GiftCardPaymentPageViewModel(INavigationService navigationService, IGiftService giftService) : base(navigationService)
        {
            _giftService = giftService;
        }



        private double _totalBasketPrice = 0;
        public double TotalBasketPrice
        {
            get
            {
                return _totalBasketPrice;
            }
            set
            {
                _totalBasketPrice = value; RaisePropertyChanged();
            }
        }

        private string _cardNo;
        public string CardNo
        {
            get
            {
                return _cardNo;
            }
            set
            {
                _cardNo = value; RaisePropertyChanged();
            }
        }

        private string _cardOwnerName;
        public string CardOwnerName
        {
            get
            {
                return _cardOwnerName;
            }
            set
            {
                _cardOwnerName = value; RaisePropertyChanged();
            }
        }

        private string _cardDayMonth;
        public string CardDayMonth
        {
            get
            {
                return _cardDayMonth;
            }
            set
            {
                _cardDayMonth = value; RaisePropertyChanged();
            }
        }

        private string _cardCvv;
        public string CardCvv
        {
            get
            {
                return _cardCvv;
            }
            set
            {
                _cardCvv = value; RaisePropertyChanged();
            }
        }

        private Models.GiftCard _buyGiftCard;
        public Models.GiftCard BuyGiftCard
        {
            get
            {
                return _buyGiftCard;
            }
            set
            {
                _buyGiftCard = value; RaisePropertyChanged();
            }
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            var paramCheck = parameters.ContainsKey("buyGiftCard");

            if (paramCheck)
            {
                var currentGiftCard = parameters.GetValue<GiftCard>("buyGiftCard");
                BuyGiftCard = currentGiftCard;
                TotalBasketPrice = currentGiftCard.GiftAmount;
            }

        }

        public ICommand ComplatePaymentCommand
        {
            get
            {
                return new Command(async () =>
                {
                    var newPayment = new Payment
                    {
                        address = null,
                        CardCvv = CardCvv,
                        CardDayMonth = CardDayMonth,
                        CardNo = CardNo,
                        CardOwnerName = CardOwnerName,
                        GiftCard = null,

                    };
                    var validatePayment = PaymentValidate(newPayment);

                    if (validatePayment.Status)
                    {
                        var result = await _giftService.AddGiftCardAsync(BuyGiftCard);
                        if (result)
                        {
                            await Toast.Make("Hediye Kartı Başarıyla Oluşturuldu").Show();
                            await _navigationService.GoBackToRootAsync();
                        }
                        else
                        {
                            await Toast.Make("Hediye Kartı Oluşturulamadı").Show();

                        }
                    }
                    else
                    {
                        await Toast.Make(validatePayment.Message).Show();
                    }

                });
            }
        }

        private Result PaymentValidate(Payment payment)
        {

            if (string.IsNullOrEmpty(payment.CardCvv) ||
                string.IsNullOrEmpty(payment.CardDayMonth) ||
                string.IsNullOrEmpty(payment.CardNo) ||
                string.IsNullOrEmpty(payment.CardOwnerName))
            {
                return new Result(false, "Bilgiler boş bırakılamaz.");
            }

            if (payment.CardCvv.Length != 3)
            {
                return new Result(false, "Cvv bilgisini eksiksiz giriniz.");

            }
            if (payment.CardDayMonth.Length != 4)
            {
                return new Result(false, "Gün ve Ay bilgisini eksiksiz giriniz.");

            }
            if (payment.CardNo.Length != 16)
            {
                return new Result(false, "Kart numarasını eksiksiz giriniz.");

            }

            if (payment.CardOwnerName.Length <= 3 || payment.CardOwnerName.Length >= 50)
            {
                return new Result(false, "Kart sahibi ismi uzunlugu 3'den az 50'den fazla olamaz.");

            }

            return new Result(true);
        }
    }
}

