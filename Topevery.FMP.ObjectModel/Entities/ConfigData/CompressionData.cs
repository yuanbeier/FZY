using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.Collections.Specialized;
using System.ComponentModel;

namespace Topevery.FMP.ObjectModel
{
    [XmlRoot("compression")]
    [Serializable]
    public class CompressionData
    {
        #region Fields
        private CompressionType _compressType = CompressionType.None;
        private int _minSize;
        private int _maxSize = int.MaxValue;        
        #endregion

        #region Properties
        [DefaultValue(CompressionType.None)]
        [XmlAttribute("compressionType")]
        public CompressionType CompressionType
        {
            get
            {
                return this._compressType;
            }
            set
            {
                this._compressType = value;
            }
        }

        [XmlAttribute("minSize")]
        [DefaultValue(0)]
        public int MinSize
        {
            get
            {
                return this._minSize;
            }
            set
            {
                this._minSize = value;
            }
        }

        [XmlAttribute("maxSize")]
        [DefaultValue(int.MaxValue)]
        public int MaxSize
        {
            get
            {
                return this._maxSize;
            }
            set
            {
                this._maxSize = value;
            }
        }
        #endregion
    }
}
