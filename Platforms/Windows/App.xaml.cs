using Microsoft.UI.Xaml;

namespace NewDawn.WinUI;

public partial class App : MauiWinUIApplication
{
    public App()
    {
        try
        {
            this.InitializeComponent();
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error in App constructor: {ex.Message}");
            throw;
        }
    }

    protected override MauiApp CreateMauiApp()
    {
        try
        {
            return MauiProgram.CreateMauiApp();
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error in CreateMauiApp: {ex.Message}");
            throw;
        }
    }
}
