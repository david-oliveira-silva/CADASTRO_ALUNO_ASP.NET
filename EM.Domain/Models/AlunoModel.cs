using EM.Domain.Enum;

namespace EM.Domain.Models
{
    public class AlunoModel : Pessoa
    {
        public long Matricula { get; set; }
        public int CidadeID_ { get; set; }
        public CidadeModel? Cidade { get; set; }

        public AlunoModel()
        {

        }

        public AlunoModel(long matricula, string alunoNome, string? CPF, SexoEnum sexo, DateOnly? dtNascimento, int cidadeID_) : base(alunoNome, CPF, sexo, dtNascimento)
        { 
            this.Matricula = matricula;
            this.CidadeID_ = cidadeID_;
        }
    }
}
