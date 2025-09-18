using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Shadcn.Wpf.Presentation.Services;
using Shadcn.Wpf.Presentation.ViewModels;
using Shadcn.Wpf.Services;

namespace Shadcn.Wpf.Presentation.Configuration;

/// <summary>
/// Service configuration for dependency injection
/// </summary>
public static class ServiceConfiguration
{
    /// <summary>
    /// Configure all services
    /// </summary>
    public static IServiceCollection ConfigureServices(this IServiceCollection services)
    {
        // Register services
        services.AddSingleton<INavigationService, NavigationService>();
        services.AddSingleton<IMessageService, MessageService>();

        // Register ViewModels
        services.AddTransient<MainWindowViewModel>();
        services.AddTransient<HomePageViewModel>();
        services.AddTransient<ButtonsPageViewModel>();
        services.AddTransient<CardsPageViewModel>();
        services.AddTransient<FormsPageViewModel>();
        services.AddTransient<ProgressPageViewModel>();
        services.AddTransient<TypographyPageViewModel>();
        services.AddTransient<NavigationPageViewModel>();
        services.AddTransient<DatePickerDemoPageViewModel>();
        services.AddTransient<ToggleSwitchDemoPageViewModel>();
        services.AddTransient<AboutPageViewModel>();

        return services;
    }

    /// <summary>
    /// Initialize the IoC container
    /// </summary>
    public static void InitializeContainer()
    {
        var services = new ServiceCollection();
        services.ConfigureServices();

        var serviceProvider = services.BuildServiceProvider();
        Ioc.Default.ConfigureServices(serviceProvider);
    }
}
