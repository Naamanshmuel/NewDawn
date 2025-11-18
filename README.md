# NewDawn - ALS Communication System

A modern communication system designed to help ALS users communicate effectively through an eye-tracking friendly interface.

## Overview

NewDawn is a comprehensive communication system built with .NET 8 and MAUI that helps ALS (Amyotrophic Lateral Sclerosis) users communicate by:

- **Listening** to conversation partners through speech recognition
- **Transcribing** speech to text in real-time
- **Understanding** the context and intent of conversations
- **Generating** intelligent candidate responses
- **Displaying** responses on an eye-tracking friendly communication board

## Technology Stack

- **Framework**: .NET 8
- **UI**: .NET MAUI (Multi-platform App UI)
- **Architecture**: Object-Oriented Design with MVVM pattern
- **Platforms**: Windows, iOS/iPadOS, Android
- **Async/Await**: Full async support for responsive UI
- **Dependency Injection**: Microsoft.Extensions.DependencyInjection
- **Error Handling**: Comprehensive try-catch blocks in all methods

## Architecture

### Core Components

#### Models
- **Message**: Represents a message in a conversation
- **ResponseCandidate**: Represents a suggested response for the user
- **Conversation**: Manages conversation state and history
- **CommunicationBoard**: Eye-tracking friendly grid of response options

#### Services
- **ISpeechRecognitionService**: Handles speech-to-text transcription
- **IContextAnalysisService**: Analyzes conversation context and determines intent
- **IResponseGenerationService**: Generates contextually appropriate responses
- **ICommunicationService**: Orchestrates all communication operations

#### ViewModels
- **MainViewModel**: Main application logic and conversation management
- **CommunicationBoardViewModel**: Communication board interaction logic

#### Views
- **MainPage**: Primary interface for conversation management
- **CommunicationBoardPage**: Eye-tracking friendly response selection interface

## Features

### 1. Speech Recognition
- Real-time speech-to-text transcription
- Continuous listening mode
- Platform-specific speech APIs integration

### 2. Context Analysis
- Intent detection (question, statement, request, emergency)
- Context-aware response generation
- Confidence scoring for predictions

### 3. Response Generation
Categories:
- **Affirmative**: Yes, Okay, I agree
- **Negative**: No, Not really
- **Question**: Can you repeat that?, Tell me more
- **Statement**: Thank you, I understand
- **Emergency**: Help, Medical emergency, Call 911
- **Emotion**: I'm happy, I'm tired, I feel better

### 4. Eye-Tracking Friendly UI
- Large, high-contrast buttons
- 3x3 grid layout optimized for eye-tracking
- Color-coded response categories
- Emergency responses highlighted in red
- Affirmative responses in green
- Questions and other categories clearly distinguished

### 5. Safety Features
- Emergency response shortcuts
- High-confidence emergency detection
- Immediate access to critical phrases

## Project Structure

```
NewDawn/
├── Models/                          # Data models
│   ├── Message.cs
│   ├── ResponseCandidate.cs
│   ├── Conversation.cs
│   └── CommunicationBoard.cs
├── Services/                        # Business logic
│   ├── ISpeechRecognitionService.cs
│   ├── SpeechRecognitionService.cs
│   ├── IContextAnalysisService.cs
│   ├── ContextAnalysisService.cs
│   ├── IResponseGenerationService.cs
│   ├── ResponseGenerationService.cs
│   ├── ICommunicationService.cs
│   └── CommunicationService.cs
├── ViewModels/                      # MVVM ViewModels
│   ├── MainViewModel.cs
│   └── CommunicationBoardViewModel.cs
├── Views/                           # XAML Views
│   ├── MainPage.xaml
│   ├── MainPage.xaml.cs
│   ├── CommunicationBoardPage.xaml
│   └── CommunicationBoardPage.xaml.cs
├── Platforms/                       # Platform-specific code
│   ├── Android/
│   ├── iOS/
│   └── Windows/
├── Resources/                       # App resources
│   ├── Styles/
│   ├── Fonts/
│   ├── Images/
│   └── AppIcon/
├── App.xaml                         # Application definition
├── AppShell.xaml                    # Shell navigation
├── MauiProgram.cs                   # DI configuration
└── NewDawn.csproj                   # Project file
```

## Getting Started

### Prerequisites
- .NET 8 SDK
- MAUI workload: `dotnet workload install maui`
- Visual Studio 2022 (Windows) or Visual Studio for Mac
- For Windows: Windows 10/11 with Windows App SDK
- For iOS: Xcode on macOS
- For Android: Android SDK

### Building the Project

```bash
# Restore dependencies
dotnet restore

# Build for specific platform
dotnet build -f net8.0-android
dotnet build -f net8.0-ios
dotnet build -f net8.0-windows10.0.19041.0
```

### Running the Demo

A console demo application is provided to demonstrate the architecture and functionality:

```bash
cd NewDawn.Demo
dotnet run
```

This will demonstrate:
1. Starting a conversation
2. Processing speech input
3. Generating contextual responses
4. Selecting responses
5. Displaying conversation history
6. Emergency response system

## Design Patterns

### Object-Oriented Design
- **Encapsulation**: Services and models have clear responsibilities
- **Abstraction**: Interface-based design for testability
- **Inheritance**: Base classes for common functionality
- **Polymorphism**: Service implementations can be swapped

### MVVM Pattern
- **Model**: Data and business logic
- **View**: XAML-based UI
- **ViewModel**: Presentation logic and data binding

### Dependency Injection
All services are registered in `MauiProgram.cs`:
```csharp
builder.Services.AddSingleton<ISpeechRecognitionService, SpeechRecognitionService>();
builder.Services.AddSingleton<IContextAnalysisService, ContextAnalysisService>();
builder.Services.AddSingleton<IResponseGenerationService, ResponseGenerationService>();
builder.Services.AddSingleton<ICommunicationService, CommunicationService>();
```

### Error Handling
Every method includes try-catch blocks:
```csharp
public async Task<string> MethodAsync()
{
    try
    {
        // Method implementation
    }
    catch (Exception ex)
    {
        System.Diagnostics.Debug.WriteLine($"Error in MethodAsync: {ex.Message}");
        throw;
    }
}
```

### Async/Await
All I/O operations use async/await for responsive UI:
```csharp
public async Task<CommunicationBoard> ProcessSpeechAsync(string text, Conversation conversation)
{
    var intent = await _contextAnalysisService.DetermineIntentAsync(text);
    var responses = await _responseGenerationService.GenerateResponsesAsync(conversation, intent);
    // ...
}
```

## Future Enhancements

- Integration with platform-specific speech recognition APIs
- Machine learning for improved response predictions
- Customizable response templates
- User profile management
- Cloud sync for conversation history
- Multi-language support
- Voice output for selected responses
- Accessibility features for different types of eye-tracking hardware

## Contributing

This is a specialized accessibility project. Contributions should focus on:
- Improving eye-tracking UI responsiveness
- Enhancing context analysis accuracy
- Adding new response categories
- Platform-specific optimizations
- Accessibility improvements

## License

See LICENSE file for details.

## Support

For questions or support, please open an issue in the GitHub repository.

---

**Note**: This system is designed as an assistive technology for ALS patients. Always consult with healthcare professionals and assistive technology specialists when implementing communication systems for medical use. 
