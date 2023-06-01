using CommunityToolkit.Maui;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.PlatformConfiguration.AndroidSpecific;
using Prism;
using Prism.Mvvm;
using VoilaMobileApp.Platforms.Android.CustomControls;
using VoilaMobileApp.Src.CustomControls;
using VoilaMobileApp.Src.Services.Concrete;
using VoilaMobileApp.Src.Services.Interfaces;
using VoilaMobileApp.Src.ViewModels;
using VoilaMobileApp.Src.ViewModels.BasketVM;
using VoilaMobileApp.Src.ViewModels.ForgotPasswordVM;
using VoilaMobileApp.Src.ViewModels.GiftVM;
using VoilaMobileApp.Src.ViewModels.HomeVM;
using VoilaMobileApp.Src.ViewModels.MenuVM;
using VoilaMobileApp.Src.ViewModels.PaymentVM;
using VoilaMobileApp.Src.ViewModels.ProfileVM;
using VoilaMobileApp.Src.ViewModels.ProfileVM.Address;
using VoilaMobileApp.Src.ViewModels.ProfileVM.EditProfile;
using VoilaMobileApp.Src.ViewModels.ProfileVM.GiftVM;
using VoilaMobileApp.Src.ViewModels.ProfileVM.MyOrdersVM;
using VoilaMobileApp.Src.Views;
using VoilaMobileApp.Src.Views.BasketViews;
using VoilaMobileApp.Src.Views.ForgetPasswordViews;
using VoilaMobileApp.Src.Views.GiftViews;
using VoilaMobileApp.Src.Views.HomeNavigation;
using VoilaMobileApp.Src.Views.HomeViews;
using VoilaMobileApp.Src.Views.MenuViews;
using VoilaMobileApp.Src.Views.PaymentViews;
using VoilaMobileApp.Src.Views.ProfileViews;
using VoilaMobileApp.Src.Views.ProfileViews.GiftCardViews;
using VoilaMobileApp.Src.Views.ProfileViews.HelpViews;
using VoilaMobileApp.Src.Views.ProfileViews.MyAddressViews;
using VoilaMobileApp.Src.Views.ProfileViews.MyAddressViews.AddressAddViews;
using VoilaMobileApp.Src.Views.ProfileViews.MyOrderViews;

namespace VoilaMobileApp;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("Anybody-Regular.ttf", "Anybody");
                fonts.AddFont("Arizonia-Regular.ttf", "Arizona");

            })
            .UsePrism(prism =>
            {

                prism.RegisterTypes(container =>
                {

                    container.RegisterInstance(typeof(ICategoryService), new CategoryService());
                    container.RegisterInstance(typeof(IProductService), new ProductService());
                    container.RegisterInstance(typeof(ICustomerService), new CustomerService());
                    container.RegisterInstance(typeof(IAddressService), new AddressService());
                    container.RegisterInstance(typeof(IOrderClaimService), new OrderClaimService(new ProductService()));
                    container.RegisterInstance(typeof(IOrderService), new OrderService(new ProductService(), new OrderClaimService(new ProductService())));
                    container.RegisterInstance(typeof(IGiftService), new GiftService());





                    ViewModelLocationProvider2.Register<LoginPage, LoginPageViewModel>();
                    ViewModelLocationProvider2.Register<RegisterPage, RegisterPageViewModel>();
                    ViewModelLocationProvider2.Register<BasketPage, BasketPageViewModel>();
                    ViewModelLocationProvider2.Register<GiftPage, GiftPageViewModel>();
                    ViewModelLocationProvider2.Register<HomePage, HomePageViewModel>();
                    ViewModelLocationProvider2.Register<ProfilePage, ProfilePageViewModel>();
                    ViewModelLocationProvider2.Register<HomeNavigationPage, HomeNavigationPageViewModel>();
                    ViewModelLocationProvider2.Register<MenuPage, MenuPageViewModel>();
                    ViewModelLocationProvider2.Register<PaymentPage, PaymentPageViewModel>();
                    ViewModelLocationProvider2.Register<ProductsPage, ProductsPageViewModel>();
                    ViewModelLocationProvider2.Register<EditProfilePage, EditProfilePageViewModel>();
                    ViewModelLocationProvider2.Register<MyAddressPage, MyAddressPageViewModel>();
                    ViewModelLocationProvider2.Register<AddressAddPage, AddressAddPageViewModel>();
                    ViewModelLocationProvider2.Register<GiftCardAddPage, GiftCardAddViewModel>();
                    ViewModelLocationProvider2.Register<MyOrdersPage, MyOrdersPageViewModel>();
                    ViewModelLocationProvider2.Register<GiftCardPaymentPage, GiftCardPaymentPageViewModel>();
                    ViewModelLocationProvider2.Register<ForgotPasswordPage, ForgotPasswordPageViewModel>();




                    container.RegisterForNavigation<NavigationPage>();
                    container.RegisterForNavigation<LoginPage, LoginPageViewModel>();
                    container.RegisterForNavigation<RegisterPage, RegisterPageViewModel>();
                    container.RegisterForNavigation<BasketPage, BasketPageViewModel>();
                    container.RegisterForNavigation<GiftPage, GiftPageViewModel>();
                    container.RegisterForNavigation<HomePage, HomePageViewModel>();
                    container.RegisterForNavigation<ProfilePage, ProfilePageViewModel>();
                    container.RegisterForNavigation<HomeNavigationPage, HomeNavigationPageViewModel>();
                    container.RegisterForNavigation<MenuPage, MenuPageViewModel>();
                    container.RegisterForNavigation<PaymentPage, PaymentPageViewModel>();
                    container.RegisterForNavigation<ProductsPage, ProductsPageViewModel>();
                    container.RegisterForNavigation<EditProfilePage, EditProfilePageViewModel>();
                    container.RegisterForNavigation<MyAddressPage, MyAddressPageViewModel>();
                    container.RegisterForNavigation<AddressAddPage, AddressAddPageViewModel>();
                    container.RegisterForNavigation<GiftCardAddPage, GiftCardAddViewModel>();
                    container.RegisterForNavigation<MyOrdersPage, MyOrdersPageViewModel>();
                    container.RegisterForNavigation<GiftCardPaymentPage, GiftCardPaymentPageViewModel>();
                    container.RegisterForNavigation<ForgotPasswordPage, ForgotPasswordPageViewModel>();
                    container.RegisterForNavigation<HelpPage>();

                }).OnAppStart(nameof(LoginPage));
            });


        builder.Logging.AddDebug();

        Microsoft.Maui.Handlers.ElementHandler.ElementMapper.AppendToMapping("entry1", (handler, view) =>
        {
            if (view is CustomEntry)
            {
                EntryMapper.Map(handler, view);
            }
        });

        Microsoft.Maui.Handlers.ElementHandler.ElementMapper.AppendToMapping("picker1", (handler, view) =>
        {
            if (view is CustomPicker)
            {
                PickerMapper.Map(handler, view);
            }
        });

        Microsoft.Maui.Handlers.ElementHandler.ElementMapper.AppendToMapping("datepicker1", (handler, view) =>
        {
            if (view is CustomDatePicker)
            {
                DatePickerMapper.Map(handler, view);
            }
        });



        return builder.Build();
    }
}

