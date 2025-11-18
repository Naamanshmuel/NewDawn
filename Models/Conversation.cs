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

    /// <summary>
    /// Default constructor for Conversation
    /// </summary>
    public Conversation()
    {
        try
        {
            Id = Guid.NewGuid().ToString();
            StartTime = DateTime.UtcNow;
            Messages = new ObservableCollection<Message>();
        }// try
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error in Conversation constructor: {ex.Message}");
            throw;
        }// catch
    }// Conversation constructor

    /// <summary>
    /// Adds a message to the conversation
    /// </summary>
    public void AddMessage(Message message)
    {
        try
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }// if null check

            Messages.Add(message);
        }// try
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error in AddMessage: {ex.Message}");
            throw;
        }// catch
    }// AddMessage

    /// <summary>
    /// Gets the last message in the conversation
    /// </summary>
    public Message? GetLastMessage()
    {
        try
        {
            return Messages.LastOrDefault();
        }// try
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error in GetLastMessage: {ex.Message}");
            throw;
        }// catch
    }// GetLastMessage

    /// <summary>
    /// Gets the most recent messages in the conversation
    /// </summary>
    public IEnumerable<Message> GetRecentMessages(int count)
    {
        try
        {
            return Messages.TakeLast(count);
        }// try
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error in GetRecentMessages: {ex.Message}");
            throw;
        }// catch
    }// GetRecentMessages

    /// <summary>
    /// Ends the conversation
    /// </summary>
    public void EndConversation()
    {
        try
        {
            EndTime = DateTime.UtcNow;
        }// try
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error in EndConversation: {ex.Message}");
            throw;
        }// catch
    }// EndConversation
}// Conversation class
