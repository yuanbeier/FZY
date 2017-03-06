using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using Topevery.Framework.Ioc;
using System.IO;
using System.Reflection;
using System.Collections.Specialized;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web;

namespace Topevery.FMP.ObjectModel
{
    [Serializable]
    public sealed class InvokeContext : InvokeContextBase,ISerializable, IDeserializationCallback
    {
        #region Fields
        
        private static string _AssemblyName = typeof(InvokeContext).Assembly.GetName().Name;
        private static IFormatter _formatter = new BinaryFormatter();
        private const int _MethodCount = 11;
        [NonSerialized]
        private SerializationInfo _info;
        //[NonSerialized]
        //private HttpContext _context;
        #endregion

        #region Constructor
        static InvokeContext()
        {
        }
        public InvokeContext()
        {
        }

        private InvokeContext(SerializationInfo info, StreamingContext context)
        {
            _info = info;
        }
        #endregion

        #region Methods
        public static InvokeContext Deserialize(/*HttpContext context,*/Stream stream)
        {
            if (stream != null)
            {
                InvokeContext result = _formatter.Deserialize(stream) as InvokeContext;
                //if (result != null)
                //{
                //    result.Context = context;
                //}
                return result;
            }
            return null;
        }

        public static void Serialize(Stream stream, InvokeContext context)
        {
            if(stream !=null && context != null)
            {
                _formatter.Serialize(stream, context);
            }
        }

        
        #endregion

        #region Methods
        
        #endregion

        #region ISerializable Members
        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AssemblyName = _AssemblyName;
            info.AddValue("version", (byte)1);
            object[] o = new object[_MethodCount];
            o[0] = Url;
            o[1] = TypeName;
            o[2] = InterfaceName;
            o[3] = MethodName;
            o[4] = IsStatic;
            o[5] = Arguments;
            o[6] = GetTypeStrings(ArgumentTypes);
            o[7] = ReturnValue;
            o[8] = InnerException;
            object[] keys = null;
            object[] values = null;
            if (UserData != null && UserData.Count > 0)
            {
                keys = GetObject(UserData.Keys);
                values = GetObject(UserData.Values);            
            }
            o[9] = keys;
            o[10] = values;
            info.AddValue("value", o);
        }
        #endregion

        #region IDeserializationCallback Members

        void IDeserializationCallback.OnDeserialization(object sender)
        {
            int version = _info.GetByte("version");
            if (version == 1)
            {               
                object[] o = _info.GetValue("value", typeof(object[])) as object[];
                if (o != null && o.Length >= _MethodCount)
                {
                    Url = (string)o[0];
                    TypeName = (string)o[1];
                    InterfaceName = (string)o[2];
                    MethodName = (string)o[3];
                    IsStatic = (bool)o[4];
                    Arguments = (object[])o[5];
                    ArgumentTypes = GetTypeFromStrings(o[6] as string[]);
                    ReturnValue = o[7];
                    InnerException = o[8] as Exception;
                    object[] keys = o[9] as object[];
                    object[] values = o[10] as object[];
                    if (keys != null && keys.Length > 0)
                    {
                        if (values != null && keys.Length == values.Length)
                        {
                            HybridDictionary userData = this.UserData as HybridDictionary;
                            for (int i = 0; i < keys.Length; i++)
                            {
                                userData.Add(keys[i], values[i]);
                            }
                        }
                    }
                }
            }
        }

        #endregion

        #region Properties
        //public HttpContext Context
        //{
        //    get
        //    {
        //        return this._context;
        //    }
        //    internal set
        //    {
        //        this._context = value;
        //    }
        //}
        #endregion
    }
}
