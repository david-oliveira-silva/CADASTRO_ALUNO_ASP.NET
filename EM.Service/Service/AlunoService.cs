

using EM.Domain.Enum;
using EM.Domain.Extensions;
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

            long proximoNumero = alunoRepository.Listar().Any() ? alunoRepository.Listar().Max(a => a.matricula) + 1 : 1;

            if (string.IsNullOrEmpty(nomeAluno)) {
                throw new Exception("Nome não pode ser vazio");
            }

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

            if (!CPF.IsCPF())
            {
                throw new Exception("CPF está invalido");
            }

           

            
            AlunoModel alunoNovo = new AlunoModel(proximoNumero, nomeAluno.ToUpper(), CPF, sexo, dtNascimento, cidadeID_);
            alunoRepository.Cadastrar(alunoNovo);

        }

        public void editarAluno(AlunoModel alunoModel)
        {
            if (alunoModel == null)
            {

                throw new Exception("Aluno não encontrando");
            }

            if (string.IsNullOrEmpty(alunoModel.nome))
            {
                throw new Exception("Nome não pode ser vazio");
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

            if (!alunoModel.CPF.IsCPF())
            {
                throw new Exception("CPF está invalido");
            }
            alunoRepository.Editar(alunoModel);
        }

        public void deletarAluno(AlunoModel alunoModel)
        {

            if (alunoModel == null)
            {
                throw new Exception("Aluno não encontrando");
            }

            alunoRepository.Deletar(alunoModel);
        }

        


        public List<AlunoModel> listarAlunos()
        {
            var aluno = alunoRepository.Listar().OrderBy(a => a.matricula).ToList();
            return aluno;
        }

        public List<AlunoModel> buscarPorMatricula(long matricula)
        {
            return alunoRepository.Listar().Where(a => a.matricula == matricula).ToList();
        }

        public List<AlunoModel> buscarPorNome(string alunoNome)
        {
            var termoDeBuscaUpper = alunoNome.ToUpper();
            if (string.IsNullOrEmpty(alunoNome))
            {
                return new List<AlunoModel>(); 
            }
            return alunoRepository.Listar().Where(a => a.nome.Contains(termoDeBuscaUpper)).ToList();
        }

        public long ObterProximaMatriculaDisponivel()
        {
            var alunos = alunoRepository.Listar();
            if (alunos == null || !alunos.Any())
            {
                return 1;
            }
            return alunos.Max(a => a.matricula) + 1;
        }

    }
}
