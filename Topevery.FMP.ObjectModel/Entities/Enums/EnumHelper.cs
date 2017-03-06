using System;
using System.Collections.Generic;
using System.Text;

namespace Topevery.FMP.ObjectModel
{
    public static class EnumHelper
    {
        public static object GetEnumValue(object enumValue)
        {
            try
            {
                if (enumValue == null)
                {
                    return DBNull.Value;
                }
                if ((int) enumValue == int.MaxValue)
                {
                    return DBNull.Value;
                }
                return (int) enumValue;
            }
            catch
            {
                return DBNull.Value;
            }
        }
    }
}
