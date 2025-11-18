namespace NewDawn.Models;

/// <summary>
/// Represents a candidate response for the ALS user to select
/// </summary>
public class ResponseCandidate
{
    public string Id { get; set; }
    public string Text { get; set; }
    public double ConfidenceScore { get; set; }
    public ResponseCategory Category { get; set; }
    public int Priority { get; set; }
    public bool IsSelected { get; set; }

    public ResponseCandidate()
    {
        try
        {
            Id = Guid.NewGuid().ToString();
            Text = string.Empty;
            ConfidenceScore = 0.0;
            Priority = 0;
            IsSelected = false;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error in ResponseCandidate constructor: {ex.Message}");
            throw;
        }
    }

    public ResponseCandidate(string text, ResponseCategory category, double confidenceScore) : this()
    {
        try
        {
            Text = text;
            Category = category;
            ConfidenceScore = confidenceScore;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error in ResponseCandidate constructor: {ex.Message}");
            throw;
        }
    }

    public void Select()
    {
        try
        {
            IsSelected = true;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error in Select: {ex.Message}");
            throw;
        }
    }

    public void Deselect()
    {
        try
        {
            IsSelected = false;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error in Deselect: {ex.Message}");
            throw;
        }
    }
}

public enum ResponseCategory
{
    Affirmative,
    Negative,
    Question,
    Statement,
    Emergency,
    Emotion
}
