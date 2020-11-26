using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace MOTUS.View
{
    public class EnumBindingSourceExtension : MarkupExtension
    {
        public Type EnumType { get; private set; }

        public EnumBindingSourceExtension(Type enumtype)
        {
            if (enumtype == null || !enumtype.IsEnum)
            {
                throw new Exception("Argument is null or not an Enum!");
            }
            EnumType = enumtype;
        }


        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return Enum.GetValues(EnumType);        
        }
    }
}
