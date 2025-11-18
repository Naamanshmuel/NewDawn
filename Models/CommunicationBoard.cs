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

    public CommunicationBoard()
    {
        try
        {
            Id = Guid.NewGuid().ToString();
            Candidates = new ObservableCollection<ResponseCandidate>();
            Rows = 3;
            Columns = 3;
            LastUpdated = DateTime.UtcNow;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error in CommunicationBoard constructor: {ex.Message}");
            throw;
        }
    }

    public CommunicationBoard(int rows, int columns) : this()
    {
        try
        {
            Rows = rows;
            Columns = columns;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error in CommunicationBoard constructor: {ex.Message}");
            throw;
        }
    }

    public void UpdateCandidates(IEnumerable<ResponseCandidate> newCandidates)
    {
        try
        {
            if (newCandidates == null)
            {
                throw new ArgumentNullException(nameof(newCandidates));
            }

            Candidates.Clear();
            foreach (var candidate in newCandidates)
            {
                Candidates.Add(candidate);
            }
            LastUpdated = DateTime.UtcNow;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error in UpdateCandidates: {ex.Message}");
            throw;
        }
    }

    public void ClearCandidates()
    {
        try
        {
            Candidates.Clear();
            LastUpdated = DateTime.UtcNow;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error in ClearCandidates: {ex.Message}");
            throw;
        }
    }

    public ResponseCandidate? GetSelectedCandidate()
    {
        try
        {
            return Candidates.FirstOrDefault(c => c.IsSelected);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error in GetSelectedCandidate: {ex.Message}");
            throw;
        }
    }
}
