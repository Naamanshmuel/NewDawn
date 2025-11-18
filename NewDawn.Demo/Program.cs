using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace NewDawn.Demo;

/// <summary>
/// Demo application showing the ALS Communication System architecture
/// This demonstrates all the OOP design patterns, async/await, DI, and error handling
/// </summary>
class Program
{
    static async Task Main(string[] args)
    {
        try
        {
            Console.WriteLine("=".PadRight(80, '='));
            Console.WriteLine("NewDawn ALS Communication System - Demo");
            Console.WriteLine("=".PadRight(80, '='));
            Console.WriteLine();

            // Setup Dependency Injection
            var serviceProvider = ConfigureServices();

            // Get the communication service
            var communicationService = serviceProvider.GetRequiredService<ICommunicationService>();
            var speechService = serviceProvider.GetRequiredService<ISpeechRecognitionService>();

            // Start a conversation
            Console.WriteLine("[1] Starting conversation...");
            var conversation = await communicationService.StartConversationAsync();
            Console.WriteLine($"    Conversation started with ID: {conversation.Id}");
            Console.WriteLine();

            // Simulate speech recognition
            Console.WriteLine("[2] Simulating speech recognition...");
            var testPhrases = new[]
            {
                "How are you feeling today?",
                "Would you like some water?",
                "Do you need help with anything?"
            };

            foreach (var phrase in testPhrases)
            {
                Console.WriteLine($"    Partner says: \"{phrase}\"");
                
                // Process the speech
                var board = await communicationService.ProcessSpeechAsync(phrase, conversation);
                
                Console.WriteLine($"    Generated {board.Candidates.Count} response candidates:");
                
                // Display top 5 responses
                var topResponses = board.Candidates.Take(5).ToList();
                for (int i = 0; i < topResponses.Count; i++)
                {
                    var candidate = topResponses[i];
                    Console.WriteLine($"      {i + 1}. \"{candidate.Text}\" " +
                                    $"(Category: {candidate.Category}, Confidence: {candidate.ConfidenceScore:F2})");
                }
                
                // Simulate selecting first response
                var selectedResponse = topResponses.First();
                await communicationService.SelectResponseAsync(selectedResponse, conversation);
                Console.WriteLine($"    >>> User selected: \"{selectedResponse.Text}\"");
                Console.WriteLine();
            }

            // Show conversation history
            Console.WriteLine("[3] Conversation History:");
            foreach (var message in conversation.Messages)
            {
                var sender = message.Sender == MessageSender.ConversationPartner ? "Partner" : "User";
                Console.WriteLine($"    [{message.Timestamp:HH:mm:ss}] {sender}: {message.Text}");
            }
            Console.WriteLine();

            // End conversation
            Console.WriteLine("[4] Ending conversation...");
            await communicationService.EndConversationAsync(conversation);
            Console.WriteLine($"    Conversation ended. Duration: {(conversation.EndTime - conversation.StartTime)?.TotalSeconds:F1} seconds");
            Console.WriteLine();

            // Demonstrate emergency responses
            Console.WriteLine("[5] Demonstrating emergency response system...");
            var responseService = serviceProvider.GetRequiredService<IResponseGenerationService>();
            var emergencyResponses = await responseService.GetEmergencyResponsesAsync();
            
            Console.WriteLine("    Emergency responses available:");
            foreach (var response in emergencyResponses.Take(5))
            {
                Console.WriteLine($"      - \"{response.Text}\" (Confidence: {response.ConfidenceScore:F2})");
            }
            Console.WriteLine();

            Console.WriteLine("=".PadRight(80, '='));
            Console.WriteLine("Demo completed successfully!");
            Console.WriteLine("=".PadRight(80, '='));
        }
        catch (Exception ex)
        {
            Console.WriteLine($"ERROR: {ex.Message}");
            Console.WriteLine($"Stack trace: {ex.StackTrace}");
        }
    }

    private static ServiceProvider ConfigureServices()
    {
        try
        {
            var services = new ServiceCollection();

            // Register logging
            services.AddLogging(configure =>
            {
                configure.AddConsole();
                configure.SetMinimumLevel(LogLevel.Warning);
            });

            // Register Services
            services.AddSingleton<ISpeechRecognitionService, SpeechRecognitionService>();
            services.AddSingleton<IContextAnalysisService, ContextAnalysisService>();
            services.AddSingleton<IResponseGenerationService, ResponseGenerationService>();
            services.AddSingleton<ICommunicationService, CommunicationService>();

            return services.BuildServiceProvider();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in ConfigureServices: {ex.Message}");
            throw;
        }
    }
}

// Include simplified versions of the models and services for the demo

