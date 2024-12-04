using QuickPillApp.Library.Models;
using QuickPillApp.Messaging.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace QuickPillApp.Messaging.Services
{
    public class MessageService : IMessageService
    {
        private TcpClient client;
        private NetworkStream clientStream;

        public bool IsConnected => clientStream != null && client != null && client.Connected;
        public string CurrentDeviceName { get; private set; }

        public void EndConnection()
        {
            if(client.Connected)
            {
                try
                {
                    CurrentDeviceName = string.Empty;
                    clientStream.Close();
                    clientStream = null;

                    client.Close();
                    client = null;
                }
                catch { }
            }
        }

        public async Task<bool> EstablishConnection(DeviceData device)
        {
            client = new TcpClient();

            try
            {
                await client.ConnectAsync(device.Address, device.Port);
                clientStream = client.GetStream();
                clientStream.ReadTimeout = 5 * 1000;
                clientStream.WriteTimeout = 5 * 1000;
                CurrentDeviceName = device.Name;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<string> RequestData(string data)
        {
            byte[] buffer = Encoding.ASCII.GetBytes($"Msg/Get/{data}");
            await clientStream.WriteAsync(buffer);

            buffer = new byte[3 * 1024];
            var bytesReceived = await clientStream.ReadAsync(buffer);

            var response = Encoding.ASCII.GetString(buffer, 0, bytesReceived);
            return response;
        }

        public async Task UpdateData(string dataType, string data)
        {
            byte[] buffer = Encoding.ASCII.GetBytes($"Msg/Update/{dataType}\n{data}");
            await clientStream.WriteAsync(buffer);
        }
    }
}
