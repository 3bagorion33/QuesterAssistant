using System;
using System.IO;
using QuesterAssistant.Panels;

namespace Launcher.Classes
{
    public class XmlSerializer
    {
        public static void Serialize(string path, object Object)
        {
            try
            {
                File.Delete(path);
            }
            catch (Exception ex)
            {
                QMessageBox.ShowError(ex.ToString());
            }
            try
            {
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    new System.Xml.Serialization.XmlSerializer(Object.GetType()).Serialize(fileStream, Object);
                }
            }
            catch (Exception ex)
            {
                QMessageBox.ShowError(ex.ToString());
            }
        }

        public static T Deserialize<T>(string path)
        {
            try
            {
                using (var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    return (T)new System.Xml.Serialization.XmlSerializer(typeof(T)).Deserialize(fileStream);
                }
            }
            catch (Exception ex)
            {
                QMessageBox.ShowError(ex.ToString());
            }
            return Activator.CreateInstance<T>();
        }
    }
}