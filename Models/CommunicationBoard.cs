using System.Collections.ObjectModel;

namespace NewDawn.Models;

/// <summary>
/// Represents the eye-tracking friendly communication board
/// </summary>
public class CommunicationBoard
{
    public string Id { get; set; }
    public ObservableCollection<ResponseCandidate> Candidates { get; set; }
    public int Rows { get; set; }
    public int Columns { get; set; }
    public DateTime LastUpdated { get; set; }

    /// <summary>
    /// Default constructor for CommunicationBoard
    /// </summary>
    public CommunicationBoard()
    {
        try
        {
            Id = Guid.NewGuid().ToString();
            Candidates = new ObservableCollection<ResponseCandidate>();
            Rows = 3;
            Columns = 3;
            LastUpdated = DateTime.UtcNow;
        }// try
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error in CommunicationBoard constructor: {ex.Message}");
            throw;
        }// catch
    }// CommunicationBoard constructor

    /// <summary>
    /// Constructor for CommunicationBoard with rows and columns
    /// </summary>
    public CommunicationBoard(int rows, int columns) : this()
    {
        try
        {
            Rows = rows;
            Columns = columns;
        }// try
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error in CommunicationBoard constructor: {ex.Message}");
            throw;
        }// catch
    }// CommunicationBoard constructor

    /// <summary>
    /// Updates the candidates on the board
    /// </summary>
    public void UpdateCandidates(IEnumerable<ResponseCandidate> newCandidates)
    {
        try
        {
            if (newCandidates == null)
            {
                throw new ArgumentNullException(nameof(newCandidates));
            }// if null check

            Candidates.Clear();
            foreach (var candidate in newCandidates)
            {
                Candidates.Add(candidate);
            }// foreach candidate
            LastUpdated = DateTime.UtcNow;
        }// try
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error in UpdateCandidates: {ex.Message}");
            throw;
        }// catch
    }// UpdateCandidates

    /// <summary>
    /// Clears all candidates from the board
    /// </summary>
    public void ClearCandidates()
    {
        try
        {
            Candidates.Clear();
            LastUpdated = DateTime.UtcNow;
        }// try
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error in ClearCandidates: {ex.Message}");
            throw;
        }// catch
    }// ClearCandidates

    /// <summary>
    /// Gets the selected candidate
    /// </summary>
    public ResponseCandidate? GetSelectedCandidate()
    {
        try
        {
            return Candidates.FirstOrDefault(c => c.IsSelected);
        }// try
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error in GetSelectedCandidate: {ex.Message}");
            throw;
        }// catch
    }// GetSelectedCandidate
}// CommunicationBoard class
