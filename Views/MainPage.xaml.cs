using NewDawn.ViewModels;

namespace NewDawn.Views;

/// <summary>
/// Main page for the application
/// </summary>
public partial class MainPage : ContentPage
{
    /// <summary>
    /// Constructor for MainPage
    /// </summary>
    public MainPage(MainViewModel viewModel)
    {
        try
        {
            InitializeComponent();
            BindingContext = viewModel;
        }// try
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error in MainPage constructor: {ex.Message}");
            throw;
        }// catch
    }// MainPage constructor
}// MainPage class
