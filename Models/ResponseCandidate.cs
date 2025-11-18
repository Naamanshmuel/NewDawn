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

    /// <summary>
    /// Default constructor for ResponseCandidate
    /// </summary>
    public ResponseCandidate()
    {
        try
        {
            Id = Guid.NewGuid().ToString();
            Text = string.Empty;
            ConfidenceScore = 0.0;
            Priority = 0;
            IsSelected = false;
        }// try
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error in ResponseCandidate constructor: {ex.Message}");
            throw;
        }// catch
    }// ResponseCandidate constructor

    /// <summary>
    /// Constructor for ResponseCandidate with text, category, and confidence score
    /// </summary>
    public ResponseCandidate(string text, ResponseCategory category, double confidenceScore) : this()
    {
        try
        {
            Text = text;
            Category = category;
            ConfidenceScore = confidenceScore;
        }// try
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error in ResponseCandidate constructor: {ex.Message}");
            throw;
        }// catch
    }// ResponseCandidate constructor

    /// <summary>
    /// Marks this response candidate as selected
    /// </summary>
    public void Select()
    {
        try
        {
            IsSelected = true;
        }// try
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error in Select: {ex.Message}");
            throw;
        }// catch
    }// Select

    /// <summary>
    /// Marks this response candidate as not selected
    /// </summary>
    public void Deselect()
    {
        try
        {
            IsSelected = false;
        }// try
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error in Deselect: {ex.Message}");
            throw;
        }// catch
    }// Deselect
}// ResponseCandidate class

public enum ResponseCategory
{
    Affirmative,
    Negative,
    Question,
    Statement,
    Emergency,
    Emotion
}// ResponseCategory enum
