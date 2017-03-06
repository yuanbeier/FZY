using System;
using System.Collections.Generic;
using System.Text;

namespace Topevery.FMP.ObjectModel
{
    public sealed class LogHelper
    {
        public static log4net.ILog Log
        {
            get
            {
                return log4net.LogManager.GetLogger("Topevery_FMP_Framework");
            }
        }
    }
}
