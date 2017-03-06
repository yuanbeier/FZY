using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using Topevery.Framework.Data.Collections;

namespace Topevery.FMP.ObjectModel
{
    [Serializable]
    public class BaseMore2MoreUpdateParameter<TData, TCollection> : BaseUpdateParameter<TCollection>
        where TCollection : BaseList<TData>
    {
        #region propertities
        private bool check_duplicate_data = true;

        /// <summary>
        /// 如果是删除，则其值始终为false。
        /// </summary>
        public virtual bool CheckDuplicateData
        {
            get {
                return this.check_duplicate_data;
            }
            set {
                this.check_duplicate_data = value;
            }
        }
        #endregion

        #region constructor
        public BaseMore2MoreUpdateParameter()
        { }
        public BaseMore2MoreUpdateParameter(TData item)
        {
            this.Add(item);
        }

        public BaseMore2MoreUpdateParameter(TCollection items)
        {
            this.Add(items);
        }
        #endregion
        #region methods
        public void Add(TData item)
        {
            if (item != null)
            {
                this.InputData.Add(item);
            }
        }

        public void Add(TCollection items)
        {
            if (items != null && items.Count > 0)
            {
                this.InputData.AddRange(items);
            }
        }


        [XmlIgnore()]
        public TCollection Items
        {
            get
            {
                return this.InputData;
            }
        }
        #endregion
    }
}
