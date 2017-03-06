using System;
using System.Collections.Generic;
using System.Text;
using Topevery.Framework.Data;

namespace Topevery.FMP.ObjectModel
{
    [DataDictionary()]
    public enum LogicFileStatus
    {
        Normal  = 200001,

        SoftDelete = 200002,

        Delete = 200003
    }
}
