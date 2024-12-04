using QuickPillApp.Messaging.Interfaces;

namespace QuickPillApp.Presentation.Views;

public partial class DeviceMenuView : ContentPage
{
	public DeviceMenuView()
	{
		InitializeComponent();
	}

    protected override bool OnBackButtonPressed()
    {
        Task.Run(async () =>
        {
            App.Services.GetService<IMessageService>().EndConnection();
            await Shell.Current.GoToAsync("//DeviceListView");
        });
        base.OnBackButtonPressed();
        return true;
    }
}