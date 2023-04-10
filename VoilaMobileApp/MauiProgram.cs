using Microsoft.Extensions.Logging;
using VoilaMobileApp.Src.ViewModels;
using VoilaMobileApp.Src.Views;

namespace VoilaMobileApp;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UsePrism(prism =>
            {
                prism.RegisterTypes(container =>
                {
                    container.RegisterForNavigation<LoginPage, LoginPageViewModel>();
                    container.RegisterForNavigation<RegisterPage, RegisterPageViewModel>();

                }).OnAppStart(nameof(LoginPage));
            });

        builder.Logging.AddDebug();

        return builder.Build();
    }
}

