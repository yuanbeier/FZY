using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Specialized;
using System.Collections;
using Topevery.Framework.Ioc;

namespace Topevery.FMP.ObjectModel
{
    [Serializable]
    public abstract class InvokeContextBase
    {
        #region Fields
        private const string Separate = ",";
        private string _url;
        private string _typeName;
        private string _interfaceName;
        private string _methodName;
        private bool _isStatic;
        private object[] _arguments;
        private Type[] _argumentTypes;
        private object _returnValue;
        private Exception _innerException;
        private HybridDictionary _userData;
        #endregion

        #region Constructor
        protected InvokeContextBase()
        {
        }
        #endregion

        #region Methods
        protected static object[] GetObject(ICollection cols)
        {
            int count = cols.Count;
            if (count > 0)
            {
                object[] result = new object[count];
                cols.CopyTo(result, 0);
                return result;
            }
            return null;
        }

        protected static string[] GetTypeStrings(Type[] types)
        {
            if (types != null && types.Length > 0)
            {
                string[] result = new string[types.Length];
                for (int i = 0; i < types.Length; i++)
                {
                    result[i] = ActivatorEx.GetInstanceTypeName(types[i]);
                }
                return result;
            }
            return null;
        }

        protected static Type[] GetTypeFromStrings(string[] typeNames)
        {
            if (typeNames != null && typeNames.Length > 0)
            {
                Type[] result = new Type[typeNames.Length];
                for (int i = 0; i < typeNames.Length; i++)
                {
                    result[i] = ActivatorEx.GetType(typeNames[i]);
                    if (result[i] == null)
                    {
                        return null;
                    }
                }
                return result;
            }
            return null;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Url").Append(this.Url).Append(Separate);
            sb.Append("TypeName").Append(this.TypeName).Append(Separate);
            sb.Append("InterfaceName").Append(this.InterfaceName).Append(Separate);
            sb.Append("MethodName").Append(this.MethodName).Append(Separate);
            sb.Append("IsStatic").Append(this.IsStatic).Append(Separate);
            if (this.InnerException != null)
            {
                sb.Append("InnerException").Append(this.InnerException.Message).Append(Separate);
            }
            return sb.ToString();
        }
        #endregion

        #region Properties
        /// <summary>
        /// 调用Url
        /// </summary>
        public string Url
        {
            get { return _url; }
            set { _url = value; }
        }

        /// <summary>
        /// 调用类型名称
        /// </summary>
        public string TypeName
        {
            get { return _typeName; }
            set { _typeName = value; }
        }

        /// <summary>
        /// 调用接口名称
        /// </summary>
        public string InterfaceName
        {
            get { return _interfaceName; }
            set { _interfaceName = value; }
        }

        /// <summary>
        /// 调用方法
        /// </summary>
        public string MethodName
        {
            get { return _methodName; }
            set { _methodName = value; }
        }

        /// <summary>
        /// 是否为静态方法
        /// </summary>
        public bool IsStatic
        {
            get { return _isStatic; }
            set { _isStatic = value; }
        }

        /// <summary>
        /// 调用参数
        /// </summary>
        public object[] Arguments
        {
            get { return _arguments; }
            set { _arguments = value; }
        }

        public Type[] ArgumentTypes
        {
            get { return _argumentTypes; }
            set { _argumentTypes = value; }
        }

        /// <summary>
        /// 返回值
        /// </summary>
        public object ReturnValue
        {
            get { return _returnValue; }
            set { _returnValue = value; }
        }

        /// <summary>
        /// 执行异常对象
        /// </summary>
        public Exception InnerException
        {
            get { return _innerException; }
            set { _innerException = value; }
        }

        public IDictionary UserData
        {
            get
            {
                if (_userData == null)
                {
                    _userData = new HybridDictionary(true);
                }
                return _userData;
            }

        }
        #endregion
    }
}
