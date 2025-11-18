using EM.Domain.Enum;

namespace EM.Domain.Models
{
    public class AlunoModel : Pessoa
    {
        public long matricula { get; set; }

        public int cidadeID_ { get; set; }

        public CidadeModel? cidade { get; set; }

        public AlunoModel()
        {

        }

        public AlunoModel(long matricula, string alunoNome, string CPF, SexoEnum sexo, DateOnly? dtNascimento, int cidadeID_) : base(alunoNome, CPF, sexo, dtNascimento)
        { 
            this.matricula = matricula;
            this.cidadeID_ = cidadeID_;
        }
    }
}
