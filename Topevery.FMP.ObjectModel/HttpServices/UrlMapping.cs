using System;
using System.Collections.Generic;
using System.Text;
using Topevery.Framework.Ioc;

namespace Topevery.FMP.ObjectModel
{
    public class UrlMapping
    {
        #region Fields

        private readonly object _lockObj = new object();
        private readonly object _lockInit = new object();
        private MappingCollection _mappings;
        private bool _initialized;
        #endregion

        #region Methods
        public void Initialize()
        {
            if (!_initialized)
            {
                //bool init = false;
                lock (_lockInit)
                {
                    if (!_initialized)
                    {
                        _initialized = true;
                        //init = true;
                    }
                }
            }
        }

        public void Add(string url, string typeName)
        {
            Type type = ActivatorEx.GetType(typeName);
            this.Add(url, type);
        }

        public virtual void Add(string url, Type type)
        {
            url = ResolveUrl(url);
            if (!string.IsNullOrEmpty(url) && type != null)
            {
                MappingCollection mappings = this.Mappings;
                
                if (mappings != null)
                {
                    Type newType = null;
                    mappings.TryGetValue(url, out newType);
                    if (newType != type)
                    {
                        lock (mappings)
                        {
                            mappings[url] = type;
                        }
                    }
                }
            }
        }

        public string FindTypeName(string url)
        {
            Type type = FindType(url);
            return ActivatorEx.GetInstanceTypeName(type);
        }

        private Type FindType(string url)
        {
            url = ResolveUrl(url);
            if (!string.IsNullOrEmpty(url))
            {
                Type result;
                MappingCollection mappings = this.Mappings;
                if (mappings.TryGetValue(url, out result))
                {
                    return result;
                }
            }
            return null;
        }

        protected virtual string ResolveUrl(string url)
        {
            return url;
        }

        

        protected virtual MappingCollection CreateMappings()
        {
            return new MappingCollection();
        }
        #endregion

        #region Properties

        public Type this[string url]
        {
            get
            {
                return FindType(url);
            }
        }

        protected MappingCollection Mappings
        {
            get
            {
                if (_mappings == null)
                {
                    lock (_lockObj)
                    {
                        if (_mappings == null)
                        {
                            _mappings = CreateMappings();
                        }
                    }
                }
                return _mappings;
            }
        }
        #endregion

        #region Protected Class
        protected class MappingCollection : Dictionary<string, Type>
        {
            #region Constructor
            public MappingCollection():
                this(StringComparer.InvariantCultureIgnoreCase)
            {
            }

            public MappingCollection(IEqualityComparer<string> compare):
                base(compare)
            {
            }
            #endregion
        }
        #endregion
    }
}
