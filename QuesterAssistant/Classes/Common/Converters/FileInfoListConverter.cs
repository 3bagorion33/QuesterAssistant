using System;
using System.ComponentModel;
using System.Globalization;
using System.IO;

namespace QuesterAssistant.Classes.Common.Converters
{
    internal class FileInfoListConverter : ListConverter
    {
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (value is FileInfo fileInfo && context.Instance is IListConverter)
                return fileInfo.FullName.Substring(Core.ProfilesPath.Length + 1);
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}
