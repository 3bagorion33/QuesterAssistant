using System;
using System.Linq;
using System.Reflection;
using System.Windows.Controls;

namespace QuesterAssistant.Classes.Common
{
    public static class ReflectionHelper
    {
        public static BindingFlags DefaultFlags = BindingFlags.Instance
                                   | BindingFlags.Static
                                   | BindingFlags.GetProperty
                                   | BindingFlags.SetProperty
                                   | BindingFlags.GetField
                                   | BindingFlags.SetField
                                   | BindingFlags.Public
                                   | BindingFlags.NonPublic;

        public static MethodInfo[] GetListOfMethods(this object obj, BindingFlags flags = BindingFlags.Default, bool BaseType = false)
        {
            if (obj == null)
                return null;
            Type type = obj.GetType();

            if (BaseType)
            {
                type = type.BaseType;
            }
            return type.GetListOfMethods(flags);
        }

        public static MethodInfo[] GetListOfMethods(this Type type, BindingFlags flags = BindingFlags.Default)
        {
            flags = DefaultFlags | flags;
            MethodInfo[] methodInfos = type.GetMethods(flags);

            return methodInfos;
        }

        public static FieldInfo[] GetListOfFields(this object obj, BindingFlags flags = BindingFlags.Default, bool BaseType = false)
        {
            if (obj == null)
                return null;
            Type type = obj.GetType();

            FieldInfo[] fields = null;

            flags = DefaultFlags | flags;
            if (BaseType)
            {
                type = type.BaseType;
            }
            fields = type.GetFields(flags);

            return fields;
        }

        public static FieldInfo GetFieldInfo(this Type type, string fieldName, BindingFlags flags = BindingFlags.Default)
        {
            if (flags == BindingFlags.Default)
                flags = DefaultFlags;
            FieldInfo[] fields = type.GetFields(flags);
            if (fields != null)
            {
                foreach (FieldInfo fi in fields)
                {
                    if (fi.Name.ToLower() == fieldName.ToLower())
                        return fi;
                }
            }
            return null;
        }

        public static Type GetTypeByName(string assemblyName, string typeName, bool fullTypeName = false)
        {
            if (!string.IsNullOrEmpty(typeName) && !string.IsNullOrEmpty(assemblyName))
            {
                return AppDomain.CurrentDomain.GetAssemblies()
                    .FirstOrDefault(a => a.FullName.Contains(assemblyName))
                    .GetTypes().ToList().FirstOrDefault(t => t.FullName != null && t.FullName.Contains(typeName));
            }
            return null;
        }

        public static bool SetPropertyValue(this object obj, string propName, object value,
            BindingFlags flags = BindingFlags.Default, bool searchBaseRecursive = false)
        {
            if (obj == null)
                return false;
            Type type = obj.GetType();

            if (flags == BindingFlags.Default)
                flags = DefaultFlags;

            PropertyInfo pi = type.GetProperty(propName, flags | BindingFlags.Instance);
            if (pi != null)
            {
                MethodInfo[] accessors = pi.GetAccessors(true);
                if (accessors != null && accessors.Length == 2)
                {
                    object[] arg = new object[] { value };
                    accessors[1]?.Invoke(obj, arg);
                    return true;
                }
            }
            else if (searchBaseRecursive)
                return SetBasePropertyValue(type.BaseType, obj, propName, value, flags);

            return false;
        }

        private static bool SetBasePropertyValue(Type type, object obj, string propName, object value, BindingFlags flags = BindingFlags.Default)
        {
            if (obj == null || type == null || type == typeof(object))
                return false;

            if (flags == BindingFlags.Default)
                flags = DefaultFlags | BindingFlags.Instance;

            PropertyInfo pi = type.GetProperty(propName, flags | BindingFlags.Instance);
            if (pi == null)
                return SetBasePropertyValue(type.BaseType, obj, propName, value, flags | BindingFlags.Instance);
            else
            {
                MethodInfo[] accessors = pi.GetAccessors(true);
                if (accessors != null && accessors.Length == 2)
                {
                    object[] arg = new object[] { value };
                    accessors[1]?.Invoke(obj, arg);
                    return true;
                }
            }

            return false;
        }

        public static bool SetStaticPropertyValue(this Type type, string propName, object value,
            BindingFlags flags = BindingFlags.Default, bool searchBaseRecursive = false)
        {
            if (flags == BindingFlags.Default)
                flags = DefaultFlags | BindingFlags.Static;

            PropertyInfo pi = type.GetProperty(propName, flags | BindingFlags.Static);
            if (pi != null)
            {
                MethodInfo[] accessors = pi.GetAccessors(true);
                if (accessors != null && accessors.Length == 2)
                {
                    object[] arg = new object[] { value };
                    accessors[1]?.Invoke(null, arg);
                    return true;
                }
            }
            else if (searchBaseRecursive)
                return SetBasePropertyValue(type.BaseType, null, propName, value, flags);

            return false;
        }

