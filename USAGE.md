# NewDawn Usage Guide

## Quick Start

### Running the Demo Application

The easiest way to see NewDawn in action is to run the demo console application:

```bash
cd NewDawn.Demo
dotnet run
```

This will demonstrate:
1. Starting a conversation
2. Processing speech from a conversation partner
3. Generating contextual responses
4. Selecting responses
5. Viewing conversation history
6. Emergency response system

## Building the MAUI Application

### Prerequisites

1. Install .NET 8 SDK:
   ```bash
   # Check if already installed
   dotnet --version
   
   # Download from: https://dotnet.microsoft.com/download/dotnet/8.0
   ```

2. Install MAUI workload:
   ```bash
   dotnet workload install maui
   ```

3. Platform-specific requirements:

   **Windows:**
   - Windows 10 version 1809 or higher
   - Visual Studio 2022 with MAUI workload

   **iOS/iPadOS:**
   - macOS with Xcode
   - Apple Developer account (for device deployment)

   **Android:**
   - Android SDK installed
   - Android device or emulator

### Build Commands

```bash
# Restore dependencies
dotnet restore

# Build for Android
dotnet build -f net8.0-android

# Build for iOS
dotnet build -f net8.0-ios

# Build for Windows
dotnet build -f net8.0-windows10.0.19041.0
```

### Running on Devices

```bash
# Run on Android device/emulator
dotnet run -f net8.0-android

# Run on iOS device/simulator
dotnet run -f net8.0-ios

# Run on Windows
dotnet run -f net8.0-windows10.0.19041.0
```

## Using the Application

### Main Page

The main page is the control center for the ALS communication system.

#### Starting a Conversation

1. Press **"Start Conversation"** button
2. Status message will update to "Conversation started"
3. The system is now ready to process speech

#### Speech Recognition

1. Press **"Start Listening"** to begin listening to the conversation partner
2. Speak clearly into the device microphone
3. The transcribed text will appear in the "Transcribed Text" section
4. Press **"Stop Listening"** when done

*Note: In the demo version, you'll need to simulate speech input. In production, platform-specific speech recognition will be used.*

#### Processing Speech

1. After transcribing speech, press **"Process Speech & Generate Responses"**
2. The system will:
   - Analyze the conversation context
   - Determine the speaker's intent
   - Generate appropriate response candidates
3. The communication board will be displayed with response options

#### Viewing Conversation History

The conversation history section shows all messages in the current conversation:
- **Partner messages**: What the conversation partner said
- **User messages**: The responses selected by the ALS user
- **Timestamps**: When each message was sent

#### Ending a Conversation

Press **"Stop Conversation"** to end the current session. The conversation will be closed and its end time recorded.

### Communication Board Page

The communication board is an eye-tracking friendly interface for selecting responses.

#### Layout

- **3x3 Grid**: 9 large buttons arranged in an easy-to-scan grid
- **Color Coding**:
  - 🔴 **Red**: Emergency responses
  - 🟢 **Green**: Affirmative responses (Yes, Okay, I agree)
  - 🟡 **Yellow**: Negative responses (No, Not really)
  - 🔵 **Blue**: Emotional responses
  - 🟣 **Purple**: Other categories (Questions, Statements)

#### Selecting a Response

1. Use eye-tracking, touch, or mouse to select a response
2. The selected button will:
   - Change color (highlighted border)
   - Be marked as selected
3. The response is automatically added to the conversation
4. Return to the main page to continue the conversation

#### Response Categories

**Affirmative**
- Yes
- Okay
- I agree
- That sounds good
- Yes, please

**Negative**
- No
- Not really
- No, thank you
- Not right now

**Questions**
- Can you repeat that?
- Tell me more
- How are you?
- What did you say?

**Statements**
- Thank you
- I understand
- Please
- I need time to think

**Emergency**
- HELP - I NEED IMMEDIATE ASSISTANCE
- EMERGENCY - CALL 911
- I'M IN PAIN
- I CAN'T BREATHE
- GET HELP NOW

**Emotional**
- I'm happy
- I'm tired
- I'm frustrated
- I feel better now
- I'm okay

## For Caregivers

### Setting Up for an ALS Patient

1. **Device Positioning**
   - Position the device at eye level
   - Ensure the screen is visible without strain
   - Consider using a device mount or stand

2. **Eye-Tracking Hardware** (if available)
   - Connect eye-tracking device to the tablet/computer
   - Calibrate according to manufacturer instructions
   - Test accuracy before use

3. **Microphone Setup**
   - Place microphone within 1-2 feet of conversation partners
   - Reduce background noise
   - Test audio input levels

