using QuickPillApp.Presentation.ViewModels;

namespace QuickPillApp.Presentation.Views;

public partial class PillConfigView : ContentPage
{
	PillConfigViewModel viewModel;
	public PillConfigView()
	{
		InitializeComponent();

		viewModel = BindingContext as PillConfigViewModel;
	}

    protected override void OnAppearing()
    {
		viewModel.LoadConfigData();
        base.OnAppearing();
    }

    protected override bool OnBackButtonPressed()
    {
        Task.Run(() => Shell.Current.GoToAsync("//DeviceMenuView"));
        base.OnBackButtonPressed();
        return true;
    }
}