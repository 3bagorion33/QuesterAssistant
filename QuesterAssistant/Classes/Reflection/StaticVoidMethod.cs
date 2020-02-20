using System;
using System.Reflection;

namespace QuesterAssistant.Classes.Reflection
{
    /// <summary>
    /// Фабрика функторов, осуществляющих через механизм рефлексии 
    /// доступ к статическим методам класса, не возвращающим значение
    /// </summary>
    public static class StaticVoidMethodFactory
    {
        public static Action 
            GetStaticVoidMethod
                (this Type containerType, string methodName = "", BindingFlags flags = Helper.DefaultFlags) =>
            ConstructStaticMethodDelegate<Action>
                (containerType, methodName, new Type[] { }, flags);

        public static Action<TArgument1>
            GetStaticVoidMethod<TArgument1>
                (this Type containerType, string methodName = "", BindingFlags flags = Helper.DefaultFlags) =>
            ConstructStaticMethodDelegate<Action<TArgument1>>
                (containerType, methodName, new[] { typeof(TArgument1) }, flags);

        public static Action<TArgument1, TArgument2> 
            GetStaticVoidMethod<TArgument1, TArgument2>
                (this Type containerType, string methodName = "", BindingFlags flags = Helper.DefaultFlags) =>
            ConstructStaticMethodDelegate<Action<TArgument1, TArgument2>>
                (containerType, methodName, new[] { typeof(TArgument1), typeof(TArgument2) }, flags);

        public static Action<TArgument1, TArgument2, TArgument3>
            GetStaticVoidMethod<TArgument1, TArgument2, TArgument3>
                (this Type containerType, string methodName = "", BindingFlags flags = Helper.DefaultFlags) =>
            ConstructStaticMethodDelegate<Action<TArgument1, TArgument2, TArgument3>>
                (containerType, methodName, new[] { typeof(TArgument1), typeof(TArgument2), typeof(TArgument3) }, flags);

        public static Action<TArgument1, TArgument2, TArgument3, TArgument4>
            GetStaticVoidMethod<TArgument1, TArgument2, TArgument3, TArgument4>
                (this Type containerType, string methodName = "", BindingFlags flags = Helper.DefaultFlags) =>
            ConstructStaticMethodDelegate<Action<TArgument1, TArgument2, TArgument3, TArgument4>>
                (containerType, methodName, new[] { typeof(TArgument1), typeof(TArgument2), typeof(TArgument3), typeof(TArgument4) }, flags);

        public static Action<TArgument1, TArgument2, TArgument3, TArgument4, TArgument5>
            GetStaticVoidMethod<TArgument1, TArgument2, TArgument3, TArgument4, TArgument5>
                (this Type containerType, string methodName, BindingFlags flags = Helper.DefaultFlags) =>
            ConstructStaticMethodDelegate<Action<TArgument1, TArgument2, TArgument3, TArgument4, TArgument5>>
                (containerType, methodName, new[] { typeof(TArgument1), typeof(TArgument1), typeof(TArgument2), typeof(TArgument3), typeof(TArgument4), typeof(TArgument5) }, flags);

        private static TDelegate ConstructStaticMethodDelegate<TDelegate>(Type containerType, string methodName,
            Type[] argumentTypes, BindingFlags flags = BindingFlags.Default) where TDelegate : class
        {
            if (containerType is null)
                return null;

            if (argumentTypes == null)
                argumentTypes = new Type[] { };

            if (string.IsNullOrEmpty(methodName))
            {
                // Поиск метода по сигнатуре (без имени)
                if (FindBySignature(containerType, argumentTypes, flags | BindingFlags.Static | BindingFlags.NonPublic,
                    out MethodInfo method))
                    return Delegate.CreateDelegate(typeof(TDelegate), method) as TDelegate;
            }
            else
            {
                // Поиск метода по имени и сигнатуре
                if (FindByNameAndSignature(containerType, methodName, argumentTypes,
                    flags | BindingFlags.Static | BindingFlags.NonPublic, out MethodInfo method))
                    return Delegate.CreateDelegate(typeof(TDelegate), method) as TDelegate;
            }

            return null;
        }

        /// <summary>
        /// Поиск метода по сигнатуре и имени
        /// </summary>
        /// <param name="type"></param>
        /// <param name="methodName"></param>
        /// <param name="inputTypes"></param>
        /// <param name="flags"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        private static bool FindByNameAndSignature(Type type, string methodName, Type[] inputTypes, BindingFlags flags, out MethodInfo method)
        {
            if (type != null)
            {
                method = type.GetMethod(methodName, flags, null, inputTypes, null);
                if (method != null)
                {
                    return true;
                }
                return FindByNameAndSignature(type.BaseType, methodName, inputTypes, flags, out method);
            }
            method = null;
            return false;
        }

        /// <summary>
        /// Поиск метода только сигнатуре (без имени)
        /// </summary>
        /// <param name="type"></param>
        /// <param name="inputTypes"></param>
        /// <param name="flags"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        private static bool FindBySignature(Type type, Type[] inputTypes, BindingFlags flags, out MethodInfo method)
        {
            if (type != null)
            {
                foreach (MethodInfo methodInfo in type.GetMethods(flags | BindingFlags.Static))
                {
                    if (methodInfo.GetParameters().Length == inputTypes.Length)
                    {
                        var arguments = methodInfo.GetParameters();
                        bool flag = true;
                        for (int i = 0; i < arguments.Length; i++)
                        {
                            if (arguments[i].ParameterType != inputTypes[i])
                            {
                                flag = false;
                                break;
                            }
                        }
                        if (flag)
                        {
                            method = methodInfo;
                            return true;
                        }
                    }
                }
                return FindBySignature(type.BaseType, inputTypes, flags, out method);
            }
            method = null;
            return false;
        }
    }
}
