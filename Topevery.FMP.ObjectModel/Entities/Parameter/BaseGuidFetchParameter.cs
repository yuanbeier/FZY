using System;
using System.Collections.Generic;
using System.Text;

namespace Topevery.FMP.ObjectModel
{
    public abstract class BaseGuidFetchParameter : BaseParameter
    {
        #region Fields
        private Guid _id;
        #endregion

        #region Properties
        /// <summary>
        /// ∂‘œÛID
        /// </summary>
        public Guid ID
        {
            get
            {
                return this._id;
            }
            set
            {
                this._id = value;
            }
        }
        #endregion
    }
}
