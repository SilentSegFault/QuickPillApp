using QuickPillApp.Presentation.ViewModels;

namespace QuickPillApp.Presentation.Views;

public partial class ScheduleAddView : ContentPage
{
    ScheduleAddViewModel viewModel;

	public ScheduleAddView()
	{
		InitializeComponent();

        viewModel = BindingContext as ScheduleAddViewModel;
	}

    protected override bool OnBackButtonPressed()
    {
        Task.Run(() => Shell.Current.GoToAsync("//ScheduleView"));
        base.OnBackButtonPressed();
        return true;
    }

    protected override void OnAppearing()
    {
        viewModel.LoadDataAsync();
        base.OnAppearing();
    }
}