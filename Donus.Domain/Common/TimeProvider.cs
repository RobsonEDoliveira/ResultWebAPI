using System;

namespace Donus.Domain.Common
{
    public class TimeProvider : ITimeProvider
    {
        public DateTime utcDateTime() => DateTime.UtcNow;
    }
}
