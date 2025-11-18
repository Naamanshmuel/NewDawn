namespace NewDawn;

/// <summary>
/// Main application class
/// </summary>
public partial class App : Application
{
    /// <summary>
    /// Constructor for App
    /// </summary>
    public App()
    {
        try
        {
            InitializeComponent();

            MainPage = new AppShell();
        }// try
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error in App constructor: {ex.Message}");
            throw;
        }// catch
    }// App constructor
}// App class
