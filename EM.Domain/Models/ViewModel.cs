

namespace EM.Domain.Models
{
    public class ViewModel
    {
        public AlunoModel Aluno { get; set; }

        public List<CidadeModel> Cidade { get; set; } = new List<CidadeModel>();

        public bool AlunoNovo { get; set; }
    }
}
