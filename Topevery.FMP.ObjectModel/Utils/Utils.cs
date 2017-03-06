using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Topevery.FMP.ObjectModel
{
    internal static class Utils
    {
        public static bool IsEmpty(ICollection cols)
        {
            return cols == null || cols.Count == 0;
        }

        public static bool IsEmpty(string text)
        {
            return string.IsNullOrEmpty(text);
        }
    }
}
