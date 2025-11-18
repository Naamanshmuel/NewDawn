using NewDawn.Models;

namespace NewDawn.Services;

/// <summary>
/// Service for analyzing conversation context and determining intent
/// </summary>
public class ContextAnalysisService : IContextAnalysisService
{
    /// <summary>
    /// Constructor for ContextAnalysisService
    /// </summary>
    public ContextAnalysisService()
    {
        try
        {
            // Initialize any required resources
        }// try
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error in ContextAnalysisService constructor: {ex.Message}");
            throw;
        }// catch
    }// ContextAnalysisService constructor

    /// <summary>
    /// Analyzes the context of a conversation
    /// </summary>
    public async Task<string> AnalyzeContextAsync(Conversation conversation)
    {
        try
        {
            if (conversation == null)
            {
                throw new ArgumentNullException(nameof(conversation));
            }// if conversation null

            return await Task.Run(() =>
            {
                var recentMessages = conversation.GetRecentMessages(5).ToList();
                if (!recentMessages.Any())
                {
                    return "general";
                }// if no messages

                var lastMessage = recentMessages.Last();
                var context = DetermineContextFromMessage(lastMessage.Text);

                return context;
            });
        }// try
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error in AnalyzeContextAsync: {ex.Message}");
            throw;
        }// catch
    }// AnalyzeContextAsync

    /// <summary>
    /// Determines the intent from a message
    /// </summary>
    public async Task<string> DetermineIntentAsync(string message)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                return "unknown";
            }// if message empty

            return await Task.Run(() =>
            {
                message = message.ToLower().Trim();

                // Question detection
                if (message.EndsWith("?") || message.StartsWith("what") || 
                    message.StartsWith("how") || message.StartsWith("where") ||
                    message.StartsWith("when") || message.StartsWith("who") ||
                    message.StartsWith("why") || message.StartsWith("can you") ||
                    message.StartsWith("could you") || message.StartsWith("would you") ||
                    message.StartsWith("do you") || message.StartsWith("are you"))
                {
                    return "question";
                }// if question

                // Greeting detection
                if (message.Contains("hello") || message.Contains("hi ") || 
                    message.Contains("hey") || message.Contains("good morning") ||
                    message.Contains("good afternoon") || message.Contains("good evening"))
                {
                    return "greeting";
                }// if greeting

                // Request detection
                if (message.Contains("please") || message.Contains("can you") ||
                    message.Contains("could you") || message.Contains("would you"))
                {
                    return "request";
                }// if request

                // Emergency detection
                if (message.Contains("help") || message.Contains("emergency") ||
                    message.Contains("urgent") || message.Contains("pain"))
                {
                    return "emergency";
                }// if emergency

                return "statement";
            });
        }// try
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error in DetermineIntentAsync: {ex.Message}");
            throw;
        }// catch
    }// DetermineIntentAsync

    /// <summary>
    /// Gets the confidence score for a message and intent
    /// </summary>
    public async Task<double> GetConfidenceScoreAsync(string message, string intent)
    {
        try
        {
            // Simulate confidence scoring
            return await Task.FromResult(0.85);
        }// try
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error in GetConfidenceScoreAsync: {ex.Message}");
            throw;
        }// catch
    }// GetConfidenceScoreAsync

    /// <summary>
    /// Determines the context from a message
    /// </summary>
    private string DetermineContextFromMessage(string message)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                return "general";
            }// if message empty

            message = message.ToLower();

            if (message.Contains("feel") || message.Contains("feeling") || 
                message.Contains("emotion") || message.Contains("happy") ||
                message.Contains("sad") || message.Contains("tired"))
            {
                return "emotional";
            }// if emotional

            if (message.Contains("eat") || message.Contains("drink") || 
                message.Contains("hungry") || message.Contains("thirsty") ||
                message.Contains("food") || message.Contains("meal"))
            {
                return "needs";
            }// if needs

            if (message.Contains("help") || message.Contains("emergency") ||
                message.Contains("urgent") || message.Contains("pain"))
            {
                return "emergency";
            }// if emergency

            return "general";
        }// try
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error in DetermineContextFromMessage: {ex.Message}");
            throw;
        }// catch
    }// DetermineContextFromMessage
}// ContextAnalysisService class
