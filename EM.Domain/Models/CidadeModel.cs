using EM.Domain.Enum;

namespace EM.Domain.Models
{
    public class CidadeModel
    {
        public int cidadeID {  get; set; }
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
