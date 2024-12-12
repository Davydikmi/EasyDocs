using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EasyDocs
{
    // Класс для атрибута NotEqualTo
    public class NotEqualToAttribute : ValidationAttribute
    {
        private readonly string disallowedValue;

        public NotEqualToAttribute(string disallowedValue)
        {
            this.disallowedValue = disallowedValue;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is string stringValue && stringValue == disallowedValue)
            {
                return new ValidationResult($"Поле \"{disallowedValue}\" должно быть обязательно заполнено.");
            }
            return ValidationResult.Success;
        }
    }

    //Класс для атрибута LettersOnly
    public class LettersOnlyAttribute : ValidationAttribute
    {
        private readonly string disallowedValue;

        public LettersOnlyAttribute(string disallowedValue)
        {
            this.disallowedValue = disallowedValue;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null) return ValidationResult.Success;

            string input = value.ToString();

            if (Regex.IsMatch(input, @"^[a-zA-Zа-яА-Яё ]+$")) return ValidationResult.Success;
            else return new ValidationResult($"Поле \"{disallowedValue}\" может содержать только буквы A-Z, a-z, А-Я, а-я и пробел.");
            
        }
    }
    //Класс для атрибута ValidDate
    public class ValidDateAttribute : ValidationAttribute
    {
        private readonly string dateFormat;

        public ValidDateAttribute(string dateFormat = "dd.MM.yyyy")
        {
            this.dateFormat = dateFormat;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
            {
                return new ValidationResult($"Поле \"{validationContext.DisplayName}\" должно содержать дату.");
            }

            if (!DateTime.TryParseExact(value.ToString(), dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedDate))
            {
                return new ValidationResult($"Поле \"{validationContext.DisplayName}\" должно содержать корректную дату в формате {dateFormat}.");
            }

            return ValidationResult.Success;
        }
    }
}
