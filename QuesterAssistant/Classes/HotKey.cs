using QuesterAssistant.Classes.Common;
using QuesterAssistant.Classes.Extensions;
using System;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace QuesterAssistant.Classes
{
    [Serializable]
    public class HotKey : NotifyHashChanged, IParse<HotKey>
    {
        private static KeysConverter kc = new KeysConverter();

        [XmlText]
        public string String { get; set; } = Keys.None.ConvertToString();
        [XmlAttribute]
        public bool Enabled { get; set; } = false;
        [XmlIgnore]
        public Keys Keys => (Keys)kc.ConvertFromString(String);

        public override int GetHashCode()
        {
            return String.GetHashCode() ^ Enabled.GetHashCode();
        }

        public void Parse(HotKey source)
        {
            String = source.String;
            Enabled = source.Enabled;
        }

        public void Init() { }
    }
}
