using System;
using System.Linq;

using Windows.ApplicationModel.Background;

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
        }

        #endregion
    }
}