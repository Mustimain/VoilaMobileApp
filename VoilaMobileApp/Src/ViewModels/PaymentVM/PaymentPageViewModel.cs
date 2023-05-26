using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Maui.Alerts;
using VoilaMobileApp.Src.Base;
using VoilaMobileApp.Src.Models;
using VoilaMobileApp.Src.Services.Interfaces;
using VoilaMobileApp.Src.Utilts.Results;

namespace VoilaMobileApp.Src.ViewModels.PaymentVM
{
    public class PaymentPageViewModel : BaseViewModel, IPageLifecycleAware, INavigatedAware
    {

        private ObservableCollection<Models.Address> _addressList = new ObservableCollection<Models.Address>();
        public ObservableCollection<Models.Address> AddressList
        {
            get
            {
                return _addressList;
            }
            set
            {
                _addressList = value; RaisePropertyChanged();
            }
        }

        private ObservableCollection<GiftCard> _giftCardList = new ObservableCollection<GiftCard>();
        public ObservableCollection<GiftCard> GiftCardList
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

        private readonly IAddressService _addressService;
        private readonly IOrderService _orderService;
        private readonly IOrderClaimService _orderClaimService;
        public PaymentPageViewModel(INavigationService navigationService, IAddressService addressService, IOrderService orderService, IOrderClaimService orderClaimService) : base(navigationService)
        {
            _addressService = addressService;
            _orderClaimService = orderClaimService;
            _orderService = orderService;
        }


        public async void OnAppearing()
        {

            var addressResult = await _addressService.GetAllAddressAsync();
            addressResult.ForEach(ad => AddressList.Add(ad));
            foreach (var basketProd in Utilts.BasketList.GlobalBasketList)
            {
                TotalBasketPrice = (double)(TotalBasketPrice + (basketProd.ProductCount * basketProd.Product.Price));
            }

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

        private Address _selectionAddress;
        public Address SelectionAddress
        {
            get
            {
                return _selectionAddress;
            }
            set
            {
                _selectionAddress = value; RaisePropertyChanged();
            }
        }

        private GiftCard _selectionGiftCard;
        public GiftCard SelectionGiftCard
        {
            get
            {
                return _selectionGiftCard;
            }
            set
            {
                _selectionGiftCard = value; RaisePropertyChanged();
            }
        }


        public ICommand ComplatePaymentCommand
        {
            get
            {
                return new Command(async () =>
                {
                    var paymentDetail = new Payment
                    {
                        address = SelectionAddress,
                        GiftCard = SelectionGiftCard,
                        CardCvv = CardCvv,
                        CardDayMonth = CardDayMonth,
                        CardNo = CardNo,
                        CardOwnerName = CardOwnerName
                    };

                    var paymentValidate = PaymentValidate(paymentDetail);
                    if (paymentValidate.Status)
                    {
                        var ordId = Guid.NewGuid().ToString();
                        var orderResult = await _orderService.AddOrderAsync(new Order
                        {
                            Id = ordId,
                            OrderStatus = Models.Enums.OrderStatusType.SiparişAlındı,
                            AddressId = SelectionAddress.Id,
                            CustomerId = Utilts.CustomerInfo.CurrentCustomer.Id,
                            GiftCode = SelectionGiftCard?.GiftCode,
                            OrderDate = DateTime.Now,
                            TotalPrice = TotalBasketPrice
                        });
                        if (orderResult)
                        {
                            foreach (var basketProduct in Utilts.BasketList.GlobalBasketList)
                            {
                                await _orderClaimService.AddOrderClaim(new OrderClaim
                                {
                                    Id = Guid.NewGuid().ToString(),
                                    NumberOfProduct = basketProduct.ProductCount,
                                    OrderId = ordId,
                                    ProductId = basketProduct.Product.Id
                                });
                            }
                        }
                        else
                        {

                        }
                    }
                    else
                    {
                        await Toast.Make(paymentValidate.Message).Show();

                    }
                });
            }
        }

        private Result PaymentValidate(Payment payment)
        {
            if (payment.address == null)
            {
                return new Result(false, "Adres boş bırakılamaz.");
            }
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