#region Models

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
            Console.WriteLine($"Error in Message constructor: {ex.Message}");
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
            Console.WriteLine($"Error in Message constructor: {ex.Message}");
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
            Console.WriteLine($"Error in ResponseCandidate constructor: {ex.Message}");
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
            Console.WriteLine($"Error in ResponseCandidate constructor: {ex.Message}");
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
            Console.WriteLine($"Error in Select: {ex.Message}");
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

public class Conversation
{
    public string Id { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public List<Message> Messages { get; set; }
    public string? CurrentContext { get; set; }
    public string? CurrentIntent { get; set; }

    public Conversation()
    {
        try
        {
            Id = Guid.NewGuid().ToString();
            StartTime = DateTime.UtcNow;
            Messages = new List<Message>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in Conversation constructor: {ex.Message}");
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
            Console.WriteLine($"Error in AddMessage: {ex.Message}");
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
            Console.WriteLine($"Error in EndConversation: {ex.Message}");
            throw;
        }
    }
}

public class CommunicationBoard
{
    public string Id { get; set; }
    public List<ResponseCandidate> Candidates { get; set; }
    public int Rows { get; set; }
    public int Columns { get; set; }

    public CommunicationBoard()
    {
        try
        {
            Id = Guid.NewGuid().ToString();
            Candidates = new List<ResponseCandidate>();
            Rows = 3;
            Columns = 3;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in CommunicationBoard constructor: {ex.Message}");
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
            Candidates.AddRange(newCandidates);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in UpdateCandidates: {ex.Message}");
            throw;
        }
    }
}

#endregion

#region Services

public interface ISpeechRecognitionService
{
    Task StartListeningAsync();
    Task StopListeningAsync();
    Task<string> GetTranscriptionAsync();
    bool IsListening { get; }
}

public class SpeechRecognitionService : ISpeechRecognitionService
{
    private bool _isListening;
    public bool IsListening => _isListening;

    public async Task StartListeningAsync()
    {
        try
        {
            _isListening = true;
            await Task.CompletedTask;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in StartListeningAsync: {ex.Message}");
            throw;
        }
    }

    public async Task StopListeningAsync()
    {
        try
        {
            _isListening = false;
            await Task.CompletedTask;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in StopListeningAsync: {ex.Message}");
            throw;
        }
    }

    public async Task<string> GetTranscriptionAsync()
    {
        try
        {
            return await Task.FromResult(string.Empty);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in GetTranscriptionAsync: {ex.Message}");
            throw;
        }
    }
}

public interface IContextAnalysisService
{
    Task<string> AnalyzeContextAsync(Conversation conversation);
    Task<string> DetermineIntentAsync(string message);
    Task<double> GetConfidenceScoreAsync(string message, string intent);
}

public class ContextAnalysisService : IContextAnalysisService
{
    public async Task<string> AnalyzeContextAsync(Conversation conversation)
    {
        try
        {
            if (conversation == null)
            {
                throw new ArgumentNullException(nameof(conversation));
            }
            return await Task.FromResult("general");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in AnalyzeContextAsync: {ex.Message}");
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
                if (message.EndsWith("?") || message.StartsWith("how") || message.StartsWith("what"))
                {
                    return "question";
                }
                if (message.Contains("help") || message.Contains("emergency"))
                {
                    return "emergency";
                }
                return "statement";
            });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in DetermineIntentAsync: {ex.Message}");
            throw;
        }
    }

    public async Task<double> GetConfidenceScoreAsync(string message, string intent)
    {
        try
        {
            return await Task.FromResult(0.85);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in GetConfidenceScoreAsync: {ex.Message}");
            throw;
        }
    }
}

public interface IResponseGenerationService
{
    Task<IEnumerable<ResponseCandidate>> GenerateResponsesAsync(Conversation conversation, string intent);
    Task<IEnumerable<ResponseCandidate>> GetEmergencyResponsesAsync();
}