4. **Display Settings**
   - Increase screen brightness if needed
   - Adjust text size for better visibility
   - Enable high contrast mode if helpful

### During Use

1. **Speak Clearly**
   - Talk at a normal pace
   - Enunciate words clearly
   - Face the microphone

2. **Give Time**
   - Allow the patient time to scan response options
   - Don't rush the selection process
   - Wait for confirmation before proceeding

3. **Monitor System**
   - Watch for status messages
   - Check battery level regularly
   - Ensure good internet connection (for future cloud features)

### Emergency Procedures

1. **Emergency Response Selected**
   - Take immediate action
   - Check patient vital signs
   - Call for help if needed

2. **System Not Responding**
   - Restart the application
   - Check device battery
   - Ensure microphone permissions are granted

## Customization

### Adding Custom Responses

Developers can add custom responses by modifying `ResponseGenerationService.cs`:

```csharp
private List<ResponseCandidate> GenerateCustomResponses()
{
    return new List<ResponseCandidate>
    {
        new ResponseCandidate("Your custom response", ResponseCategory.Statement, 0.8),
        // Add more responses here
    };
}
```

### Adjusting Grid Size

To change the communication board grid size, modify `CommunicationBoard.cs`:

```csharp
public CommunicationBoard()
{
    // Change from 3x3 to 4x4 or other sizes
    Rows = 4;
    Columns = 4;
}
```

### Customizing Response Priorities

Adjust confidence scores to change which responses appear first:

```csharp
new ResponseCandidate("High priority response", ResponseCategory.Affirmative, 0.95),
new ResponseCandidate("Lower priority response", ResponseCategory.Statement, 0.70),
```

## Troubleshooting

### Speech Not Being Recognized

**Problem**: Microphone not picking up speech

**Solutions**:
- Check microphone permissions in device settings
- Test microphone with another app
- Restart the application
- Check if microphone is muted
- Move closer to the microphone

### Responses Not Appropriate

**Problem**: Generated responses don't match the conversation

**Solutions**:
- Ensure speech is transcribed correctly
- Check conversation history for context
- Manually select "Can you repeat that?" if unclear
- Report issues to developers for intent detection improvements

### App Crashes or Freezes

**Problem**: Application stops responding

**Solutions**:
- Check device memory/storage
- Close other applications
- Restart the device
- Reinstall the application
- Check for app updates

### Eye-Tracking Not Working

**Problem**: Eye-tracking hardware not controlling selections

**Solutions**:
- Re-calibrate eye-tracking device
- Check hardware connections
- Update eye-tracking software
- Use touch/mouse as fallback
- Consult hardware manufacturer documentation

## Best Practices

### For Optimal Performance

1. **Battery Management**
   - Keep device charged above 20%
   - Reduce screen brightness when possible
   - Close unused applications

2. **Regular Maintenance**
   - Clear conversation history periodically
   - Update app when new versions available
   - Backup settings and custom responses

3. **Comfort and Ergonomics**
   - Take breaks to prevent eye strain
   - Adjust device position regularly
   - Use external keyboard/mouse as needed

### For Better Conversations

1. **Context Building**
   - Start with greetings to establish conversation
   - Build context gradually
   - Use conversation history for reference

2. **Response Selection**
   - Scan all options before selecting
   - Use emergency responses when truly needed
   - Combine responses for complex communication

3. **Feedback**
   - Use "Can you repeat that?" when unclear
   - Select "Tell me more" to continue topics
   - Use "I don't know" when uncertain

## Privacy and Data

### Data Storage

- All conversations stored locally on device
- No cloud backup (in current version)
- No data transmitted to external servers

### Data Management

- Conversation history can be cleared manually
- No personal data collected by the app
- Speech data processed locally only

### Future Privacy Features

- Optional cloud backup with encryption
- Password protection for app access
- Secure sharing with healthcare providers

## Support and Resources

### Getting Help

- Check this documentation first
- Review the ARCHITECTURE.md for technical details
- Submit issues on GitHub repository
- Contact healthcare team for usage support

### Additional Resources

- ALS Association: https://www.als.org/
- Assistive Technology resources
- Eye-tracking device documentation
- .NET MAUI documentation

## Feedback

We welcome feedback to improve NewDawn:

- Feature requests
- Bug reports
- Usability suggestions
- Response template ideas
- Documentation improvements

Please submit feedback through the GitHub repository issues page.

---

**Remember**: This system is designed to assist communication for ALS patients. Always consult with healthcare professionals and assistive technology specialists for proper setup and usage.
