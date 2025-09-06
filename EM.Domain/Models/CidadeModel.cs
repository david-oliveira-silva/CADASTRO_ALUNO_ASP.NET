using EM.Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace EM.Domain.Models
{
    public class CidadeModel
    {
        public int cidadeID {  get; set; }

        [Required(ErrorMessage = "nome não pode ser vazio")]
        public string cidadeNome {  get; set; }

        public UF cidadeUF { get; set; }


        public CidadeModel()
        {
        }
        public CidadeModel(string cidadeNome,UF cidadeUF) {

        this.cidadeNome = cidadeNome;
        this.cidadeUF = cidadeUF;
        
        }
    }
}
