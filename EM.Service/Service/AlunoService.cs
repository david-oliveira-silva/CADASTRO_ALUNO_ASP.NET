

using EM.Domain.Enum;
using EM.Domain.Models;
using EM.Repository.Repository.Aluno;

namespace EM.Service.Service
{
    public class AlunoService
    {

        IAlunoRepository alunoRepository;

        public AlunoService(IAlunoRepository alunoRepository)
        {
            this.alunoRepository = alunoRepository;
        }

        public void cadastrarAluno(long matricula, string nomeAluno, string CPF, DateOnly dtNascimento, SexoEnum sexo, int cidadeID_)
        {


            
            if (matricula == 0)
            {
                throw new Exception("Matricula não pode ser 0");
            }

            
            if (dtNascimento > DateOnly.FromDateTime(DateTime.Today))
            {
                throw new Exception("A data de nascimento não pode ser uma data futura.");
            }

            if (cidadeID_ == null)
            {
                throw new Exception("Cidade não encontrada");
            }
            if(nomeAluno.Length < 3 || nomeAluno.Length > 100)
            {
                throw new Exception("O nome deve conter entre 3 e 100 caracteres.");
            }

            AlunoModel alunoNovo = new AlunoModel(matricula, nomeAluno.ToUpper(), CPF, sexo, dtNascimento, cidadeID_);
            alunoRepository.Cadastrar(alunoNovo);

        }

        public void deletarAluno(AlunoModel alunoModel)
        {

            if (alunoModel == null)
            {
                throw new Exception("Aluno não encontrando");
            }

            alunoRepository.Deletar(alunoModel);
        }

        public void editarAluno(AlunoModel alunoModel)
        {
            if (alunoModel == null)
            {

                throw new Exception("Aluno não encontrando");
            }
           
            if (alunoModel.dtNascimento > DateOnly.FromDateTime(DateTime.Today))
            {
                throw new Exception("A data de nascimento não pode ser uma data futura.");
            }

            if (alunoModel.cidadeID_ == null)
            {
                throw new Exception("Cidade não encontrada");
            }
            if (alunoModel.nome.Length < 3 || alunoModel.nome.Length > 100)
            {
                throw new Exception("O nome deve conter entre 3 e 100 caracteres.");
            }
            alunoRepository.Editar(alunoModel);
        }

        public List<AlunoModel> listarAlunos()
        {
            var aluno = alunoRepository.Listar().OrderBy(a => a.matricula).ToList();
            return aluno;
        }

        public AlunoModel buscarPorMatricula(long matricula)
        {
            return alunoRepository.buscarPorMatricula(matricula);
            
        }
    }
}
