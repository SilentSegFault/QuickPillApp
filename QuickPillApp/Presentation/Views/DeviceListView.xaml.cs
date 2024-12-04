using QuickPillApp.Library.Models;
using QuickPillApp.Presentation.ViewModels;

namespace QuickPillApp.Presentation.Views;

public partial class DeviceListView : ContentPage
{
	DeviceListViewModel viewModel;

	public DeviceListView()
	{
		InitializeComponent();

		viewModel = BindingContext as DeviceListViewModel;
	}

    protected override void OnAppearing()
    {
		viewModel.OnAppearing();
        base.OnAppearing();
    }

    private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
    {
        var device = e.Item as DeviceData;
        if(device != null)
            viewModel.ConnectToDevice(device);
    }
}