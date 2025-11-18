using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using NewDawn.Models;
using NewDawn.Services;

namespace NewDawn.ViewModels;

/// <summary>
/// View model for the communication board
/// </summary>
public class CommunicationBoardViewModel : INotifyPropertyChanged
{
    private readonly ICommunicationService _communicationService;
    private CommunicationBoard? _board;
    private ResponseCandidate? _selectedResponse;
    private string _statusMessage;

    public event PropertyChangedEventHandler? PropertyChanged;

    public ObservableCollection<ResponseCandidate> Candidates { get; set; }

    public ResponseCandidate? SelectedResponse
    {
        get => _selectedResponse;
        set
        {
            try
            {
                _selectedResponse = value;
                OnPropertyChanged();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in SelectedResponse setter: {ex.Message}");
            }
        }
    }

    public string StatusMessage
    {
        get => _statusMessage;
        set
        {
            try
            {
                _statusMessage = value;
                OnPropertyChanged();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in StatusMessage setter: {ex.Message}");
            }
        }
    }

    public ICommand SelectResponseCommand { get; }

    public CommunicationBoardViewModel(ICommunicationService communicationService)
    {
        try
        {
            _communicationService = communicationService ?? throw new ArgumentNullException(nameof(communicationService));
            
            Candidates = new ObservableCollection<ResponseCandidate>();
            _statusMessage = "Select a response";

            SelectResponseCommand = new Command<ResponseCandidate>(async (candidate) => await SelectResponseAsync(candidate));
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error in CommunicationBoardViewModel constructor: {ex.Message}");
            throw;
        }
    }

    public async Task LoadBoardAsync(CommunicationBoard board)
    {
        try
        {
            if (board == null)
            {
                throw new ArgumentNullException(nameof(board));
            }

            _board = board;
            Candidates.Clear();
            
            foreach (var candidate in board.Candidates)
            {
                Candidates.Add(candidate);
            }

            StatusMessage = $"Loaded {Candidates.Count} response options";
            await Task.CompletedTask;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error in LoadBoardAsync: {ex.Message}");
            StatusMessage = $"Error loading board: {ex.Message}";
        }
    }

    private async Task SelectResponseAsync(ResponseCandidate candidate)
    {
        try
        {
            if (candidate == null)
            {
                return;
            }

            var conversation = _communicationService.CurrentConversation;
            if (conversation == null)
            {
                StatusMessage = "No active conversation";
                return;
            }

            // Deselect all other candidates
            foreach (var c in Candidates)
            {
                c.Deselect();
            }

            // Select this candidate
            candidate.Select();
            SelectedResponse = candidate;

            await _communicationService.SelectResponseAsync(candidate, conversation);
            
            StatusMessage = $"Selected: {candidate.Text}";
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error in SelectResponseAsync: {ex.Message}");
            StatusMessage = $"Error selecting response: {ex.Message}";
        }
    }

    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        try
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error in OnPropertyChanged: {ex.Message}");
        }
    }
}
