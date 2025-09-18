using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Shadcn.Wpf.Models;
using Shadcn.Wpf.Services;

namespace Shadcn.Wpf.ViewModels;

/// <summary>
/// ViewModel for the FormsPage
/// </summary>
public partial class FormsPageViewModel : BasePageViewModel
{
    private readonly IMessageService _messageService;

    [ObservableProperty]
    private string _textInput = "";

    [ObservableProperty]
    private string _firstName = "";

    [ObservableProperty]
    private string _lastName = "";

    [ObservableProperty]
    private string _email = "";

    [ObservableProperty]
    private string _password = "";

    [ObservableProperty]
    private bool _checkBoxValue = false;

    [ObservableProperty]
    private bool _agreeToTerms = false;

    [ObservableProperty]
    private string _selectedOption = "";

    [ObservableProperty]
    private string _selectedCountry = "";

    [ObservableProperty]
    private Person? _selectedPerson;

    [ObservableProperty]
    private List<string> _availableOptions = new();

    [ObservableProperty]
    private List<string> _availableCountries = new();

    [ObservableProperty]
    private ObservableCollection<Person> _people = new();

    public FormsPageViewModel(IMessageService messageService) 
        : base("Form Components", "Interactive form controls and validation")
    {
        _messageService = messageService;
        InitializeOptions();
        InitializePeople();
    }

    /// <summary>
    /// Full name computed property
    /// </summary>
    public string FullName => $"{FirstName} {LastName}".Trim();

    /// <summary>
    /// Command to submit form
    /// </summary>
    [RelayCommand]
    private void SubmitForm()
    {
        var message = $"Form submitted!\n\nName: {FullName}\nEmail: {Email}\nCheckbox: {CheckBoxValue}\nTerms: {AgreeToTerms}\nSelected: {SelectedOption}\nCountry: {SelectedCountry}\nPerson: {SelectedPerson?.Name ?? "None"}";
        _messageService.ShowInformation(message, "Form Submission");
    }

    /// <summary>
    /// Command to reset form
    /// </summary>
    [RelayCommand]
    private void ResetForm()
    {
        TextInput = "";
        FirstName = "";
        LastName = "";
        Email = "";
        CheckBoxValue = false;
        AgreeToTerms = false;
        SelectedOption = "";
        SelectedCountry = "";
        SelectedPerson = null;
    }

    private void InitializeOptions()
    {
        AvailableOptions = new List<string>
        {
            "Option 1",
            "Option 2", 
            "Option 3",
            "Option 4"
        };

        AvailableCountries = new List<string>
        {
            "United States",
            "Canada", 
            "United Kingdom",
            "Germany",
            "France",
            "Japan",
            "Australia"
        };
    }

    private void InitializePeople()
    {
        People = new ObservableCollection<Person>
        {
            new() { Id = 1, Name = "Alice Johnson", Email = "alice@example.com" },
            new() { Id = 2, Name = "Bob Smith", Email = "bob@example.com" },
            new() { Id = 3, Name = "Charlie Brown", Email = "charlie@example.com" },
            new() { Id = 4, Name = "Diana Davis", Email = "diana@example.com" },
            new() { Id = 5, Name = "Eve Wilson", Email = "eve@example.com" }
        };
    }
}
