using System;

namespace Hire.Core
{
    public static class Guard
    {
        public static void NotNull(object arg, string argName)
        {
            if (arg == null)
                throw new ArgumentNullException(argName);
        }
    }
}
