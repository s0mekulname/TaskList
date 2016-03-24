using System.Threading.Tasks;

namespace MarkupExtensions
{
    public class EnumValueDescription
    {
        public EnumValueDescription(TaskStatus status)
        {
            
        }
        public object EnumValue { get; }
        public string DisplayString { get; }
    }
}
