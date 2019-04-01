using Astral;
using QuesterAssistant.Panels;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace QuesterAssistant.Classes.Common
{
    internal class BinFile
    {
        public static void Save (object o, string s)
        {
            FileStream fileStream = new FileStream(s, FileMode.Create);
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            binaryFormatter.AssemblyFormat = System.Runtime.Serialization.Formatters.FormatterAssemblyStyle.Simple;
            try
            {
                binaryFormatter.Serialize(fileStream, o);
            }
            catch (Exception ex)
            {
                ErrorBox.Show(ex.ToString());
                Logger.WriteLine(ex.ToString());
            }
            finally
            {
                fileStream.Close();
            }
        }

        public static T Load<T> (string s)
        {
            FileStream fileStream = new FileStream(s, FileMode.Open);
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            binaryFormatter.AssemblyFormat = System.Runtime.Serialization.Formatters.FormatterAssemblyStyle.Simple;
            T result;
            try
            {
                result = (T)binaryFormatter.Deserialize(fileStream);
            }
            catch (Exception ex)
            {
                Logger.WriteLine(ex.ToString());
                result = Activator.CreateInstance<T>();
            }
            finally
            {
                fileStream.Close();
            }
            return result;
        }
    }
}
