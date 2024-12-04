using Microsoft.Extensions.Logging;
using QuickPillApp.Messaging.Interfaces;
using QuickPillApp.Messaging.Services;
using QuickPillApp.Presentation.Interfaces;
using QuickPillApp.Presentation.Services;

namespace QuickPillApp;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif

        builder.Services.AddSingleton<IDeviceFinderService, DeviceFinderService>();
        builder.Services.AddSingleton<IMessageService, MessageService>();
        builder.Services.AddSingleton<IAlertService, AlertService>();

        return builder.Build();
	}
}
