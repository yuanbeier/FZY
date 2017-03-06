using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using Topevery.Framework.Ioc;
using Topevery.Framework.Ioc.Reflection;
using System.Collections;
using System.Globalization;

namespace Topevery.FMP.ObjectModel
{
    public class DynamicProxyExecutor
    {
        #region Fields
        private static readonly object _syncMethodObject = new object();
        private static readonly TypeMethodCache _tpyeCache = new TypeMethodCache();
        private const BindingFlags _defaultBinding = BindingFlags.Instance| BindingFlags.Public  | BindingFlags.NonPublic | BindingFlags.InvokeMethod | BindingFlags.Static;
        private static readonly object[] _emptyArgs = new object[0];
        #endregion

        #region Method
        public virtual bool Execute(InvokeContextBase context)
        {
            bool result = ExcuteInternal(context);
            return result;
        }

        private bool ExcuteInternal(InvokeContextBase context)
        {
            try
            {
                MethodInfo methodInfo = FindMethodInfo(context);
                if (methodInfo != null)
                {
                    object instance = null;
                    if (!context.IsStatic)
                    {
                        instance = ActivatorEx.CreateInstance(methodInfo.ReflectedType);
                        if (instance != null)
                        {
                            context.ReturnValue = methodInfo.Invoke(instance, context.Arguments);
                        }
                    }
                    else
                    {
                        context.ReturnValue = methodInfo.Invoke(null, context.Arguments);
                    }
                    context.Arguments = null;
                    context.ArgumentTypes = null;
                    return true;
                }                
            }
            catch (Exception e)
            {
                context.InnerException = e.GetBaseException();
            }
            return false;
        }

        protected static MethodInfo FindMethodInfo(InvokeContextBase context)
        {
            if (context != null && !string.IsNullOrEmpty(context.TypeName)
                  && !string.IsNullOrEmpty(context.MethodName))
            {
                Type type = ActivatorEx.GetType(context.TypeName);
                if (type == null)
                {
                    return null;
                }
                MethodInfoCollection methods = GetMethodCollectionAndCache(context, type);
                if (methods != null && methods.Count > 0)
                {
                    if (methods.Count == 1)
                    {
                        return CreateMethodEx(methods[0]);
                    }

                    object[] args = context.Arguments;
                    if (args == null)
                    {
                        args = _emptyArgs;
                        context.Arguments = args;
                    }
                    int len = args.Length;
                    Type[] argTypes = context.ArgumentTypes;
                    bool selectedTypes = true;
                    if (argTypes == null)
                    {
                        selectedTypes = false;
                        argTypes = new Type[len];
                        for (int i = 0; i < len; i++)
                        {
                            if (args[i] != null)
                            {
                                argTypes[i] = args[i].GetType();
                            }
                        }
                    }
                    
                    BindingFlags binding = _defaultBinding;                    
                    ArrayList list = new ArrayList(methods.Count);
                    for (int i = 0; i < methods.Count; i++)
                    {
                        if (FilterApplyMethodBaseInfo(methods[i], binding, CallingConventions.Any, argTypes))
                        {
                            list.Add(methods[i]);
                        }
                    }
                    len = list.Count;
                    if (len >= 1)
                    {
                        MethodInfoEx info = null;
                        if (len == 1)
                        {
                            info = CreateMethodEx(list[0] as MethodInfo);
                        }
                        else
                        {
                            Binder binder = Type.DefaultBinder;
                            MethodBase methodBase = null;
                            MethodBase[] array = new MethodBase[len];
                            list.CopyTo(array);                            
                            try
                            {
                                if (!selectedTypes)
                                {
                                    object state;
                                    methodBase = binder.BindToMethod(binding, array, ref args, null, null, null, out state);
                                }
                                else
                                {
                                    methodBase = binder.SelectMethod(binding, array, argTypes, null);
                                }                                
                            }
                            catch (MissingMethodException)
                            {
                                methodBase = null;
                            }
                            if (methodBase != null)
                            {
                                info = CreateMethodEx(methodBase as MethodInfo);
                            }
                        }
                        return info;
                    }

                }
            }
            return null;
        }

        private static MethodInfoEx CreateMethodEx(MethodInfo info)
        {
            if (info != null)
            {
                return new MethodInfoEx(info);
            }
            return null;
        }

        private static MethodInfoCollection GetMethodCollectionAndCache(InvokeContextBase context, Type type)
        {
            MethodCache cache = GetMethodCache(type);
            MethodInfoCollection infos = GetMethodCollection(cache, type, context.MethodName);
            if (infos == null)
            {
                lock (_syncMethodObject)
                {
                    infos = GetMethodCollection(cache, type, context.MethodName);
                    if (infos == null)
                    {
                        infos = new MethodInfoCollection();
                        MemberInfo[] methods = type.GetMembers(_defaultBinding);
                        int index = 0; 
                        if (methods != null && methods.Length > 0)
                        {
                            foreach (MemberInfo info in methods)
                            {
                                index++;
                                if (index < 0)
                                {
                                    break;
                                }
                                if (info.MemberType == MemberTypes.Method)
                                {
                                    MethodInfo method = (MethodInfo)info;
                                    MethodAttribute attr = GetMethodAttribute(method);
                                    if (attr != null && string.Compare(attr.MethodName, context.MethodName, true) == 0)
                                    {
                                        infos.Add(method);
                                    }
                                    else if (string.Compare(info.Name, context.MethodName, true) == 0)
                                    {
                                        infos.Add(method);
                                    }
                                }
                            }
                        }
                        cache[context.MethodName] = infos;
                    }
                }
            }
            return infos;
        }

