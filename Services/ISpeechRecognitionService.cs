namespace NewDawn.Services;

/// <summary>
/// Interface for speech recognition service
/// </summary>
public interface ISpeechRecognitionService
{
    /// <summary>
    /// Start listening to the conversation partner
    /// </summary>
    Task StartListeningAsync();

    /// <summary>
    /// Stop listening to the conversation partner
    /// </summary>
    Task StopListeningAsync();

    /// <summary>
    /// Get the transcribed text
    /// </summary>
    Task<string> GetTranscriptionAsync();

    /// <summary>
    /// Event fired when speech is recognized
    /// </summary>
    event EventHandler<string>? SpeechRecognized;

    /// <summary>
    /// Check if the service is currently listening
    /// </summary>
    bool IsListening { get; }
}
