using NewDawn.Models;

namespace NewDawn.Services;

/// <summary>
/// Main communication service that orchestrates all operations
/// </summary>
public class CommunicationService : ICommunicationService
{
    private readonly IContextAnalysisService _contextAnalysisService;
    private readonly IResponseGenerationService _responseGenerationService;
    private Conversation? _currentConversation;

    public Conversation? CurrentConversation => _currentConversation;

    public CommunicationService(
        IContextAnalysisService contextAnalysisService,
        IResponseGenerationService responseGenerationService)
    {
        try
        {
            _contextAnalysisService = contextAnalysisService ?? throw new ArgumentNullException(nameof(contextAnalysisService));
            _responseGenerationService = responseGenerationService ?? throw new ArgumentNullException(nameof(responseGenerationService));
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error in CommunicationService constructor: {ex.Message}");
            throw;
        }
    }

    public async Task<Conversation> StartConversationAsync()
    {
        try
        {
            _currentConversation = new Conversation();
            await Task.CompletedTask;
            return _currentConversation;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error in StartConversationAsync: {ex.Message}");
            throw;
        }
    }

    public async Task<CommunicationBoard> ProcessSpeechAsync(string transcribedText, Conversation conversation)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(transcribedText))
            {
                throw new ArgumentException("Transcribed text cannot be empty", nameof(transcribedText));
            }

            if (conversation == null)
            {
                throw new ArgumentNullException(nameof(conversation));
            }

            // Create and add the message to the conversation
            var message = new Message(transcribedText, MessageSender.ConversationPartner);
            
            // Determine intent
            var intent = await _contextAnalysisService.DetermineIntentAsync(transcribedText);
            message.Intent = intent;
            
            // Get confidence score
            message.ConfidenceScore = await _contextAnalysisService.GetConfidenceScoreAsync(transcribedText, intent);
            
            conversation.AddMessage(message);

            // Analyze context
            var context = await _contextAnalysisService.AnalyzeContextAsync(conversation);
            conversation.CurrentContext = context;
            conversation.CurrentIntent = intent;

            // Generate responses
            var responses = await _responseGenerationService.GenerateResponsesAsync(conversation, intent);
            
            // Also add contextual responses
            var contextualResponses = await _responseGenerationService.GenerateContextualResponsesAsync(context, intent);
            
            // Combine and prioritize responses
            var allResponses = responses.Concat(contextualResponses)
                .OrderByDescending(r => r.ConfidenceScore)
                .Take(9) // Take top 9 for a 3x3 grid
                .ToList();

            // Create communication board
            var board = new CommunicationBoard(3, 3);
            board.UpdateCandidates(allResponses);

            return board;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error in ProcessSpeechAsync: {ex.Message}");
            throw;
        }
    }

    public async Task SelectResponseAsync(ResponseCandidate response, Conversation conversation)
    {
        try
        {
            if (response == null)
            {
                throw new ArgumentNullException(nameof(response));
            }

            if (conversation == null)
            {
                throw new ArgumentNullException(nameof(conversation));
            }

            // Mark response as selected
            response.Select();

            // Add the selected response as a message from the ALS user
            var message = new Message(response.Text, MessageSender.ALSUser)
            {
                Intent = response.Category.ToString()
            };

            conversation.AddMessage(message);

            await Task.CompletedTask;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error in SelectResponseAsync: {ex.Message}");
            throw;
        }
    }

    public async Task EndConversationAsync(Conversation conversation)
    {
        try
        {
            if (conversation == null)
            {
                throw new ArgumentNullException(nameof(conversation));
            }

            conversation.EndConversation();

            if (_currentConversation?.Id == conversation.Id)
            {
                _currentConversation = null;
            }

            await Task.CompletedTask;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error in EndConversationAsync: {ex.Message}");
            throw;
        }
    }
}
