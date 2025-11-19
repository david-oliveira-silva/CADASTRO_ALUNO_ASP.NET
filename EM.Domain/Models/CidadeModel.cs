using EM.Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace EM.Domain.Models
{
    public class CidadeModel
    {
        public int CidadeID { get; set; }

        [Required(ErrorMessage = "nome não pode ser vazio")]
        public string? CidadeNome { get; set; }

        public UF CidadeUF { get; set; }
        public CidadeModel()
        {
        }
        public CidadeModel(string cidadeNome, UF cidadeUF)
        {
            this.CidadeNome = cidadeNome;
            this.CidadeUF = cidadeUF;
        }
    }
}
