using Android.App;
using Android.Runtime;

namespace NewDawn;

/// <summary>
/// Main application for Android
/// </summary>
[Application]
public class MainApplication : MauiApplication
{
    /// <summary>
    /// Constructor for MainApplication
    /// </summary>
    public MainApplication(IntPtr handle, JniHandleOwnership ownership)
        : base(handle, ownership)
    {
    }// MainApplication constructor

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
}// MainApplication class
