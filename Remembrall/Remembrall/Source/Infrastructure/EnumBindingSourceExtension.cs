using System;
using System.Windows.Markup;

namespace Remembrall.Source.Infrastructure
{
    public class EnumBindingSourceExtension : MarkupExtension
    {
        private Type _enumType;


        public EnumBindingSourceExtension() { }

        public EnumBindingSourceExtension(Type enumType)
        {
            _enumType = enumType;
        }


        public Type EnumType
        {
            get => _enumType;
            set
            {
                if (_enumType == value)
                    return;

                if (value != null)
                {
                    var enumType = Nullable.GetUnderlyingType(value) ?? value;
                    if (!enumType.IsEnum)
                        throw new ArgumentException("Type is not enum");
                }
                _enumType = value;
            }
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (_enumType == null)
                throw new ArgumentException("The EnumType must be specified.");

            var actualEnumType = Nullable.GetUnderlyingType(_enumType) ?? _enumType;
            var enumValues = Enum.GetValues(actualEnumType);

            if (actualEnumType == this._enumType)
                return enumValues;

            var tempArray = Array.CreateInstance(actualEnumType, enumValues.Length + 1);
            enumValues.CopyTo(tempArray, 1);
            return tempArray;
        }
    }
}
