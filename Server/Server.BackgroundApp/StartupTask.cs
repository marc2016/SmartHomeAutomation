using System;
using System.Diagnostics;
using System.Linq;

using Windows.ApplicationModel.Background;

using Sockets.Plugin;

// The Background Application template is documented at http://go.microsoft.com/fwlink/?LinkID=533884&clcid=0x409

namespace Server.BackgroundApp
{
    public sealed class StartupTask : IBackgroundTask
    {
        #region Fields

        private BackgroundTaskDeferral deferral;

        #endregion

        #region Method

        public void Run(IBackgroundTaskInstance taskInstance)
        {
            this.deferral = taskInstance.GetDeferral();
            var listenPort = 11000;
            var listener = new TcpSocketListener();

            // when we get connections, read byte-by-byte from the socket's read stream
            listener.ConnectionReceived += async (sender, args) =>
            {
                var client = args.SocketClient;

                var bytesRead = -1;
                var buf = new byte[1];

                while (bytesRead != 0)
                {
                    bytesRead = await args.SocketClient.ReadStream.ReadAsync(buf, 0, 1);
                    if (bytesRead > 0)
                        Debug.Write(buf[0]);
                }
            };

            // bind to the listen port across all interfaces
            listener.StartListeningAsync(listenPort);
        }

        #endregion
    }
}