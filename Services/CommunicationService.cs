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

    /// <summary>
    /// Constructor for CommunicationService
    /// </summary>
    public CommunicationService(
        IContextAnalysisService contextAnalysisService,
        IResponseGenerationService responseGenerationService)
    {
        try
        {
            _contextAnalysisService = contextAnalysisService ?? throw new ArgumentNullException(nameof(contextAnalysisService));
            _responseGenerationService = responseGenerationService ?? throw new ArgumentNullException(nameof(responseGenerationService));
        }// try
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error in CommunicationService constructor: {ex.Message}");
            throw;
        }// catch
    }// CommunicationService constructor

    /// <summary>
    /// Starts a new conversation
    /// </summary>
    public async Task<Conversation> StartConversationAsync()
    {
        try
        {
            _currentConversation = new Conversation();
            await Task.CompletedTask;
            return _currentConversation;
        }// try
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error in StartConversationAsync: {ex.Message}");
            throw;
        }// catch
    }// StartConversationAsync

    /// <summary>
    /// Processes transcribed speech and generates response candidates
    /// </summary>
    public async Task<CommunicationBoard> ProcessSpeechAsync(string transcribedText, Conversation conversation)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(transcribedText))
            {
                throw new ArgumentException("Transcribed text cannot be empty", nameof(transcribedText));
            }// if text empty

            if (conversation == null)
            {
                throw new ArgumentNullException(nameof(conversation));
            }// if conversation null

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
        }// try
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error in ProcessSpeechAsync: {ex.Message}");
            throw;
        }// catch
    }// ProcessSpeechAsync

    /// <summary>
    /// Handles selection of a response candidate
    /// </summary>
    public async Task SelectResponseAsync(ResponseCandidate response, Conversation conversation)
    {
        try
        {
            if (response == null)
            {
                throw new ArgumentNullException(nameof(response));
            }// if response null

            if (conversation == null)
            {
                throw new ArgumentNullException(nameof(conversation));
            }// if conversation null

            // Mark response as selected
            response.Select();

            // Add the selected response as a message from the ALS user
            var message = new Message(response.Text, MessageSender.ALSUser)
            {
                Intent = response.Category.ToString()
            };

            conversation.AddMessage(message);

            await Task.CompletedTask;
        }// try
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error in SelectResponseAsync: {ex.Message}");
            throw;
        }// catch
    }// SelectResponseAsync

    /// <summary>
    /// Ends the conversation
    /// </summary>
    public async Task EndConversationAsync(Conversation conversation)
    {
        try
        {
            if (conversation == null)
            {
                throw new ArgumentNullException(nameof(conversation));
            }// if conversation null

            conversation.EndConversation();

            if (_currentConversation?.Id == conversation.Id)
            {
                _currentConversation = null;
            }// if current conversation matches

            await Task.CompletedTask;
        }// try
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error in EndConversationAsync: {ex.Message}");
            throw;
        }// catch
    }// EndConversationAsync
}// CommunicationService class
