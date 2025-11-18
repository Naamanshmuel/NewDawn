using System.Collections.ObjectModel;

namespace NewDawn.Models;

/// <summary>
/// Represents a conversation session
/// </summary>
public class Conversation
{
    public string Id { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public ObservableCollection<Message> Messages { get; set; }
    public string? CurrentContext { get; set; }
    public string? CurrentIntent { get; set; }

    public Conversation()
    {
        try
        {
            Id = Guid.NewGuid().ToString();
            StartTime = DateTime.UtcNow;
            Messages = new ObservableCollection<Message>();
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error in Conversation constructor: {ex.Message}");
            throw;
        }
    }

    public void AddMessage(Message message)
    {
        try
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            Messages.Add(message);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error in AddMessage: {ex.Message}");
            throw;
        }
    }

    public Message? GetLastMessage()
    {
        try
        {
            return Messages.LastOrDefault();
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error in GetLastMessage: {ex.Message}");
            throw;
        }
    }

    public IEnumerable<Message> GetRecentMessages(int count)
    {
        try
        {
            return Messages.TakeLast(count);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error in GetRecentMessages: {ex.Message}");
            throw;
        }
    }

    public void EndConversation()
    {
        try
        {
            EndTime = DateTime.UtcNow;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error in EndConversation: {ex.Message}");
            throw;
        }
    }
}
