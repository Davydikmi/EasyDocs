using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyDocs
{
    // Класс для атрибута NotEqualTo
    public class NotEqualToAttribute : ValidationAttribute
    {
        private readonly string _disallowedValue;

        public NotEqualToAttribute(string disallowedValue)
        {
            _disallowedValue = disallowedValue;
        }

        protected override System.ComponentModel.DataAnnotations.ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is string stringValue && stringValue == _disallowedValue)
            {
                return new System.ComponentModel.DataAnnotations.ValidationResult($"Поле \"{_disallowedValue}\" должно быть обязательно заполнено.");
            }
            return System.ComponentModel.DataAnnotations.ValidationResult.Success;
        }
    }
}
