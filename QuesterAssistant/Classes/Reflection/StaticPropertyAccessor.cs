using System;
using System.Reflection;

namespace QuesterAssistant.Classes.Reflection
{
    public static class StaticPropertyAccessorFactory
    {
        public static StaticPropertyAccessor<TProperty> GetStaticProperty<TProperty>
            (this Type containerType, string propName, BindingFlags flags = Helper.DefaultFlags) =>
            new StaticPropertyAccessor<TProperty>(containerType, propName, flags);
    }

    /// <summary>
    /// Класс доступа к статическому свойству
    /// </summary>
    /// <typeparam name="TProperty"></typeparam>
    public class StaticPropertyAccessor<TProperty>
    {
        private Type containerType;
        private PropertyInfo propertyInfo;
        private MethodInfo getter;
        private MethodInfo setter;

        public StaticPropertyAccessor(Type t, string propName, BindingFlags flags = Helper.DefaultFlags)
        {
            if (string.IsNullOrEmpty(propName))
                throw new ArgumentException("Property name is invalid");

            if (!Initialize(t, propName, flags | BindingFlags.Static | BindingFlags.NonPublic))
            {
                containerType = null;
                propertyInfo = null;
                getter = null;
            }
        }

        /// <summary>
        /// Инициализация полей, необходимых для работы со свойством
        /// </summary>
        /// <param name="t"></param>
        /// <param name="propName"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        private bool Initialize(Type t, string propName, BindingFlags flags)
        {
            bool result = false;
            if (t != null)
            {
                PropertyInfo pi = t.GetProperty(propName, flags);
                if (pi != null)
                {
                    if (pi.PropertyType == typeof(TProperty))
                    {
                        MethodInfo[] accessors = pi.GetAccessors((flags & BindingFlags.NonPublic) == BindingFlags.NonPublic);
                        if (accessors.Length > 0)
                        {
                            getter = accessors[0];
                            result = true;
                        }
                        if (accessors.Length > 1)
                        {
                            setter = accessors[1];
                            result = true;
                        }
                        if (result)
                        {
                            containerType = t;
                            propertyInfo = pi;
                        }
                        return result;
                    }
                    return false;
                }
                return Initialize(t.BaseType, propName, flags);
            }
            return false;
        }

        public bool IsValid()
        {
            return containerType != null && propertyInfo != null && getter != null;
        }

        public TProperty Value
        {
            get
            {
                object result = getter?.Invoke(null, new object[] { });
                if (result != null)
                    return (TProperty)result;
                return default(TProperty);                
            }
            set
            {
                if (IsValid() && setter != null)
                {
                    setter.Invoke(null, new object[] { value });
                }
            }
        }

        public static implicit operator TProperty(StaticPropertyAccessor<TProperty> accessor) => accessor.Value;
    }
}
