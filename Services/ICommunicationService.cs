using NewDawn.Models;

namespace NewDawn.Services;

/// <summary>
/// Interface for the main communication service that orchestrates all operations
/// </summary>
public interface ICommunicationService
{
    /// <summary>
    /// Start a new conversation session
    /// </summary>
    Task<Conversation> StartConversationAsync();

    /// <summary>
    /// Process incoming speech and generate responses
    /// </summary>
    Task<CommunicationBoard> ProcessSpeechAsync(string transcribedText, Conversation conversation);

    /// <summary>
    /// Select a response from the communication board
    /// </summary>
    Task SelectResponseAsync(ResponseCandidate response, Conversation conversation);

    /// <summary>
    /// End the current conversation
    /// </summary>
    Task EndConversationAsync(Conversation conversation);

    /// <summary>
    /// Get the current conversation
    /// </summary>
    Conversation? CurrentConversation { get; }
}
