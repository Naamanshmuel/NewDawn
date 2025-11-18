namespace NewDawn;

/// <summary>
/// Application shell
/// </summary>
public partial class AppShell : Shell
{
    /// <summary>
    /// Constructor for AppShell
    /// </summary>
    public AppShell()
    {
        try
        {
            InitializeComponent();
        }// try
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error in AppShell constructor: {ex.Message}");
            throw;
        }// catch
    }// AppShell constructor
}// AppShell class
