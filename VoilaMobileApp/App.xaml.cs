using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Controls.PlatformConfiguration.AndroidSpecific;

namespace VoilaMobileApp;

public partial class App : Microsoft.Maui.Controls.Application
{
    public App()
    {
        // App.Current.On<Microsoft.Maui.Controls.PlatformConfiguration.Android>().UseWindowSoftInputModeAdjust(WindowSoftInputModeAdjust.Resize);

        InitializeComponent();
    }
}