public class ResponseGenerationService : IResponseGenerationService
{
    public async Task<IEnumerable<ResponseCandidate>> GenerateResponsesAsync(Conversation conversation, string intent)
    {
        try
        {
            if (conversation == null)
            {
                throw new ArgumentNullException(nameof(conversation));
            }

            return await Task.Run(() =>
            {
                var responses = new List<ResponseCandidate>();
                switch (intent.ToLower())
                {
                    case "question":
                        responses.AddRange(new[]
                        {
                            new ResponseCandidate("Yes", ResponseCategory.Affirmative, 0.9),
                            new ResponseCandidate("No", ResponseCategory.Negative, 0.9),
                            new ResponseCandidate("Maybe", ResponseCategory.Statement, 0.7),
                            new ResponseCandidate("I don't know", ResponseCategory.Statement, 0.8),
                            new ResponseCandidate("Can you repeat that?", ResponseCategory.Question, 0.7),
                            new ResponseCandidate("Tell me more", ResponseCategory.Question, 0.7),
                            new ResponseCandidate("That sounds good", ResponseCategory.Affirmative, 0.8),
                            new ResponseCandidate("I'm not sure", ResponseCategory.Statement, 0.7),
                            new ResponseCandidate("I need time to think", ResponseCategory.Statement, 0.6)
                        });
                        break;
                    case "emergency":
                        responses.AddRange(GetEmergencyResponsesSync());
                        break;
                    default:
                        responses.AddRange(new[]
                        {
                            new ResponseCandidate("Yes", ResponseCategory.Affirmative, 0.8),
                            new ResponseCandidate("No", ResponseCategory.Negative, 0.8),
                            new ResponseCandidate("Okay", ResponseCategory.Affirmative, 0.8),
                            new ResponseCandidate("Thank you", ResponseCategory.Statement, 0.8),
                            new ResponseCandidate("Please", ResponseCategory.Statement, 0.7),
                            new ResponseCandidate("I understand", ResponseCategory.Statement, 0.8),
                            new ResponseCandidate("Tell me more", ResponseCategory.Question, 0.7),
                            new ResponseCandidate("I agree", ResponseCategory.Affirmative, 0.7),
                            new ResponseCandidate("Not really", ResponseCategory.Negative, 0.7)
                        });
                        break;
                }
                return responses;
            });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in GenerateResponsesAsync: {ex.Message}");
            throw;
        }
    }

    public async Task<IEnumerable<ResponseCandidate>> GetEmergencyResponsesAsync()
    {
        try
        {
            return await Task.FromResult(GetEmergencyResponsesSync());
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in GetEmergencyResponsesAsync: {ex.Message}");
            throw;
        }
    }

    private List<ResponseCandidate> GetEmergencyResponsesSync()
    {
        try
        {
            return new List<ResponseCandidate>
            {
                new ResponseCandidate("HELP - I NEED IMMEDIATE ASSISTANCE", ResponseCategory.Emergency, 1.0),
                new ResponseCandidate("EMERGENCY - CALL 911", ResponseCategory.Emergency, 1.0),
                new ResponseCandidate("I'M IN PAIN", ResponseCategory.Emergency, 0.95),
                new ResponseCandidate("I CAN'T BREATHE", ResponseCategory.Emergency, 1.0),
                new ResponseCandidate("GET HELP NOW", ResponseCategory.Emergency, 1.0),
                new ResponseCandidate("MEDICAL EMERGENCY", ResponseCategory.Emergency, 1.0),
                new ResponseCandidate("I NEED A DOCTOR", ResponseCategory.Emergency, 0.9),
                new ResponseCandidate("SOMETHING IS WRONG", ResponseCategory.Emergency, 0.9),
                new ResponseCandidate("URGENT ASSISTANCE NEEDED", ResponseCategory.Emergency, 0.95)
            };
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in GetEmergencyResponsesSync: {ex.Message}");
            throw;
        }
    }
}

public interface ICommunicationService
{
    Task<Conversation> StartConversationAsync();
    Task<CommunicationBoard> ProcessSpeechAsync(string transcribedText, Conversation conversation);
    Task SelectResponseAsync(ResponseCandidate response, Conversation conversation);
    Task EndConversationAsync(Conversation conversation);
    Conversation? CurrentConversation { get; }
}

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
            Console.WriteLine($"Error in CommunicationService constructor: {ex.Message}");
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
            Console.WriteLine($"Error in StartConversationAsync: {ex.Message}");
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

            var message = new Message(transcribedText, MessageSender.ConversationPartner);
            var intent = await _contextAnalysisService.DetermineIntentAsync(transcribedText);
            message.Intent = intent;
            message.ConfidenceScore = await _contextAnalysisService.GetConfidenceScoreAsync(transcribedText, intent);
            
            conversation.AddMessage(message);

            var context = await _contextAnalysisService.AnalyzeContextAsync(conversation);
            conversation.CurrentContext = context;
            conversation.CurrentIntent = intent;

            var responses = await _responseGenerationService.GenerateResponsesAsync(conversation, intent);
            var allResponses = responses.OrderByDescending(r => r.ConfidenceScore).Take(9).ToList();

            var board = new CommunicationBoard();
            board.UpdateCandidates(allResponses);

            return board;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in ProcessSpeechAsync: {ex.Message}");
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

            response.Select();
            var message = new Message(response.Text, MessageSender.ALSUser)
            {
                Intent = response.Category.ToString()
            };
            conversation.AddMessage(message);

            await Task.CompletedTask;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in SelectResponseAsync: {ex.Message}");
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
            Console.WriteLine($"Error in EndConversationAsync: {ex.Message}");
            throw;
        }
    }
}

#endregion
