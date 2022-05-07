using System;

namespace Hire.Core.Extensions
{
    public static class DateTimeOffsetExtensions
    {
        public static DateTimeOffset StartOfDay(this DateTimeOffset @this, TimeSpan? offset = null)
        {
            return new DateTimeOffset(new DateTime(@this.Year, @this.Month, @this.Day), offset ?? TimeSpan.Zero);
        }

        public static DateTimeOffset EndOfDay(this DateTimeOffset @this, TimeSpan? offset = null)
        {
            return new DateTimeOffset(new DateTime(@this.Year, @this.Month, @this.Day).AddDays(1).Subtract(TimeSpan.FromTicks(1)), offset ?? TimeSpan.Zero);
        }
    }
}
