using QuickPillApp.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickPillApp.Messaging.Interfaces
{
    public interface IMessageService
    {
        bool IsConnected { get; }
        string CurrentDeviceName { get; }

        Task<bool> EstablishConnection(DeviceData device);
        void EndConnection();
        Task<string> RequestData(string data);
        Task UpdateData(string dataType, string data);
    }
}
