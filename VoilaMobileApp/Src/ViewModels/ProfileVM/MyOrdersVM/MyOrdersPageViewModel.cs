using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using VoilaMobileApp.Src.Base;
using VoilaMobileApp.Src.Services.Interfaces;

namespace VoilaMobileApp.Src.ViewModels.ProfileVM.MyOrdersVM
{
    public class MyOrdersPageViewModel : BaseViewModel, INavigationAware, IPageLifecycleAware
    {
        private ObservableCollection<Models.OrderDetail> _orderDetailList = new ObservableCollection<Models.OrderDetail>();
        public ObservableCollection<Models.OrderDetail> OrderDetailList
        {
            get
            {
                return _orderDetailList;
            }
            set
            {
                _orderDetailList = value; RaisePropertyChanged();
            }
        }

        private readonly IOrderService _orderService;
        public MyOrdersPageViewModel(INavigationService navigationService, IOrderService orderService) : base(navigationService)
        {
            _orderService = orderService;
        }

        public async void OnAppearing()
        {
            var orderDetList = await _orderService.GetAllOrdersDetailAsync();
            orderDetList.ForEach(det => OrderDetailList.Add(det));
        }




    }
}

