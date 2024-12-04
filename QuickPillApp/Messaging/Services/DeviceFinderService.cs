using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using QuickPillApp.Messaging.Interfaces;
using QuickPillApp.Library.Models;
using QuickPillApp.Library.Enums;

namespace QuickPillApp.Messaging.Services
{
    public class DeviceFinderService : IDeviceFinderService
    {
        private const int port = 11000;

        private UdpClient finder;

        public DeviceFinderService()
        {
            finder = new UdpClient();
            finder.EnableBroadcast = true;
            finder.Client.ReceiveTimeout = 1000 * 5;
        }

        public async Task<IEnumerable<DeviceData>> FindDevices()
        {
            await BroadcastHelloMessage();

            var data = await ReceiveMessage();

            return new List<DeviceData> { data };
        }

        private async Task BroadcastHelloMessage()
        {
            byte[] dgram = Encoding.ASCII.GetBytes("Msg/Hello/1.0");
            await finder.SendAsync(dgram, dgram.Length, "192.168.0.255", port);
        }

        private async Task<DeviceData> ReceiveMessage()
        {
            try
            {
                var result = await finder.ReceiveAsync();

                string message = Encoding.ASCII.GetString(result.Buffer);

                return new DeviceData
                {
                    Address = result.RemoteEndPoint.Address,
                    Port = 11001,
                    Name = message,
                    Status = DeviceConnectionStatus.Connected
                };
            }
            catch (SocketException)
            {
                return null;
            }
        }
    }
}
