using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace QuesterAssistant.Classes.Extensions
{
    public static class CommonEx
    {
        /// <summary>
        /// Check <see cref="@int"/> for zero and returns <see cref="value"/> if true
        /// </summary>
        public static int CheckZero(this int @int, int value) => DynCheckZero(@int, value);
        /// <summary>
        /// Check <see cref="@int"/> for zero and returns <see cref="value"/> if true
        /// </summary>
        public static uint CheckZero(this uint @uint, uint value) => DynCheckZero(@uint, value);
        private static dynamic DynCheckZero(dynamic @this, dynamic value)
        {
            return @this == 0 ? value : @this;
        }

        /// <summary>
        /// Check <see cref="@int"/> for zero and returns <see cref="value"/> if true
        /// </summary>
        public static int CheckNegative(this int @int, int value) => DynCheckNegative(@int, value);
        private static dynamic DynCheckNegative(dynamic @this, dynamic value)
        {
            return @this < 0 ? value : @this;
        }

        public static T SerializedClone<T>(this T toClone)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Context = new StreamingContext(StreamingContextStates.Clone);
                formatter.Serialize(ms, toClone);
                ms.Position = 0;
                return (T) formatter.Deserialize(ms);
            }
        }

        public static T MarshaledClone<T>(this T toClone)
        {
            return (T) (toClone as object).MarshaledClone();
        }

        public static object MarshaledClone(this object toClone)
        {
            IntPtr p = Marshal.AllocHGlobal(Marshal.SizeOf(toClone));
            try
            {
                Marshal.StructureToPtr(toClone, p, false);
                return Marshal.PtrToStructure(p, toClone.GetType());
            }
            finally
            {
                Marshal.FreeHGlobal(p);
            }
        }

        public static object MemberwiseClone(this object input)
        {
            return CopyHelper.CreateDeepCopy(input);
        }
    }
}