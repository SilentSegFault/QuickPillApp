using QuickPillApp.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickPillApp.Messaging.Interfaces
{
    public interface IDeviceFinderService
    {
        Task<IEnumerable<DeviceData>> FindDevices();
    }
}
