using System;
using System.Diagnostics;
using System.Linq;

namespace Server.RemoteControl
{
    public static class DelayHelper
    {
        private static readonly Stopwatch _internalStopWatch;

        static DelayHelper()
        {
            _internalStopWatch = Stopwatch.StartNew();
        }

        #region Method

        public static void Delay(long microseconds)
        {
            var initialTick = _internalStopWatch.ElapsedTicks;
            var desiredTicks = microseconds / 1000d / 1000.0 * Stopwatch.Frequency;
            var finalTick = initialTick + desiredTicks;
            while (_internalStopWatch.ElapsedTicks < finalTick) {}
        }

        #endregion
    }
}