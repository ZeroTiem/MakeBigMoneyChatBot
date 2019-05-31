using System;
using System.Collections.Generic;
using System.Text;

namespace Utility.Common
{
    public static class SystemDateTime
    {
        public static DateTime ThisSystemDateTime;
        public static bool IsSet;

        public static void Set(DateTime dateTime)
        {
            ThisSystemDateTime = dateTime;
            IsSet = true;
        }

        public static DateTime Now()
        {
            if (!IsSet)
                ThisSystemDateTime = DateTime.Now;

            return ThisSystemDateTime;
        }
    }
}
