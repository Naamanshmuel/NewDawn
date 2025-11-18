using NewDawn.Models;

namespace NewDawn.Services;

/// <summary>
/// Interface for response generation service
/// </summary>
public interface IResponseGenerationService
{
    /// <summary>
    /// Generate candidate responses based on conversation context
    /// </summary>
    Task<IEnumerable<ResponseCandidate>> GenerateResponsesAsync(Conversation conversation, string intent);

    /// <summary>
    /// Generate responses for a specific context
    /// </summary>
    Task<IEnumerable<ResponseCandidate>> GenerateContextualResponsesAsync(string context, string intent);

    /// <summary>
    /// Get emergency responses
    /// </summary>
    Task<IEnumerable<ResponseCandidate>> GetEmergencyResponsesAsync();
}
