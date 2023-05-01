using Microsoft.Extensions.Logging;
using VoilaMobileApp.Platforms.Android.CustomControls;
using VoilaMobileApp.Src.CustomControls;
using VoilaMobileApp.Src.ViewModels;
using VoilaMobileApp.Src.ViewModels.BasketVM;
using VoilaMobileApp.Src.ViewModels.GiftVM;
using VoilaMobileApp.Src.ViewModels.HomeVM;
using VoilaMobileApp.Src.ViewModels.MenuVM;
using VoilaMobileApp.Src.ViewModels.ProfileVM;
using VoilaMobileApp.Src.Views;
using VoilaMobileApp.Src.Views.BasketViews;
using VoilaMobileApp.Src.Views.GiftViews;
using VoilaMobileApp.Src.Views.HomeNavigation;
using VoilaMobileApp.Src.Views.HomeViews;
using VoilaMobileApp.Src.Views.MenuViews;
using VoilaMobileApp.Src.Views.ProfileViews;

namespace VoilaMobileApp;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>().ConfigureFonts(fonts =>
            {
                fonts.AddFont("Anybody-Regular.ttf", "Anybody");
                fonts.AddFont("Arizonia-Regular.ttf", "Arizona");

            })
            .UsePrism(prism =>
            {
                prism.RegisterTypes(container =>
                {
                    ViewModelLocationProvider.Register<LoginPage, LoginPageViewModel>();
                    ViewModelLocationProvider.Register<RegisterPage, RegisterPageViewModel>();
                    ViewModelLocationProvider.Register<BasketPage, BasketPageViewModel>();
                    ViewModelLocationProvider.Register<GiftPage, GiftPageViewModel>();
                    ViewModelLocationProvider.Register<HomePage, HomePageViewModel>();
                    ViewModelLocationProvider.Register<FoodMenuPage, FoodMenuPageViewModel>();
                    ViewModelLocationProvider.Register<DrinkMenuPage, DrinkMenuPageViewModel>();
                    ViewModelLocationProvider.Register<ProfilePage, ProfilePageViewModel>();




                    container.RegisterForNavigation<NavigationPage>();
                    container.RegisterForNavigation<LoginPage, LoginPageViewModel>();
                    container.RegisterForNavigation<RegisterPage, RegisterPageViewModel>();
                    container.RegisterForNavigation<BasketPage, BasketPageViewModel>();
                    container.RegisterForNavigation<GiftPage, GiftPageViewModel>();
                    container.RegisterForNavigation<HomePage, HomePageViewModel>();
                    container.RegisterForNavigation<FoodMenuPage, FoodMenuPageViewModel>();
                    container.RegisterForNavigation<DrinkMenuPage, DrinkMenuPageViewModel>();
                    container.RegisterForNavigation<ProfilePage, ProfilePageViewModel>();
                    container.RegisterForNavigation<HomeNavigationPage>();


                }).OnAppStart(nameof(HomeNavigationPage));
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

