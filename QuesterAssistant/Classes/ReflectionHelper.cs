using System;
using System.Reflection;

namespace QuesterAssistant.Classes
{
    public static class ReflectionHelper
    {
        public static BindingFlags Flags = BindingFlags.Instance
                                   | BindingFlags.GetProperty
                                   | BindingFlags.SetProperty
                                   | BindingFlags.GetField
                                   | BindingFlags.SetField
                                   | BindingFlags.NonPublic;

        /// <summary>
        /// A static method to get the FieldInfo of a private field of any object.
        /// </summary>
        /// <param name="type">The Type that has the private field</param>
        /// <param name="fieldName">The name of the private field</param>
        /// <returns>FieldInfo object. It has the field name and a useful GetValue() method.</returns>
        public static FieldInfo GetPrivateFieldInfo(Type type, string fieldName, BindingFlags flags = BindingFlags.Default)
        {
            if (flags == BindingFlags.Default)
                flags = Flags;
            FieldInfo[] fields = type.GetFields(flags);
            if (fields != null)
            {
                foreach (FieldInfo fi in fields) //fields.FirstOrDefault(feildInfo => feildInfo.Name == fieldName);
                {
                    if (fi.Name.ToLower() == fieldName.ToLower())
                        return fi;
                }
            }
            return null;
        }
        /// <summary>
        /// A static method to get the FieldInfo of a private field <see cref="fieldName"> of any object <see cref="o">.
        /// </summary>
        /// <param name="type">The Type that has the private field</param>
        /// <param name="fieldName">The name of the private field</param>
        /// <param name="o">The instance from which to read the private value.</param>
        /// <returns>The value of the property boxed as an object.</returns>
        public static object GetPrivateFieldValue(Type type, string fieldName, object o, BindingFlags flags = BindingFlags.Default)
        {
            FieldInfo fi = GetPrivateFieldInfo(type, fieldName, flags);
            if (fi != null)
            {
                return fi.GetValue(o);
            }
            return null;
        }

        /// <summary>
        /// A static method to assigne <see cref="value"> to the field <see cref="fieldName">.
        /// </summary>
        /// <param name="obj">The instance contains private filed</param>
        /// <param name="fieldName">The name of the private field</param>
        /// <param name="value">The value which assigns to the private field</param>
        /// <param name="flags"></param>
        /// <param name="BaseType"></param>
        /// <returns></returns>
        public static bool SetPrivateFieldValue(object obj, string fieldName, object value, BindingFlags flags = BindingFlags.Default, bool BaseType = false)
        {
            Type type = obj.GetType();
            if (BaseType)
            {
                type = type.BaseType;
            }
            FieldInfo info = GetPrivateFieldInfo(type, fieldName, flags);
            if (info != null)
            {
                info.SetValue(obj, value);
                return true;
            }
            return false;
        }

        public static T GetPrivatePropertyValue<T, TO>(string propertyName, BindingFlags flags = BindingFlags.Default)
        {
            var ass = Assembly.GetAssembly(typeof(TO));
            var type = ass.GetType(typeof(TO).FullName);
            var prop = type.GetProperty(propertyName, flags);
            return (T) prop.GetValue(type);
        }
    }
}
