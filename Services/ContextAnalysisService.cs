using NewDawn.Models;

namespace NewDawn.Services;

/// <summary>
/// Service for analyzing conversation context and determining intent
/// </summary>
public class ContextAnalysisService : IContextAnalysisService
{
    public ContextAnalysisService()
    {
        try
        {
            // Initialize any required resources
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error in ContextAnalysisService constructor: {ex.Message}");
            throw;
        }
    }

    public async Task<string> AnalyzeContextAsync(Conversation conversation)
    {
        try
        {
            if (conversation == null)
            {
                throw new ArgumentNullException(nameof(conversation));
            }

            return await Task.Run(() =>
            {
                var recentMessages = conversation.GetRecentMessages(5).ToList();
                if (!recentMessages.Any())
                {
                    return "general";
                }

                var lastMessage = recentMessages.Last();
                var context = DetermineContextFromMessage(lastMessage.Text);

                return context;
            });
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error in AnalyzeContextAsync: {ex.Message}");
            throw;
        }
    }

    public async Task<string> DetermineIntentAsync(string message)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                return "unknown";
            }

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
                }

                // Greeting detection
                if (message.Contains("hello") || message.Contains("hi ") || 
                    message.Contains("hey") || message.Contains("good morning") ||
                    message.Contains("good afternoon") || message.Contains("good evening"))
                {
                    return "greeting";
                }

                // Request detection
                if (message.Contains("please") || message.Contains("can you") ||
                    message.Contains("could you") || message.Contains("would you"))
                {
                    return "request";
                }

                // Emergency detection
                if (message.Contains("help") || message.Contains("emergency") ||
                    message.Contains("urgent") || message.Contains("pain"))
                {
                    return "emergency";
                }

                return "statement";
            });
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error in DetermineIntentAsync: {ex.Message}");
            throw;
        }
    }

    public async Task<double> GetConfidenceScoreAsync(string message, string intent)
    {
        try
        {
            // Simulate confidence scoring
            return await Task.FromResult(0.85);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error in GetConfidenceScoreAsync: {ex.Message}");
            throw;
        }
    }

    private string DetermineContextFromMessage(string message)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                return "general";
            }

            message = message.ToLower();

            if (message.Contains("feel") || message.Contains("feeling") || 
                message.Contains("emotion") || message.Contains("happy") ||
                message.Contains("sad") || message.Contains("tired"))
            {
                return "emotional";
            }

            if (message.Contains("eat") || message.Contains("drink") || 
                message.Contains("hungry") || message.Contains("thirsty") ||
                message.Contains("food") || message.Contains("meal"))
            {
                return "needs";
            }

            if (message.Contains("help") || message.Contains("emergency") ||
                message.Contains("urgent") || message.Contains("pain"))
            {
                return "emergency";
            }

            return "general";
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error in DetermineContextFromMessage: {ex.Message}");
            throw;
        }
    }
}
