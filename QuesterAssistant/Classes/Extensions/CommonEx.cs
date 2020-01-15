using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace QuesterAssistant.Classes.Extensions
{
    public static class CommonEx
    {
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