namespace EM.Domain.Models
{
    public class ViewModel
    {
        public AlunoModel Aluno { get; set; }
        public List<CidadeModel> Cidade { get; set; } = [];
        public bool AlunoNovo { get; set; }
    }
}
