using System.ComponentModel.DataAnnotations;

namespace GameHub.CustomValidation
{
    public class CoverTypeValidation:ValidationAttribute
    {
        private readonly string _type;
        public CoverTypeValidation(string AvaliableExtention) { _type = AvaliableExtention; }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var Cover=value as IFormFile;
            if (Cover is null) return ValidationResult.Success;
            
            if (Cover != null)
            {
                var extention = Path.GetExtension(Cover.FileName);
               if(_type.Split(",").Contains(extention,StringComparer.OrdinalIgnoreCase)) return ValidationResult.Success;
            }
            return new ValidationResult("Not Supported Type");
        }
    }
}
