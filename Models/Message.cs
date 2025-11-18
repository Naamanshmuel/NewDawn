namespace NewDawn.Models;

/// <summary>
/// Represents a message in a conversation
/// </summary>
public class Message
{
    public string Id { get; set; }
    public string Text { get; set; }
    public DateTime Timestamp { get; set; }
    public MessageSender Sender { get; set; }
    public string? Intent { get; set; }
    public double ConfidenceScore { get; set; }

    public Message()
    {
        try
        {
            Id = Guid.NewGuid().ToString();
            Timestamp = DateTime.UtcNow;
            Text = string.Empty;
            ConfidenceScore = 0.0;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error in Message constructor: {ex.Message}");
            throw;
        }
    }

    public Message(string text, MessageSender sender) : this()
    {
        try
        {
            Text = text;
            Sender = sender;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error in Message constructor: {ex.Message}");
            throw;
        }
    }
}

public enum MessageSender
{
    ConversationPartner,
    ALSUser,
    System
}
