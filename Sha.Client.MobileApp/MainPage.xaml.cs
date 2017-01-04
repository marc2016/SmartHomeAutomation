using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Sockets.Plugin;

using Xamarin.Forms;

namespace Sha.Client.MobileApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            NewMethod();
        }

        private static async Task NewMethod()
        {
            var address = "192.168.178.104";
            var port = 11000;
            var r = new Random();

            var client = new TcpSocketClient();
            await client.ConnectAsync(address, port);

            // we're connected!
            for (int i = 0; i < 5; i++)
            {
                // write to the 'WriteStream' property of the socket client to send data
                var nextByte = (byte)r.Next(0, 254);
                //client.WriteStream.Write();
                client.WriteStream.WriteByte(nextByte);
                await client.WriteStream.FlushAsync();

                // wait a little before sending the next bit of data
                await Task.Delay(TimeSpan.FromMilliseconds(500));
            }

            await client.DisconnectAsync();
        }

        private void Button_OnClicked(object sender, EventArgs e)
        {
            NewMethod();
        }
    }
}
