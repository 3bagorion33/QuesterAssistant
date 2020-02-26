using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace QuesterAssistant.Classes.Common.Converters
{
    internal class FileFullNameListConverter : ListConverter
    {
        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            if (context.Instance is IListConverter converter)
                return new StandardValuesCollection((converter.ListConverterData as List<string>)
                    .Select(f => f.Substring(Core.ProfilesPath.Length + 1)).ToList());
            return new StandardValuesCollection(null);
        }
    }
}