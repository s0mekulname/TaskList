using System;
using System.Collections.Generic;
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

            var eValues = Enum.GetValues(EnumType);
            var enumValueDescriptions = new List<EnumValueDescription>();
            foreach (Model.TaskStatus v in eValues)
            {
                enumValueDescriptions.Add(new EnumValueDescription(v));
            }
            return enumValueDescriptions;
        }


    }
}
