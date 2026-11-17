using System.ComponentModel.DataAnnotations;

namespace GameHub.CustomValidation
{
    public class CoverSizeValidation:ValidationAttribute
    {
        private readonly int _value;
        public CoverSizeValidation(int MaxSize){ _value = MaxSize*1024*1024; }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var cover = value as IFormFile;
            if (cover == null) return ValidationResult.Success;
            if( cover.Length<=_value) return ValidationResult.Success;
            return new ValidationResult("Size Is To Large");
        }
    }

}
