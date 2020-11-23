using System.ComponentModel;

namespace Remembrall.Source.Infrastructure.Interfaces
{
    [TypeConverter(typeof(EnumDescriptionTypeConverter))]
    public enum RelationshipEnumViewModel
    {
        [Description("Семья")]
        Family,
        [Description("Друзья")]
        Friends,
        [Description("Знакомые")]
        Familiar
    }
}
