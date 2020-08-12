using System.Collections.Generic;

namespace QuesterAssistant.Classes.ItemFilter
{
    public class ItemFilterCollection
    {
        public string Description { get; set; } = string.Empty;
        public List<string> Values { get; set; } = new List<string>();
    }
}