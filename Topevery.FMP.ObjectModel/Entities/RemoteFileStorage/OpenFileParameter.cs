using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Topevery.FMP.ObjectModel
{
    [Serializable]
    public class OpenFileParameter : BaseUpdateParameter<OpenFileItemDataCollection>
    {
        [XmlArrayItem(typeof(OpenFileItemData))]
        [XmlArrayItem(typeof(RemoteStreamContext))]
        public override OpenFileItemDataCollection InputData
        {
            get
            {
                return base.InputData;
            }
        }

    }
}
