using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace Topevery.FMP.ObjectModel.Configuration
{
    public abstract class ConfigurationElementCollectionBase<T> : ConfigurationElementCollection where T: ConfigurationElement,new()
    {
        #region Fields
        private ConfigurationElementCollectionType collectionType;
        private string elementName;

        #endregion

        #region Constructor
        protected ConfigurationElementCollectionBase()
            : this(ConfigurationElementCollectionType.AddRemoveClearMap, null)
        {
            
        }

        protected ConfigurationElementCollectionBase(string addElementName)
            : this()
        {
            this.AddElementName = addElementName;
        }
        
        protected ConfigurationElementCollectionBase(ConfigurationElementCollectionType collectionType, string elementName) 
            : this(collectionType, elementName, StringComparer.InvariantCultureIgnoreCase)
        {
            
        }
        
        protected ConfigurationElementCollectionBase(ConfigurationElementCollectionType collectionType, string elementName, IComparer comparer)
            : base(comparer)
        {
              this.collectionType = collectionType;
              this.elementName = elementName;
              if (!string.IsNullOrEmpty(elementName))
              {
                  AddElementName = elementName;
              }
        }


        #endregion

        #region Methods
        public void Add(T element)
        {
            if (!this.IsReadOnly() && (element == null))
            {
                //throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("element");
            }
            this.BaseAdd(element);
        }

        protected override void BaseAdd(ConfigurationElement element)
        {
            if (!this.IsReadOnly() && !this.ThrowOnDuplicate)
            {
                object obj1 = this.GetElementKey(element);
                if (this.ContainsKey(obj1))
                {
                    BaseRemove(obj1);
                }
            }
            base.BaseAdd(element);
        }

        public void Clear()
        {
            BaseClear();
        }


        public virtual bool ContainsKey(object key)
        {
            if (key == null)
            {
                List<string> list = new List<string>();
                ConfigurationElement element1 = this.CreateNewElement();
                foreach (PropertyInformation information1 in element1.ElementInformation.Properties)
                {
                    if (information1.IsKey)
                    {
                        list.Add(information1.Name);
                    }
                }
                if (list.Count == 0)
                {
                    //throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("key");
                }
                if (1 == list.Count)
                {
                    //throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ConfigurationErrorsException(SR.GetString("ConfigElementKeyNull", new object[] { list1[0] })));
                }
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < (list.Count - 1); i++)
                {
                    sb = sb.Append(list[i] + ", ");
                }
                sb = sb.Append(list[list.Count - 1]);
                //throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ConfigurationErrorsException(SR.GetString("ConfigElementKeysNull", new object[] { list1.ToString() })));
            }
            return (null != BaseGet(key));
        }

        public void CopyTo(T[] array, int start)
        {
            if (array == null)
            {
                //throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("array");
            }
            if ((start < 0) || (start >= array.Length))
            {
                //throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("start", SR.GetString("ConfigInvalidStartValue", new object[] { array.Length - 1, start }));
            }
            this.CopyTo(array, start);
        }



        protected override ConfigurationElement CreateNewElement()
        {
            //return Activator.CreateInstance<T>();
            return new T();
        }

        public int IndexOf(T element)
        {
            if (element == null)
            {
                //throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("element");
            }
            return BaseIndexOf(element);
        }

        public void Remove(T element)
        {
            if (!this.IsReadOnly() && (element == null))
            {
                //throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("element");
            }
            BaseRemove(this.GetElementKey(element));
        }

        public void RemoveAt(int index)
        {
            BaseRemoveAt(index);
        }

        public void RemoveAt(object key)
        {
            if (!this.IsReadOnly() && (key == null))
            {
                //throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("key");
            }
            BaseRemove(key);
        }
        #endregion

        #region Properties
        public override ConfigurationElementCollectionType CollectionType
        {
            get
            {
                return this.collectionType;
            }
        }

        protected override string ElementName
        {
            get
            {
                string result = this.elementName;
                if (string.IsNullOrEmpty(result))
                {
                    result = base.ElementName;
                }
                return result;
            }
        }

        public T this[int index]
        {
            get
            {
                return (T)BaseGet(index);
            }
            set
            {
                if ((!this.IsReadOnly() && !this.ThrowOnDuplicate) && (BaseGet(index) != null))
                {
                    BaseRemoveAt(index);
                }
                this.BaseAdd(index, value);
            }
        }

        public virtual T this[object key]
        {
            get
            {
                if (key == null)
                {
                    //throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("key");
                }
                T local1 = (T)base.BaseGet(key);
                if (local1 == null)
                {
                    //throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new KeyNotFoundException(SR.GetString("ConfigKeyNotFoundInElementCollection", new object[] { key.ToString() })));
                }
                return local1;
            }
            set
            {
                if (this.IsReadOnly())
                {
                    this.Add(value);
                }
                if (value == null)
                {
                    //throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("value");
                }
                if (key == null)
                {
                    //throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("key");
                }
                if (!this.GetElementKey(value).ToString().Equals((string)key, StringComparison.Ordinal))
                {
                   // throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument(SR.GetString("ConfigKeysDoNotMatch", new object[] { this.GetElementKey(value).ToString(), key.ToString() }));
                }
                if (BaseGet(key) != null)
                {
                    BaseRemove(key);
                }
                this.Add(value);
            }
        }

        #endregion

        

        
    }
}
