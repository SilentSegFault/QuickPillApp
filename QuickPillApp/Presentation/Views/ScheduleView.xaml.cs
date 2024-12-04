using QuickPillApp.Presentation.ViewModels;

namespace QuickPillApp.Presentation.Views;

public partial class ScheduleView : ContentPage
{
	ScheduleViewModel viewModel { get; set; }

	public ScheduleView()
	{
		InitializeComponent();

		viewModel = BindingContext as ScheduleViewModel;
	}

    protected override void OnAppearing()
    {
        viewModel.LoadSchedules();
        base.OnAppearing();
    }

    protected override bool OnBackButtonPressed()
    {
        Task.Run(() => Shell.Current.GoToAsync("//DeviceMenuView"));
        base.OnBackButtonPressed();
        return true;
    }
}