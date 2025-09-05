
using EM.Domain.Enum;
using EM.Domain.Models;
using EM.Repository.Repository.Cidade;

namespace EM.Service.Service
{
    public class CidadeService
    {
        private ICidadeRepository cidadeRepository;

        public CidadeService(ICidadeRepository cidadeRepository)
        {
            this.cidadeRepository = cidadeRepository;
        }

        public void CadastrarCidade(string cidadeNome, UF cidadeUF)
        {
            if (string.IsNullOrEmpty(cidadeNome))
            {
                throw new Exception("Nome não pode ser vazio");
            }

           


            var novacidade = new CidadeModel(cidadeNome, cidadeUF);

            cidadeRepository.Cadastrar(novacidade);
        }

        public void DeletarCidade(CidadeModel cidadeModel)
        {
            if(cidadeModel == null)
            {
                throw new Exception("Cidade não encontrada");
            }

            cidadeRepository.Deletar(cidadeModel);
        }

        public void EditarCidade(CidadeModel cidadeModel)
        {
            if(cidadeModel == null)
            {
                throw new Exception("Cidade não encontrada");
            }
             if (string.IsNullOrEmpty(cidadeModel.cidadeNome))
            {
                throw new Exception("Nome não pode ser vazio");
            }
        }

        public List<CidadeModel> buscarPorNome(string cidadeNome)
        {
            var cidade = cidadeRepository.Listar();

            if (string.IsNullOrEmpty(cidadeNome))
            {
                return cidade;
            }
            return cidade.Where(c => c.cidadeNome.Contains(cidadeNome.ToUpper())).ToList();
           
        
        }
        public List<CidadeModel> ListarCidades()
        {
            var cidade = cidadeRepository.Listar();
            return cidade.OrderBy(c=>c.cidadeID).ToList();
        }
    }
}
