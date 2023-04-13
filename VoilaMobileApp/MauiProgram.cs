using Microsoft.Extensions.Logging;
using VoilaMobileApp.Platforms.Android.CustomControls;
using VoilaMobileApp.Src.CustomControls;
using VoilaMobileApp.Src.ViewModels;
using VoilaMobileApp.Src.Views;

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
                    container.RegisterForNavigation<LoginPage, LoginPageViewModel>();
                    container.RegisterForNavigation<RegisterPage, RegisterPageViewModel>();

                }).OnAppStart(nameof(RegisterPage));
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

