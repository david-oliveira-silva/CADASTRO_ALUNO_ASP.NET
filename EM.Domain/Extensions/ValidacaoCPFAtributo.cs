
namespace EM.Domain.Extensions
{
    using System.ComponentModel.DataAnnotations;

   

    namespace EM.Domain.Extensions 
    {
   
        public class ValidacaoCPFAtributo : ValidationAttribute
        {

            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {

                if (value == null)
                {
                    return ValidationResult.Success;
                }
                if (value is string cpf)
                {
                    if (string.IsNullOrWhiteSpace(cpf))
                    {
                        return ValidationResult.Success;
                    }
                
                    if (cpf.IsCPF())
                    {
                        return ValidationResult.Success;
                    }
                }
                return new ValidationResult(ErrorMessage, new[] { validationContext.MemberName });
            }
        }
    }
}
