using System.ComponentModel;

namespace DataStorage.Source.Entity
{
    public enum RelationshipEnum
    {
        [Description("Семья")]
        Family,
        [Description("Друзья")] 
        Friends,
        [Description("Знакомые")]
        Familiar
    }
}
