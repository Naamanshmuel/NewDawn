using UIKit;

namespace NewDawn;

public class Program
{
    static void Main(string[] args)
    {
        try
        {
            UIApplication.Main(args, null, typeof(AppDelegate));
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error in Main: {ex.Message}");
            throw;
        }
    }
}
