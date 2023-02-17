using System;

namespace Donus.Domain.Common
{
    public interface ITimeProvider
    {
        DateTime utcDateTime();
    }
}
