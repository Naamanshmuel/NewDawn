using NewDawn.Models;

namespace NewDawn.Services;

/// <summary>
/// Interface for context analysis service
/// </summary>
public interface IContextAnalysisService
{
    /// <summary>
    /// Analyze the conversation context and determine the intent
    /// </summary>
    Task<string> AnalyzeContextAsync(Conversation conversation);

    /// <summary>
    /// Determine the intent of a message
    /// </summary>
    Task<string> DetermineIntentAsync(string message);

    /// <summary>
    /// Get the confidence score for an intent
    /// </summary>
    Task<double> GetConfidenceScoreAsync(string message, string intent);
}