        public static bool GetPropertyValue(this object obj, string propName, out object result,
            BindingFlags flags = BindingFlags.Default, bool searchBaseRecursive = false)
        {
            result = null;

            if (obj == null)
                return false;
            Type type = obj.GetType();

            if (flags == BindingFlags.Default)
                flags = DefaultFlags | BindingFlags.Instance;

            PropertyInfo pi = type.GetProperty(propName, flags);
            if (pi != null)
            {
                MethodInfo getter = pi.GetGetMethod();
                if (getter != null)
                {
                    object[] arg = new object[] { };
                    result = getter.Invoke(obj, arg);
                    return true;
                }
            }
            else if (searchBaseRecursive)
                return GetBasePropertyValue(type.BaseType, obj, propName, out result, flags);

            return false;
        }

        private static bool GetBasePropertyValue(Type type, object obj, string propName, out object result,
            BindingFlags flags = BindingFlags.Default)
        {
            result = null;

            if (obj == null || type == null || type == typeof(object))
                return false;

            if (flags == BindingFlags.Default)
                flags = DefaultFlags;

            PropertyInfo pi = type.GetProperty(propName, flags);
            if (pi == null)
                return GetBasePropertyValue(type.BaseType, obj, propName, out result, flags);
            else
            {
                MethodInfo[] accessors = pi.GetAccessors(true);
                if (accessors != null && accessors.Length > 0)
                {
                    object[] arg = new object[] { };
                    result = accessors[0]?.Invoke(obj, arg);
                    return true;
                }
            }

            return false;
        }

        public static bool GetStaticPropertyValue(this Type type, string propName, out object result,
            BindingFlags flags = BindingFlags.Default, bool searchBaseRecursive = false)
        {
            result = null;

            if (flags == BindingFlags.Default)
                flags = DefaultFlags | BindingFlags.Static;

            PropertyInfo pi = type.GetProperty(propName, flags);
            if (pi != null)
            {
                MethodInfo getter = pi.GetGetMethod();
                if (getter != null)
                {
                    object[] arg = new object[] { };
                    result = getter.Invoke(null, arg);
                    return true;
                }
            }
            else if (searchBaseRecursive)
                return GetBasePropertyValue(type.BaseType, null, propName, out result, flags);

            return false;
        }

        public static bool SetFieldValue(this object obj, string fieldName, object value,
            BindingFlags flags = BindingFlags.Default, bool BaseType = false)
        {
            if (flags == BindingFlags.Default)
                flags = DefaultFlags;

            if (obj == null)
                return false;
            Type type = obj.GetType();

            if (BaseType)
            {
                type = type.BaseType;
            }
            FieldInfo info = type.GetField(fieldName, flags);
            if (info != null)
            {
                info.SetValue(obj, value);
                return true;
            }
            else return false;
        }

        public static bool SetStaticFieldValue(this Type type, string fieldName, object value,
            BindingFlags flags = BindingFlags.Default, bool BaseType = false)
        {
            if (flags == BindingFlags.Default)
                flags = DefaultFlags;
            if (BaseType)
            {
                type = type.BaseType;
            }

            FieldInfo info = type.GetField(fieldName, flags | BindingFlags.Static);
            if (info != null)
            {
                info.SetValue(type, value);
                return true;
            }
            else return false;
        }

        public static bool GetFieldValue(this object obj, string fieldName, out object result,
            BindingFlags flags = BindingFlags.Default, bool BaseType = false)
        {
            result = null;

            if (obj == null)
                return false;
            Type type = obj.GetType();

            if (flags == BindingFlags.Default)
                flags = DefaultFlags;

            if (BaseType)
            {
                type = type.BaseType;
            }

            FieldInfo fi = type.GetField(fieldName, flags);
            if (fi != null)
            {
                result = fi.GetValue(obj);
                return true;
            }

            return false;
        }

        public static bool GetStaticFieldValue(this Type type, string fieldName, out object result,
            BindingFlags flags = BindingFlags.Default, bool BaseType = false)
        {
            result = null;
            if (flags == BindingFlags.Default)
                flags = DefaultFlags;
            if (BaseType)
            {
                type = type.BaseType;
            }

            FieldInfo fi = type.GetField(fieldName, flags | BindingFlags.Static);
            if (fi != null)
            {
                result = fi.GetValue(type);
                return true;
            }

            return false;
        }

