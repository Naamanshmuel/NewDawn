using Foundation;

namespace NewDawn;

/// <summary>
/// App delegate for iOS
/// </summary>
[Register("AppDelegate")]
public class AppDelegate : MauiUIApplicationDelegate
{
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
}// AppDelegate class
