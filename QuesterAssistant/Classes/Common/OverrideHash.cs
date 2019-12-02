using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace QuesterAssistant.Classes.Common
{
    public abstract class OverrideHash : IOverrideHash
    {
        private readonly IEnumerable<object> hashObjects;

        protected OverrideHash()
        {
            hashObjects = GetType()
                .GetRuntimeProperties()
                .Where(member =>
                    member.GetCustomAttribute<HashIncludeAttribute>() != null &&
                    member.GetCustomAttribute<HashIncludeAttribute>().Match(new HashIncludeAttribute()))
                .Select(member => member.GetValue(this));
        }

        public override int GetHashCode()
        {
            return hashObjects.GetSafeHashCode();
        }
    }

    interface IOverrideHash
    {
        int GetHashCode();
    }

    public static class OverrideHashExt
    {
        public static int GetSafeHashCode<T>(this T @this)
        {
            if (@this == null) return 0;
            if (@this is string) return @this.GetHashCode();
            if (@this is IEnumerable @enum)
            {
                int hash = 0;
                int i = 0;
                foreach (var item in @enum)
                {
                    i++;
                    hash ^= i * item.GetSafeHashCode();
                }
                return hash;
            }
            return @this is IOverrideHash overrideHash ? overrideHash.GetHashCode() : @this.GetHashCode();
        }
    }
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class HashIncludeAttribute : Attribute { }
}