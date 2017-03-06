using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace Topevery.FMP.ObjectModel.Configuration
{
    public static class FMPConfiguration
    {
        private static readonly object _lockObject = new object();
        private static FMPConfigurationSection _section;
        #region Methods
        public static FMPConfigurationSection GetSection()
        {
            FMPConfigurationSection result = null;
            result = ConfigurationManager.GetSection(ConfigurationStrings.SectionName) as FMPConfigurationSection;
            return result;
        }
        #endregion
        
        #region Properties
        public static FMPConfigurationSection Section
        {
            get
            {
                lock(_lockObject)
                {
                    if(_section == null)
                    {
                        _section = GetSection();
                    }
                    return _section;
                }
            }
        }
        #endregion
    }
}
