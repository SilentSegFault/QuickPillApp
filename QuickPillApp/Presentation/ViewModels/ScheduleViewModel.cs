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
    public class ScheduleViewModel : ViewModelBase
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

        public ObservableCollection<Schedule> Schedule { get; set; }
        #endregion

        #region Services
        public IMessageService MessageService { get; set; }
        #endregion

        #region Commands
        public Command AddNewEntryCommand { get; set; }
        #endregion

        #region Constructor
        public ScheduleViewModel()
        {
            MessageService = App.Services.GetService<IMessageService>();

            AddNewEntryCommand = new Command(AddNewEntry, CanAddNewEntry);

            Schedule = new ObservableCollection<Schedule>();

            DeviceName = MessageService.CurrentDeviceName;
        }
        #endregion

        #region Commands Logic
        public async void LoadSchedules()
        {
            Schedule.Clear();

            var scheduleData = await MessageService.RequestData("Schedule");
            var schedule = JsonSerializer.Deserialize<IEnumerable<Schedule>>(scheduleData);

            var configData = await MessageService.RequestData("Config");
            var config = JsonSerializer.Deserialize<IEnumerable<DeviceSlotConfig>>(configData);

            foreach (var scheduleEntry in schedule)
            {
                var pillName = config.FirstOrDefault(c => c.SlotId == scheduleEntry.SlotId)?.PillName;
                if (string.IsNullOrEmpty(pillName))
                    scheduleEntry.PillName = "Unknown";
                else
                    scheduleEntry.PillName = pillName;
                
                Schedule.Add(scheduleEntry);
            }
        }

        private async void AddNewEntry()
        {
            await Shell.Current.GoToAsync("//ScheduleAddView");
        }

        private bool CanAddNewEntry()
        {
            return true;
        }
        #endregion
    }
}
