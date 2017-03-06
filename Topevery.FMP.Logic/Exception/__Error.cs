using System;
using System.Collections.Generic;
using System.Text;

namespace Topevery.FMP.Logic
{
    internal static class __Error
    {
        public static void RecordHasLocked(Guid formID, Guid formUniqueID)
        {
            string text = SR.RecordHasLocked;
            string.Format(text, formID, formUniqueID);
            throw new RecordLockedException(text);
        }
    }
}
