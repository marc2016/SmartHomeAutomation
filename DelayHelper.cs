using System;

public static class DelayHelper
{
    public static void Delay(long microseconds)
    {
        long initialTick = mStopwatch.ElapsedTicks;
        long initialElapsed = mStopwatch.ElapsedMilliseconds;
        double desiredTicks = mMillisecondDelay / 1000.0 * Stopwatch.Frequency;
        double finalTick = initialTick + desiredTicks;
        while (mStopwatch.ElapsedTicks < finalTick) { }
    }
}

