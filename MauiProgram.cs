using Microsoft.Extensions.Logging;
using NewDawn.Services;
using NewDawn.ViewModels;
using NewDawn.Views;

namespace NewDawn;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        try
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            // Register Services
            builder.Services.AddSingleton<ISpeechRecognitionService, SpeechRecognitionService>();
            builder.Services.AddSingleton<IContextAnalysisService, ContextAnalysisService>();
            builder.Services.AddSingleton<IResponseGenerationService, ResponseGenerationService>();
            builder.Services.AddSingleton<ICommunicationService, CommunicationService>();

            // Register ViewModels
            builder.Services.AddSingleton<MainViewModel>();
            builder.Services.AddSingleton<CommunicationBoardViewModel>();

            // Register Views
            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<CommunicationBoardPage>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error in CreateMauiApp: {ex.Message}");
            throw;
        }
    }
}
