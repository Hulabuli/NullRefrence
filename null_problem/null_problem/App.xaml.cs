using Common.Services;
using GUI.Interfaces;
using GUI.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using SampleCore.Services;
using SessionCore.Services;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace null_problem;

/// <summary>
/// Provides application-specific behavior to supplement the default Application class.
/// </summary>
public partial class App : Application
{
    private readonly IServiceScope _scope;

    /// <summary>
    /// Initializes the singleton application object.  This is the first line of authored code
    /// executed, and as such is the logical equivalent of main() or WinMain().
    /// </summary>
    public App()
    {
        Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NjA0NDAxQDMyMzAyZTMxMmUzMFFKdnVrM080OTFTbWdDc1ZUT2FVb2d2ZXdYMWZrdEFNZDFtNXU5dHdIdFE9");
        this.InitializeComponent();
        var services = new ServiceCollection()
            .AddCommonServices()
            .AddSampleCoreServices()
            .AddSessionCoreServices()
            .AddGUIServices();

        _scope = services.BuildServiceProvider(validateScopes: true).CreateScope();
    }

    /// <summary>
    /// Invoked when the application is launched normally by the end user.  Other entry points
    /// will be used such as when the application is launched to open a specific file.
    /// </summary>
    /// <param name="args">Details about the launch request and process.</param>
    protected override void OnLaunched(LaunchActivatedEventArgs args)
    {
        var mainWindow = _scope.ServiceProvider.GetRequiredService<IWindow>();
        mainWindow.Activate();
    }
}