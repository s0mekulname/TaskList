using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace MarkupExtensions
{
    public class EnumValueDescription
    {
        public EnumValueDescription(Model.TaskStatus status)
        {
            EnumValue = status;
            DisplayString =  status
                .GetType()
                .GetMember(status.ToString())
                .First()
                .GetCustomAttribute<DisplayAttribute>()
                .GetName();
        }

        public object EnumValue { get; }

        public string DisplayString { get; }
    }
}
