using System;
using System.Collections.Generic;
using System.Text;

namespace Topevery.FMP.ObjectModel
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class MethodAttribute : Attribute
    {
        #region Private
        private string _methodName;
        #endregion

        #region Constructor
        public MethodAttribute(string methodName)
        {
            _methodName = methodName;
        }
        #endregion

        #region Properties
        public string MethodName
        {
            get
            {
                return _methodName;
            }
        }
        #endregion
    }
}
