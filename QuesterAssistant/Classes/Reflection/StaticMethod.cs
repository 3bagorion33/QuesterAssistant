using System;
using System.Reflection;

namespace QuesterAssistant.Classes.Reflection
{
    /// <summary>
    /// Фабрика функторов, осуществляющих через механизм рефлексии 
    /// доступ к статическим методам класса, возвращающим значение заданного типа
    /// </summary>
    public static class StaticMethodDelegateFactory
    {
        public static Action<TReturn>
            GetStaticMethod<TReturn>
                (this Type containerType, string methodName = "", BindingFlags flags = Helper.DefaultFlags) =>
            ConstructStaticMethodDelegate<Action<TReturn>, TReturn>
                (containerType, methodName, new Type[] { }, flags);

        public static Func<TArgument1, TReturn> 
            GetStaticMethod<TArgument1, TReturn>
                (this Type containerType, string methodName = "", BindingFlags flags = Helper.DefaultFlags) =>
            ConstructStaticMethodDelegate<Func<TArgument1, TReturn>, TReturn>
                (containerType, methodName, new[] { typeof(TArgument1) }, flags);

        public static Func<TArgument1, TArgument2, TReturn>
            GetStaticMethod<TArgument1, TArgument2, TReturn>
                (this Type containerType, string methodName = "", BindingFlags flags = Helper.DefaultFlags) =>
            ConstructStaticMethodDelegate<Func<TArgument1, TArgument2, TReturn>, TReturn>
                (containerType, methodName, new[] { typeof(TArgument1), typeof(TArgument2) }, flags);

        public static Func<TArgument1, TArgument2, TArgument3, TReturn>
            GetStaticMethod<TArgument1, TArgument2, TArgument3, TReturn>
                (this Type containerType, string methodName = "", BindingFlags flags = Helper.DefaultFlags) =>
            ConstructStaticMethodDelegate<Func<TArgument1, TArgument2, TArgument3, TReturn>, TReturn>
                (containerType, methodName, new[] { typeof(TArgument1), typeof(TArgument2), typeof(TArgument3) }, flags);

        public static Func<TArgument1, TArgument2, TArgument3, TArgument4, TReturn>
            GetStaticMethod<TArgument1, TArgument2, TArgument3, TArgument4, TReturn>
                (this Type containerType, string methodName = "", BindingFlags flags = Helper.DefaultFlags) =>
            ConstructStaticMethodDelegate<Func<TArgument1, TArgument2, TArgument3, TArgument4, TReturn>, TReturn>
                (containerType, methodName, new[] { typeof(TArgument1), typeof(TArgument2), typeof(TArgument3), typeof(TArgument4) }, flags);

        public static Func<TArgument1, TArgument2, TArgument3, TArgument4, TArgument5, TReturn>
            GetStaticMethod<TArgument1, TArgument2, TArgument3, TArgument4, TArgument5, TReturn>
                (this Type containerType, string methodName, BindingFlags flags = Helper.DefaultFlags) =>
            ConstructStaticMethodDelegate<Func<TArgument1, TArgument2, TArgument3, TArgument4, TArgument5, TReturn>, TReturn>
                (containerType, methodName, new[] { typeof(TArgument1), typeof(TArgument1), typeof(TArgument2), typeof(TArgument3), typeof(TArgument4), typeof(TArgument5) }, flags);

        private static TDelegate ConstructStaticMethodDelegate<TDelegate, TReturn>(Type containerType, string methodName, Type[] argumentTypes, BindingFlags flags = Helper.DefaultFlags) where TDelegate : class
        {
            if (containerType is null)
                return null;

            if (argumentTypes == null)
                argumentTypes = new Type[] { };

            if (string.IsNullOrEmpty(methodName))
            {
                // Поиск метода по сигнатуре (без имени)
                if (FindBySignature(containerType, typeof(TReturn), argumentTypes, flags | BindingFlags.Static | BindingFlags.NonPublic, out MethodInfo method))
                    return Delegate.CreateDelegate(typeof(TDelegate), method) as TDelegate;
            }
            else
            {
                // Поиск метода по имени и сигнатуре
                if (FindByNameAndSignature(containerType, methodName, typeof(TReturn), argumentTypes, flags | BindingFlags.Static | BindingFlags.NonPublic, out MethodInfo method))
                    return Delegate.CreateDelegate(typeof(TDelegate), method) as TDelegate;
            }
            return null;
        }

        /// <summary>
        /// Поиск метода по сигнатуре и имени
        /// </summary>
        /// <param name="type"></param>
        /// <param name="methodName"></param>
        /// <param name="returnType"></param>
        /// <param name="inputTypes"></param>
        /// <param name="flags"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        private static bool FindByNameAndSignature(Type type, string methodName, Type returnType, Type[] inputTypes, BindingFlags flags, out MethodInfo method)
        {
            if (type != null)
            {
                method = type.GetMethod(methodName, flags, null, inputTypes, null);
                if (method != null)
                {
                    if (method.ReturnType == returnType)
                        return true;
                    {
                        method = null;
                        return false;
                    }
                }
                return FindByNameAndSignature(type.BaseType, methodName, returnType, inputTypes, flags, out method);
            }
            method = null;
            return false;
        }

        /// <summary>
        /// Поиск метода только сигнатуре (без имени)
        /// </summary>
        /// <param name="type"></param>
        /// <param name="returnType"></param>
        /// <param name="inputTypes"></param>
        /// <param name="flags"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        private static bool FindBySignature(Type type, Type returnType, Type[] inputTypes, BindingFlags flags, out MethodInfo method)
        {
            if (type != null)
            {
                foreach (MethodInfo methodInfo in type.GetMethods(flags | BindingFlags.Static))
                {
                    if (methodInfo.ReturnType == returnType && methodInfo.GetParameters().Length == inputTypes.Length)
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
                return FindBySignature(type.BaseType, returnType, inputTypes, flags, out method);
            }
            method = null;
            return false;
        }
    }
}
