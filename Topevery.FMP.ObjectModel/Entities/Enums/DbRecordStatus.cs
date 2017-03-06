using System;
using System.Collections.Generic;
using System.Text;
#if WCF
using System.Runtime.Serialization;
#endif

namespace Topevery.FMP.ObjectModel
{
    public enum DbRecordStatus
    {
        Normal,
        SoftDelete
    }
}
