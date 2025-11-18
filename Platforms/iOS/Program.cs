using UIKit;

namespace NewDawn;

/// <summary>
/// Program entry point for iOS
/// </summary>
public class Program
{
    /// <summary>
    /// Main entry point
    /// </summary>
    static void Main(string[] args)
    {
        try
        {
            UIApplication.Main(args, null, typeof(AppDelegate));
        }// try
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error in Main: {ex.Message}");
            throw;
        }// catch
    }// Main
}// Program class
