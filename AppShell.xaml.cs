namespace NewDawn;

public partial class AppShell : Shell
{
    public AppShell()
    {
        try
        {
            InitializeComponent();
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error in AppShell constructor: {ex.Message}");
            throw;
        }
    }
}
