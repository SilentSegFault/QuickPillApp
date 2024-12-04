namespace QuickPillApp;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
	}

    private void NavBtn_Clicked(object sender, EventArgs e)
    {
		Shell.Current.GoToAsync("//DeviceListView");
    }
}

