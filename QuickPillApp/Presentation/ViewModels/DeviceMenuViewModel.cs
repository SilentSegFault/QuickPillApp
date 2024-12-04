using QuickPillApp.Library.Models;
using QuickPillApp.Presentation.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QuickPillApp.Presentation.ViewModels
{
    public class DeviceMenuViewModel : ViewModelBase
    {
        #region Private Fields
        private DeviceData _currentDevice;
        #endregion

        #region Properties
        public DeviceData CurrentDevice
        {
            get => _currentDevice;
            set => SetProperty(ref _currentDevice, value);
        }
        #endregion

        #region Commands
        public Command OpenConfigCommand { get; set; }
        public Command OpenScheduleCommand { get; set; }
        #endregion

        #region Constructor
        public DeviceMenuViewModel()
        {
            OpenConfigCommand = new Command(OpenConfig, CanOpenConfig);
            OpenScheduleCommand = new Command(OpenScheduleAsync, CanOpenSchedule);
        }
        #endregion

        #region Commands Logic
        private bool CanOpenConfig()
        {
            return true;
        }

        private async void OpenConfig()
        {
            await Shell.Current.GoToAsync("//PillConfigView");
        }

        private bool CanOpenSchedule()
        {
            return true;
        }

        private async void OpenScheduleAsync()
        {
            await Shell.Current.GoToAsync("//ScheduleView");
        }
        #endregion
    }
}
