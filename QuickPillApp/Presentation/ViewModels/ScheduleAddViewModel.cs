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
    public class ScheduleAddViewModel : ViewModelBase
    {
        #region Private Fields
        private DeviceSlotConfig _selectedPill;
        private int _period;
        private int _frequency;
        private TimeSpan _time1;
        private TimeSpan _time2;
        private TimeSpan _time3;
        #endregion

        #region Properties
        public ObservableCollection<DeviceSlotConfig> Config { get; set; }

        public DeviceSlotConfig SelectedPill
        {
            get => _selectedPill;
            set => SetProperty(ref _selectedPill, value);
        }

        public int Period
        {
            get => _period;
            set => SetProperty(ref _period, value);
        }

        public int Frequency
        {
            get => _frequency;
            set => SetProperty(ref _frequency, value);
        }

        public TimeSpan Time1
        {
            get => _time1;
            set => SetProperty(ref _time1, value);
        }

        public TimeSpan Time2
        {
            get => _time2;
            set => SetProperty(ref _time2, value);
        }

        public TimeSpan Time3
        {
            get => _time3;
            set => SetProperty(ref _time3, value);
        }
        #endregion

        #region Services
        public IMessageService MessageService { get; set; }
        #endregion

        #region Commands
        public Command AddNewEntryCommand { get; set; }
        public Command BackCommand { get; set; }
        #endregion

        #region Constructor
        public ScheduleAddViewModel()
        {
            MessageService = App.Services.GetService<IMessageService>();
            AddNewEntryCommand = new Command(AddNewEntry, CanAddNewEntry);
            BackCommand = new Command(Back, CanBack);

            Config = new ObservableCollection<DeviceSlotConfig>();
        }
        #endregion

        #region Commands Logic
        public async void LoadDataAsync()
        {
            Config.Clear();

            var data = await MessageService.RequestData("Config");
            var config = JsonSerializer.Deserialize<IEnumerable<DeviceSlotConfig>>(data);
            config = config.Where(c => !string.IsNullOrEmpty(c.PillName));

            foreach (var configItem in config)
            {
                Config.Add(configItem);
            }
        }

        private bool CanAddNewEntry()
        {
            return true;
        }

        private async void AddNewEntry()
        {
            var times = new List<TimeOnly>();

            if(Time1 != TimeSpan.Zero)
            {
                var time = TimeOnly.FromTimeSpan(Time1);
                times.Add(time);
            }

            if (Time2 != TimeSpan.Zero)
            {
                var time = TimeOnly.FromTimeSpan(Time2);
                times.Add(time);
            }

            if (Time2 != TimeSpan.Zero)
            {
                var time = TimeOnly.FromTimeSpan(Time3);
                times.Add(time);
            }

            Schedule schedule = new Schedule
            {
                Id = -1,
                SlotId = SelectedPill.SlotId,
                Time = times,
                Period = Period,
                Frequency = Frequency
            };

            var data = JsonSerializer.Serialize(schedule);
            await MessageService.UpdateData("Schedule", data);
            await Shell.Current.GoToAsync("//ScheduleView");
        }

        private async void Back()
        {
            await Shell.Current.GoToAsync("//ScheduleView");
        }

        private bool CanBack()
        {
            return true;
        }
        #endregion
    }
}
