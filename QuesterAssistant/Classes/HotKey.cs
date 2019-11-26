using QuesterAssistant.Classes.Common;
using QuesterAssistant.Classes.Extensions;
using System;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace QuesterAssistant.Classes
{
    [Serializable]
    public class HotKey : OverrideHash, IParse<HotKey>
    {
        private static KeysConverter kc = new KeysConverter();

        [XmlText, HashInclude]
        public string String { get; set; } = Keys.None.ConvertToString();
        [XmlAttribute, HashInclude]
        public bool Enabled { get; set; }
        [XmlIgnore]
        public Keys Keys => (Keys)kc.ConvertFromString(String);

        public void Parse(HotKey source)
        {
            String = source.String;
            Enabled = source.Enabled;
        }

        public void Init() { }
    }
}