        private static MethodAttribute GetMethodAttribute(MethodInfo method)
        {
            object[] attrs = method.GetCustomAttributes(typeof(MethodAttribute), true);
            if (attrs != null && attrs.Length > 0)
            {
                foreach (object o in attrs)
                {
                    MethodAttribute attr = o as MethodAttribute;
                    if (attr != null)
                    {
                        return attr;
                    }
                }
            }
            return null;
        }

        private static bool FilterApplyMethodBaseInfo(MethodInfo methodBase, BindingFlags bindingFlags, string name, CallingConventions callConv, Type[] argumentTypes, bool prefixLookup)
        {
            bindingFlags ^= BindingFlags.DeclaredOnly;           
            return (
                    ((!prefixLookup || FilterApplyPrefixLookup(methodBase, name, (bindingFlags & BindingFlags.IgnoreCase) != BindingFlags.Default))
                    ) 
                    && FilterApplyMethodBaseInfo(methodBase, bindingFlags, callConv, argumentTypes)
                );
        }

        private static bool FilterApplyPrefixLookup(MemberInfo memberInfo, string name, bool ignoreCase)
        {
            if (ignoreCase)
            {
                if (!memberInfo.Name.ToLower(CultureInfo.InvariantCulture).StartsWith(name, StringComparison.Ordinal))
                {
                    return false;
                }
            }
            else if (!memberInfo.Name.StartsWith(name, StringComparison.Ordinal))
            {
                return false;
            }
            return true;
        }

        private static bool FilterApplyMethodBaseInfo(MethodInfo methodBase, BindingFlags bindingFlags, CallingConventions callConv, Type[] argumentTypes)
        {
            if ((callConv & CallingConventions.Any) == 0)
            {
                if (((callConv & CallingConventions.VarArgs) != 0) && ((methodBase.CallingConvention & CallingConventions.VarArgs) == 0))
                {
                    return false;
                }
                if (((callConv & CallingConventions.Standard) != 0) && ((methodBase.CallingConvention & CallingConventions.Standard) == 0))
                {
                    return false;
                }
            }
            if (argumentTypes != null)
            {
                ParameterInfo[] parametersNoCopy = methodBase.GetParameters();
                if (argumentTypes.Length != parametersNoCopy.Length)
                {
                    if ((bindingFlags & (BindingFlags.SetProperty | BindingFlags.GetProperty | BindingFlags.CreateInstance | BindingFlags.InvokeMethod)) == BindingFlags.Default)
                    {
                        return false;
                    }
                    bool flag = false;
                    if (argumentTypes.Length > parametersNoCopy.Length)
                    {
                        if ((methodBase.CallingConvention & CallingConventions.VarArgs) == 0)
                        {
                            flag = true;
                        }
                    }
                    else if ((bindingFlags & BindingFlags.OptionalParamBinding) == BindingFlags.Default)
                    {
                        flag = true;
                    }
                    else if (!parametersNoCopy[argumentTypes.Length].IsOptional)
                    {
                        flag = true;
                    }
                    if (flag)
                    {
                        if (parametersNoCopy.Length != 0)
                        {
                            if (argumentTypes.Length < (parametersNoCopy.Length - 1))
                            {
                                return false;
                            }
                            ParameterInfo info = parametersNoCopy[parametersNoCopy.Length - 1];
                            if (!info.ParameterType.IsArray)
                            {
                                return false;
                            }
                            if (info.IsDefined(typeof(ParamArrayAttribute), false))
                            {
                                goto Label_0108;
                            }
                        }
                        return false;
                    }
                }
                else if (((bindingFlags & BindingFlags.ExactBinding) != BindingFlags.Default) && ((bindingFlags & BindingFlags.InvokeMethod) == BindingFlags.Default))
                {
                    for (int i = 0; i < parametersNoCopy.Length; i++)
                    {
                        if ((argumentTypes[i] != null) && (parametersNoCopy[i].ParameterType != argumentTypes[i]))
                        {
                            return false;
                        }
                    }
                }
            }
        Label_0108:
            return true;
        }


        private static MethodInfoCollection GetMethodCollection(MethodCache cache, Type type, string name)
        {
            MethodInfoCollection info;
            if (!cache.TryGetValue(name, out info))
            {
                return null;
            }
            return info;
        }

        private static MethodCache GetMethodCache(Type type)
        {
            MethodCache result;
            if (!_tpyeCache.TryGetValue(type, out result))
            {
                lock (_tpyeCache)
                {
                    if (!_tpyeCache.TryGetValue(type, out result))
                    {
                        result = new MethodCache();
                        _tpyeCache[type] = result;
                    }
                }
            }
            return result;
        }
        #endregion

        #region Private
        private class TypeMethodCache : Dictionary<Type, MethodCache>
        {

        }

        private class MethodCache : Dictionary<string, MethodInfoCollection>
        {
            #region Constructor
            public MethodCache()
                : base(StringComparer.InvariantCultureIgnoreCase)
            {
            }
            #endregion
        }

        private class MethodInfoCollection : List<MethodInfo>
        {
        }
        #endregion
    }
}
