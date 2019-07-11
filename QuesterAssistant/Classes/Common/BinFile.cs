using Astral;
using QuesterAssistant.Panels;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary;

namespace QuesterAssistant.Classes.Common
{
    internal static class BinFile
    {
        private static readonly BinaryFormatter binaryFormatter = new BinaryFormatter { AssemblyFormat = FormatterAssemblyStyle.Simple };

        public static void Save (object o, string s)
        {
            using (FileStream fileStream = new FileStream(s, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                try
                {
                    binaryFormatter.Serialize(fileStream, o);
                }
                catch (Exception ex)
                {
                    ErrorBox.Show(ex.ToString());
                    Logger.WriteLine(ex.ToString());
                }
            }
        }

        public static T Load<T> (string s)
        {
            T result;
            using (FileStream fileStream = new FileStream(s, FileMode.Open))
            {
                try
                {
                    result = (T)binaryFormatter.Deserialize(fileStream);
                }
                catch (Exception ex)
                {
                    Logger.WriteLine(ex.ToString());
                    result = Activator.CreateInstance<T>();
                }
            }
            return result;
        }
    }
}
