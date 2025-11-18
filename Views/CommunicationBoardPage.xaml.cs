using NewDawn.ViewModels;

namespace NewDawn.Views;

public partial class CommunicationBoardPage : ContentPage
{
    public CommunicationBoardPage(CommunicationBoardViewModel viewModel)
    {
        try
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error in CommunicationBoardPage constructor: {ex.Message}");
            throw;
        }
    }
}
