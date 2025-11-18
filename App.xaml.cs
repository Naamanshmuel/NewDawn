namespace NewDawn;

public partial class App : Application
{
    public App()
    {
        try
        {
            InitializeComponent();

            MainPage = new AppShell();
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error in App constructor: {ex.Message}");
            throw;
        }
    }
}
