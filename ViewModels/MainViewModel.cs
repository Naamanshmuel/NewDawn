using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using NewDawn.Models;
using NewDawn.Services;

namespace NewDawn.ViewModels;

/// <summary>
/// Main view model for the application
/// </summary>
public class MainViewModel : INotifyPropertyChanged
{
    private readonly ISpeechRecognitionService _speechRecognitionService;
    private readonly ICommunicationService _communicationService;
    private Conversation? _currentConversation;
    private string _statusMessage;
    private bool _isListening;
    private string _transcribedText;

    public event PropertyChangedEventHandler? PropertyChanged;

    public ObservableCollection<Message> Messages { get; set; }
    
    public string StatusMessage
    {
        get => _statusMessage;
        set
        {
            try
            {
                _statusMessage = value;
                OnPropertyChanged();
            }// try
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in StatusMessage setter: {ex.Message}");
            }// catch
        }// set
    }// StatusMessage

    public bool IsListening
    {
        get => _isListening;
        set
        {
            try
            {
                _isListening = value;
                OnPropertyChanged();
            }// try
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in IsListening setter: {ex.Message}");
            }// catch
        }// set
    }// IsListening

    public string TranscribedText
    {
        get => _transcribedText;
        set
        {
            try
            {
                _transcribedText = value;
                OnPropertyChanged();
            }// try
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in TranscribedText setter: {ex.Message}");
            }// catch
        }// set
    }// TranscribedText

    public ICommand StartConversationCommand { get; }
    public ICommand StopConversationCommand { get; }
    public ICommand StartListeningCommand { get; }
    public ICommand StopListeningCommand { get; }
    public ICommand ProcessSpeechCommand { get; }

    /// <summary>
    /// Constructor for MainViewModel
    /// </summary>
    public MainViewModel(
        ISpeechRecognitionService speechRecognitionService,
        ICommunicationService communicationService)
    {
        try
        {
            _speechRecognitionService = speechRecognitionService ?? throw new ArgumentNullException(nameof(speechRecognitionService));
            _communicationService = communicationService ?? throw new ArgumentNullException(nameof(communicationService));

            Messages = new ObservableCollection<Message>();
            _statusMessage = "Ready to start conversation";
            _isListening = false;
            _transcribedText = string.Empty;

            StartConversationCommand = new Command(async () => await StartConversationAsync());
            StopConversationCommand = new Command(async () => await StopConversationAsync());
            StartListeningCommand = new Command(async () => await StartListeningAsync());
            StopListeningCommand = new Command(async () => await StopListeningAsync());
            ProcessSpeechCommand = new Command(async () => await ProcessSpeechAsync());

            _speechRecognitionService.SpeechRecognized += OnSpeechRecognized;
        }// try
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error in MainViewModel constructor: {ex.Message}");
            throw;
        }// catch
    }// MainViewModel constructor

    /// <summary>
    /// Starts a new conversation
    /// </summary>
    private async Task StartConversationAsync()
    {
        try
        {
            _currentConversation = await _communicationService.StartConversationAsync();
            StatusMessage = "Conversation started";
            Messages.Clear();
        }// try
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error in StartConversationAsync: {ex.Message}");
            StatusMessage = $"Error starting conversation: {ex.Message}";
        }// catch
    }// StartConversationAsync

    /// <summary>
    /// Stops the current conversation
    /// </summary>
    private async Task StopConversationAsync()
    {
        try
        {
            if (_currentConversation != null)
            {
                await _communicationService.EndConversationAsync(_currentConversation);
                _currentConversation = null;
                StatusMessage = "Conversation ended";
            }// if conversation exists
        }// try
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error in StopConversationAsync: {ex.Message}");
            StatusMessage = $"Error stopping conversation: {ex.Message}";
        }// catch
    }// StopConversationAsync

    /// <summary>
    /// Starts listening for speech
    /// </summary>
    private async Task StartListeningAsync()
    {
        try
        {
            await _speechRecognitionService.StartListeningAsync();
            IsListening = true;
            StatusMessage = "Listening...";
        }// try
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error in StartListeningAsync: {ex.Message}");
            StatusMessage = $"Error starting listening: {ex.Message}";
        }// catch
    }// StartListeningAsync

    /// <summary>
    /// Stops listening for speech
    /// </summary>
    private async Task StopListeningAsync()
    {
        try
        {
            await _speechRecognitionService.StopListeningAsync();
            IsListening = false;
            StatusMessage = "Stopped listening";
        }// try
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error in StopListeningAsync: {ex.Message}");
            StatusMessage = $"Error stopping listening: {ex.Message}";
        }// catch
    }// StopListeningAsync

    /// <summary>
    /// Processes the transcribed speech
    /// </summary>
    private async Task ProcessSpeechAsync()
    {
        try
        {
            if (_currentConversation == null)
            {
                StatusMessage = "Please start a conversation first";
                return;
            }// if no conversation

            if (string.IsNullOrWhiteSpace(TranscribedText))
            {
                StatusMessage = "No speech to process";
                return;
            }// if no text

            StatusMessage = "Processing speech...";
            
            var board = await _communicationService.ProcessSpeechAsync(TranscribedText, _currentConversation);
            
            // Update messages
            Messages.Clear();
            foreach (var message in _currentConversation.Messages)
            {
                Messages.Add(message);
            }// foreach message

            StatusMessage = $"Generated {board.Candidates.Count} response candidates";

            // Navigate to communication board (would be implemented with navigation service)
            // For now, just update status
        }// try
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error in ProcessSpeechAsync: {ex.Message}");
            StatusMessage = $"Error processing speech: {ex.Message}";
        }// catch
    }// ProcessSpeechAsync

    /// <summary>
    /// Handles speech recognized event
    /// </summary>
    private void OnSpeechRecognized(object? sender, string text)
    {
        try
        {
            TranscribedText = text;
            StatusMessage = $"Recognized: {text}";
        }// try
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error in OnSpeechRecognized: {ex.Message}");
        }// catch
    }// OnSpeechRecognized

    /// <summary>
    /// Raises property changed event
    /// </summary>
    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        try
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }// try
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error in OnPropertyChanged: {ex.Message}");
        }// catch
    }// OnPropertyChanged
}// MainViewModel class
