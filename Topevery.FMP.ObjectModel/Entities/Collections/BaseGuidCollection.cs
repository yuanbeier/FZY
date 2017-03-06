using System;
using System.Collections.Generic;
using System.Text;
using Topevery.Framework.Data;
using Topevery.Framework.Data.Collections;

namespace Topevery.FMP.ObjectModel
{
    [Serializable]
    public class BaseGuidCollection<TItem> : DataCollectionBase<Guid, TItem> where TItem : BaseEntity<Guid>
    {
        protected override Guid GetKeyForItem(TItem item)
        {
            return item.ID;
        }       
    }
}
