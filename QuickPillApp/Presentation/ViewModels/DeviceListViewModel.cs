using QuickPillApp.Library.Models;
using QuickPillApp.Messaging.Interfaces;
using QuickPillApp.Presentation.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QuickPillApp.Presentation.ViewModels
{
    public class DeviceListViewModel : ViewModelBase
    {
        #region Properties
        public ObservableCollection<DeviceData> NearbyDevices { get; set; }
        #endregion

        #region Services
        private IDeviceFinderService DeviceFinderService { get; set; }
        private IMessageService MessageService { get; set; }
        private IAlertService AlertService { get; set; }
        #endregion

        #region Commands
        public Command RefreshCommand { get; set; }
        #endregion

        #region Constructor
        public DeviceListViewModel()
        {
            DeviceFinderService = App.Services.GetService<IDeviceFinderService>();
            MessageService = App.Services.GetService<IMessageService>();
            AlertService = App.Services.GetService<IAlertService>();

            NearbyDevices = new ObservableCollection<DeviceData>();

            RefreshCommand = new Command(Refresh, CanRefresh);
        }
        #endregion

        #region Navigation Methods
        public void OnAppearing()
        {
            Refresh();
        }
        #endregion

        #region Commands Logic
        private bool CanRefresh()
        {
            return true;
        }

        private async void Refresh()
        {
            NearbyDevices.Clear();

            var devices = await DeviceFinderService.FindDevices();

            foreach (var device in devices)
            {
                NearbyDevices.Add(device);
            }
        }

        public async void ConnectToDevice(DeviceData device)
        {
            var connected = await MessageService.EstablishConnection(device);
            if (connected)
            {
                await Shell.Current.GoToAsync("//DeviceMenuView");
            }
            else
            {
                AlertService.ShowAlert("Ups!", "Unable to connect to the device!");
            }
        }
        #endregion
    }
}
