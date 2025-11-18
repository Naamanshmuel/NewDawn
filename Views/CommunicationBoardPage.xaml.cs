using NewDawn.ViewModels;

namespace NewDawn.Views;

/// <summary>
/// Communication board page
/// </summary>
public partial class CommunicationBoardPage : ContentPage
{
    /// <summary>
    /// Constructor for CommunicationBoardPage
    /// </summary>
    public CommunicationBoardPage(CommunicationBoardViewModel viewModel)
    {
        try
        {
            InitializeComponent();
            BindingContext = viewModel;
        }// try
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error in CommunicationBoardPage constructor: {ex.Message}");
            throw;
        }// catch
    }// CommunicationBoardPage constructor
}// CommunicationBoardPage class
