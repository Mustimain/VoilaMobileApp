using System;
using Prism.Navigation;

namespace VoilaMobileApp.Src.Base
{
    public class BaseViewModel : BindableBase, IPageLifecycleAware, IInitialize, INavigationAware
    {
        protected INavigationService _navigationService { get; set; }

        public BaseViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }



        public void Initialize(INavigationParameters parameters)
        {
        }

        public void OnAppearing()
        {
        }

        public void OnDisappearing()
        {
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
        }
    }
}

