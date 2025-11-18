using Microsoft.UI.Xaml;

namespace NewDawn.WinUI;

/// <summary>
/// App for Windows platform
/// </summary>
public partial class App : MauiWinUIApplication
{
    /// <summary>
    /// Constructor for App
    /// </summary>
    public App()
    {
        try
        {
            this.InitializeComponent();
        }// try
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error in App constructor: {ex.Message}");
            throw;
        }// catch
    }// App constructor

    /// <summary>
    /// Creates the MAUI app
    /// </summary>
    protected override MauiApp CreateMauiApp()
    {
        try
        {
            return MauiProgram.CreateMauiApp();
        }// try
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error in CreateMauiApp: {ex.Message}");
            throw;
        }// catch
    }// CreateMauiApp
}// App class
