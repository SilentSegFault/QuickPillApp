using QuickPillApp.Library.Models;
using QuickPillApp.Messaging.Interfaces;
using QuickPillApp.Presentation.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace QuickPillApp.Presentation.ViewModels
{
    public class PillConfigViewModel : ViewModelBase
    {
        #region Private fields
        private string _deviceName;
        #endregion

        #region Properties
        public string DeviceName
        {
            get => _deviceName;
            set => SetProperty(ref _deviceName, value);
        }

        public ObservableCollection<DeviceSlotConfig> Config { get; set; }
        #endregion

        #region Services
        public IMessageService MessageService { get; set; }
        #endregion

        #region Commands
        public Command SaveCommand { get; set; }
        public Command BackCommand { get; set; }
        #endregion

        #region Constructor
        public PillConfigViewModel()
        {
            MessageService = App.Services.GetService<IMessageService>();

            SaveCommand = new Command(Save, CanSave);
            BackCommand = new Command(Back, CanBack);

            Config = new ObservableCollection<DeviceSlotConfig>();

            DeviceName = MessageService.CurrentDeviceName;
        }
        #endregion

        #region Commands Logic
        public async void LoadConfigData()
        {
            Config.Clear();

            var data = await MessageService.RequestData("Config");
            var config = JsonSerializer.Deserialize<IEnumerable<DeviceSlotConfig>>(data);

            foreach (var configItem in config)
            {
                Config.Add(configItem);
            }
        }

        private bool CanSave()
        {
            return true;
        }

        private async void Save()
        {
            var data = JsonSerializer.Serialize(Config);
            await MessageService.UpdateData("Config", data);

            await Shell.Current.GoToAsync("//DeviceMenuView");
        }

        private async void Back()
        {
            await Shell.Current.GoToAsync("//DeviceMenuView");
        }

        private bool CanBack()
        {
            return true;
        }
        #endregion
    }
}
