using System;
using System.Collections.Generic;
using System.Text;
using Topevery.FMP.ObjectModel;

namespace Topevery.FMP.Logic
{
    internal sealed class ConfigSettingCache
    {
        #region Fields
        private const int MaxCount = 4096;
        private List<Guid> _list = new List<Guid>();
        private Dictionary<Guid, ConfigSettingData> _dict = new Dictionary<Guid, ConfigSettingData>(MaxCount);
        #endregion

        #region Methods
        public void Add(Guid id, ConfigSettingData config)
        {
            if (config == null || _dict.ContainsKey(id))
            {
                return;
            }
            if (_dict.Count >= MaxCount)
            {
                Guid removeID = _list[0];
                _dict.Remove(removeID);
                _list.RemoveAt(0);
            }
            _list.Add(id);
            _dict[id] = config;
        }
        #endregion

        #region Properties
        public ConfigSettingData this[Guid id]
        {
            get
            {
                ConfigSettingData result;
                if (_dict.TryGetValue(id, out result))
                {
                    return result;
                }
                return null;
            }
        }
        #endregion
    }
}
