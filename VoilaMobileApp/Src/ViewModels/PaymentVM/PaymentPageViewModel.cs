using System;
using VoilaMobileApp.Src.Base;

namespace VoilaMobileApp.Src.ViewModels.PaymentVM
{
    public class PaymentPageViewModel : BaseViewModel
    {
        public PaymentPageViewModel(INavigationService navigationService) : base(navigationService)
        {
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

    }
}

