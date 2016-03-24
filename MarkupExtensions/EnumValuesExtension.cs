using System;
using System.Windows.Markup;

namespace MarkupExtensions
{
    public class EnumValuesExtension : MarkupExtension
    {
        public EnumValuesExtension()
        {
            
        }

        public EnumValuesExtension(Type enumType)
        {
            EnumType = enumType;
        }

        public  Type EnumType { get; set; }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (EnumType == null)
            {
                throw new ArgumentException("The enumeration's type is not set.");
            }
            return Enum.GetValues(EnumType);
        }
    }
}
