using EM.Domain.Enum;
using EM.Domain.Models;
using EM.Repository.Repository.Aluno;
using EM.Repository.Repository.Cidade;

namespace EM.Service.Service
{
    public class CidadeService(ICidadeRepository cidadeRepository, IAlunoRepository alunoRepository)
    {
        private readonly ICidadeRepository cidadeRepository = cidadeRepository;

        private readonly IAlunoRepository alunoRepository = alunoRepository;

        public void CadastrarCidade(string cidadeNome, UF cidadeUF)
        {
            if (string.IsNullOrEmpty(cidadeNome))
            {
                throw new Exception("Nome não pode ser vazio");
            }

            CidadeModel novacidade = new(cidadeNome.ToUpper(), cidadeUF);
            cidadeRepository.Cadastrar(novacidade);
        }

        public void DeletarCidade(CidadeModel cidadeModel)
        {
            if (cidadeModel == null)
            {
                throw new Exception("Cidade não encontrada");
            }

            var alunoVinculado = alunoRepository.Listar().FirstOrDefault(a => a.Cidade != null &&  a.Cidade.CidadeID == cidadeModel.CidadeID);

            if (alunoVinculado != null)
            {
                throw new Exception("Não é possível excluir esta cidade. Ela está vinculada a um aluno.");
            }

            cidadeRepository.Deletar(cidadeModel);
        }

        public void EditarCidade(CidadeModel cidadeModel)
        {
            if (cidadeModel == null)
            {
                throw new Exception("Cidade não encontrada");
            }
            if (string.IsNullOrEmpty(cidadeModel.CidadeNome))
            {
                throw new Exception("Nome não pode ser vazio");
            }

            cidadeRepository.Editar(cidadeModel);
        }

        public List<CidadeModel> BuscarPorNome(string cidadeNome)
        {
            var cidade = cidadeRepository.Listar();

            if (string.IsNullOrEmpty(cidadeNome))
            {
                return cidade;
            }
            return [.. cidade.Where(c => c.CidadeNome != null && c.CidadeNome.Contains(cidadeNome.ToUpper()))];
        }

        public List<CidadeModel> ListarCidades()
        {
            var cidade = cidadeRepository.Listar();
            return [.. cidade.OrderBy(c => c.CidadeID)];
        }

        public CidadeModel? ObterPorCodigo(long cidadeID)
        {

            var cidade = ListarCidades().FirstOrDefault(a =>  a.CidadeID == cidadeID);
            return cidade;
        }
    }
}
