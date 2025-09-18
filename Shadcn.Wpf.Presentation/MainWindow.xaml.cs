using System.Windows;
using CommunityToolkit.Mvvm.DependencyInjection;
using Shadcn.Wpf.Presentation.ViewModels;
using Shadcn.Wpf.Models;
using Shadcn.Wpf.Controls;
using Shadcn.Wpf.Presentation.Services;

namespace Shadcn.Wpf.Presentation;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : ShadcnWindow
{
    private readonly MainWindowViewModel _viewModel;

    public MainWindow()
    {
        InitializeComponent();
        
        // Get services from IoC container
        var navigationService = Ioc.Default.GetRequiredService<INavigationService>();
        _viewModel = Ioc.Default.GetRequiredService<MainWindowViewModel>();
        
        // Initialize navigation service with the frame
        if (navigationService is NavigationService navService)
        {
            navService.Initialize(MainContentFrame);
        }
        
        // Set DataContext to ViewModel
        DataContext = _viewModel;
        
        // Initialize navigation after everything is set up
        _viewModel.InitializeNavigation();

        // Add menu item for ShadcnWindow test
        KeyDown += MainWindow_KeyDown;
    }
    
    private void MainWindow_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
    {
        // Press F12 to open ShadcnWindow test
        if (e.Key == System.Windows.Input.Key.F12)
        {
            var testWindow = new TestShadcnWindow();
            testWindow.Show();
        }
    }
}
