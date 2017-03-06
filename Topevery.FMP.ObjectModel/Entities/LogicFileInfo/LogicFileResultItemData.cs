using System;
using System.Collections.Generic;
using System.Text;

namespace Topevery.FMP.ObjectModel
{
    [Serializable]
    public class LogicFileResultItemData 
    {
        #region Fields
        LogicFileInfoData _logicFileInfo;
        #endregion

        #region Properties
        public Guid LogicFileID
        {
            get
            {
                if (LogicFileInfo == null)
                {
                    return Guid.Empty;
                }
                return LogicFileInfo.ID;
            }
        }

        public LogicFileInfoData LogicFileInfo
        {
            get
            {
                return this._logicFileInfo;
            }
            set
            {
                this._logicFileInfo = value;
            }
        }
        #endregion
    }
}
