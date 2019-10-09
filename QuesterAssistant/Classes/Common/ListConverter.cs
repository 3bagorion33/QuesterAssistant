using System.Collections;
using System.ComponentModel;

namespace QuesterAssistant.Classes.Common
{
    internal class ListConverter : TypeConverter
    {
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context) => true;
        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context) => true;

        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            if (context.Instance is IListConverter converter)
            {
                return new StandardValuesCollection(converter.ListConverterData);
            }
            return new StandardValuesCollection(null);
        }
    }

    internal interface IListConverter
    {
        IList ListConverterData { get; }
    }
}
