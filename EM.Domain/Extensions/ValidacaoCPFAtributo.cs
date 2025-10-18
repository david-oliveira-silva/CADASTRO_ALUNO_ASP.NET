using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EM.Domain.Extensions
{
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    // Não precisa de 'using EM.Domain.Extensions' se esta classe estiver no mesmo namespace.

    namespace EM.Domain.Extensions // <--- Localização e Namespace
    {
        // CRIE ESTA CLASSE!
        public class ValidacaoCPFAtributo : ValidationAttribute
        {
            // ... (o método IsValid)
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                if (value is string cpf)
                {
                    if (string.IsNullOrWhiteSpace(cpf))
                    {
                        return ValidationResult.Success;
                    }
                    // O método IsCPF é acessível aqui.
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
