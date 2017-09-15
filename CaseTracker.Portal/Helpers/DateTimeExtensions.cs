using System;

namespace CaseTracker.Portal.Helpers
{
    public static class DateTimeExtensions
    {
        public static DateTime TrimMilliseconds(this DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second, 0);
        }

        public static DateTime TrimSeconds(this DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, 0, 0);
        }
    }
}