        public static bool ExecMethod(this object obj, string methodName, object[] arguments, out object result,
            BindingFlags flags = BindingFlags.Default, bool BaseType = false)
        {
            result = null;

            if (obj == null)
                return false;
            Type type = obj.GetType();

            if (flags == BindingFlags.Default)
                flags = DefaultFlags;

            flags = DefaultFlags | flags;
            if (BaseType)
            {
                type = type.BaseType;
            }

            MethodInfo m = type.GetMethod(methodName, flags);
            if (m != null)
            {
                result = m.Invoke(obj, arguments);
                return true;
            }
            return false;
        }

        public static bool ExecStaticMethod(this Type type, string MethodName,
            BindingFlags flags = BindingFlags.Default, bool BaseType = false)
        {
            return type.ExecStaticMethod(MethodName, out var result, flags);
        }

        public static bool ExecStaticMethod(this Type type, string MethodName, object[] arguments,
            BindingFlags flags = BindingFlags.Default, bool BaseType = false)
        {
            return type.ExecStaticMethod(MethodName, arguments, out var result, flags);
        }

        public static bool ExecStaticMethod(this Type type, string MethodName, out object result,
            BindingFlags flags = BindingFlags.Default, bool BaseType = false)
        {
            return type.ExecStaticMethod(MethodName, new object[0], out result, flags);
        }

        public static bool ExecStaticMethod(this Type type, string MethodName, object[] arguments, out object result,
            BindingFlags flags = BindingFlags.Default, bool BaseType = false)
        {
            result = null;

            if (flags == BindingFlags.Default)
                flags = DefaultFlags;

            if (BaseType)
            {
                type = type.BaseType;
            }

            MethodInfo methodInfo = type.GetMethod(MethodName, flags | BindingFlags.Static);
            if (methodInfo != null)
            {
                result = methodInfo.Invoke(null, arguments);
                return true;
            }
            return false;
        }

        public static bool ExecStaticMethodByArgs(this Type type, string MethodName, object[] arguments, out object result,
            BindingFlags flags = BindingFlags.Default, bool BaseType = false)
        {
            result = null;

            if (flags == BindingFlags.Default)
                flags = DefaultFlags;

            if (BaseType)
            {
                type = type.BaseType;
            }

            Type[] types;
            if (arguments != null && arguments.Length > 0)
            {
                types = new Type[arguments.Length];
                for (int i = 0; i < arguments.Length; i++)
                    types[i] = arguments[i].GetType();
            }
            else types = new Type[] { };

            MethodInfo methodInfo = type.GetMethod(MethodName, flags | BindingFlags.Static, null, types, null);
            if (methodInfo != null)
            {
                result = methodInfo.Invoke(null, arguments);
                return true;
            }
            return false;
        }

        private static void SubscribeEvent(this object obj, Type control, Type typeHandler, string EventName, MethodInfo method, bool IsConsole = false)
        {
            EventInfo eventInfo = control.GetEvent(EventName);
            Delegate handler;
            if (IsConsole)
            {
                handler = Delegate.CreateDelegate(typeHandler, null, method);
                eventInfo.AddEventHandler(obj, handler);
            }
            else
            {
                handler = Delegate.CreateDelegate(typeHandler, obj, method);
                eventInfo.AddEventHandler(control, handler);
            }
        }

        public static void SubscribeEvent(this object obj, Control control, Type typeHandler, string EventName, MethodInfo method)
        {
            if (typeof(Control).IsAssignableFrom(control.GetType()))
            {
                obj.SubscribeEvent(control.GetType(), typeHandler, EventName, method);
            }
        }

        private static void UnSubscribeEvent(this object obj, Type control, Type typeHandler, string EventName, MethodInfo method, bool IsConsole = false)
        {
            if (obj != null)
            {
                EventInfo eventInfo = control.GetEvent(EventName);
                if (IsConsole)
                {
                    Delegate handler = Delegate.CreateDelegate(typeHandler, null, method);
                    if (handler != null)
                        eventInfo.RemoveEventHandler(obj, handler);
                }
                else
                {
                    Delegate handler = Delegate.CreateDelegate(typeHandler, obj, method);
                    if (handler != null)
                        eventInfo.RemoveEventHandler(control, handler);
                }
            }
        }
        public static void UnSubscribeEvent(this object obj, Control control, Type typeHandler, string EventName, MethodInfo method)
        {
            if (typeof(Control).IsAssignableFrom(control.GetType()))
            {
                obj.UnSubscribeEvent(control.GetType(), typeHandler, EventName, method);
            }
        }
    }
}
