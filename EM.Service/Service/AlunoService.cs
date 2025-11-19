using EM.Domain.Enum;
using EM.Domain.Extensions;
using EM.Domain.Models;
using EM.Repository.Repository.Aluno;


namespace EM.Service.Service
{
    public class AlunoService(IAlunoRepository alunoRepository)
    {
        private readonly IAlunoRepository alunoRepository = alunoRepository;

        public void CadastrarAluno(long matricula, string nomeAluno, string ?CPF, DateOnly? dtNascimento, SexoEnum sexo, int cidadeID_)
        {
            var todosAlunos = alunoRepository.Listar();

            long proximoNumero = todosAlunos.Count != 0 ? matricula : matricula;
            long ultimaMatricula = todosAlunos.Count != 0 ? todosAlunos.Max(m => m.Matricula) : 0;

            if (string.IsNullOrEmpty(nomeAluno))
            {
                throw new Exception("Nome não pode ser vazio");
            }

            if (matricula == 0)
            {
                throw new Exception("Matrícula não pode ser 0");
            }
            if (matricula <= ultimaMatricula)
            {
                throw new Exception($"A matrícula deve ser maior que a última cadastrada");
            }


            if (dtNascimento > DateOnly.FromDateTime(DateTime.Today))
            {
                throw new Exception("A data de nascimento não pode ser uma data futura.");
            }

            if (nomeAluno.Length < 3 || nomeAluno.Length > 100)
            {
                throw new Exception("O nome deve conter entre 3 e 100 caracteres.");
            }

            if (!string.IsNullOrWhiteSpace(CPF))
            {
                CPF = CPF.Replace(".", "").Replace("-", "");
                if (!CPF.IsCPF())
                {
                    throw new Exception("CPF está inválido.");
                }
            }

            if (dtNascimento == null)
            {
                throw new Exception("Data de nascimento não pode ser vazia");
            }

            if (sexo == 0)
            {
                throw new Exception("O campo Sexo é obrigatório.");
            }

            AlunoModel alunoNovo = new (proximoNumero, nomeAluno.ToUpper(), CPF, sexo, dtNascimento, cidadeID_);
            alunoRepository.Cadastrar(alunoNovo);

        }
        public void EditarAluno(AlunoModel alunoModel)
        {
            if (alunoModel == null)
            {

                throw new Exception("Aluno não encontrando");
            }

            if (string.IsNullOrEmpty(alunoModel.Nome))
            {
                throw new Exception("Nome não pode ser vazio");
            }

            
            if (alunoModel.DtNascimento > DateOnly.FromDateTime(DateTime.Today))
            {
                throw new Exception("A data de nascimento não pode ser uma data futura.");
            }

         
            if (alunoModel.Nome.Length < 3 || alunoModel.Nome.Length > 100)
            {
                throw new Exception("O nome deve conter entre 3 e 100 caracteres.");
            }

            if (!string.IsNullOrWhiteSpace(alunoModel.CPF))
            {
                alunoModel.CPF = alunoModel.CPF.Replace(".", "").Replace("-", "");
                if (!alunoModel.CPF.IsCPF())
                {
                    throw new Exception("CPF está inválido.");
                }
            }
            if (alunoModel.DtNascimento == null)
            {
                throw new Exception("Data de nascimento não pode ser vazia");
            }
            if (alunoModel.Sexo == 0)
            {
                throw new Exception("O campo Sexo é obrigatório.");
            }

            alunoModel.Nome = alunoModel.Nome.ToUpper();
            alunoRepository.Editar(alunoModel);
        }
        public void DeletarAluno(AlunoModel alunoModel)
        {
            if (alunoModel == null)
            {
                throw new Exception("Aluno não encontrando");
            }
            alunoRepository.Deletar(alunoModel);
        }

        public List<AlunoModel> ListarAlunos()
        {
            var aluno = alunoRepository.Listar().OrderBy(a => a.Matricula).ToList();
            return aluno;
        }

        public List<AlunoModel> BuscarPorMatricula(long matricula)
        {
            return [.. alunoRepository.Listar().Where(a => a.Matricula == matricula)];
        }

        public List<AlunoModel> BuscarPorNome(string alunoNome)
        {
            var termoDeBuscaUpper = alunoNome.ToUpper();
            if (string.IsNullOrEmpty(alunoNome))
            {
                return [];
            }
            return [.. alunoRepository.Listar().Where(a => a.Nome.Contains(termoDeBuscaUpper))];
        }

        public AlunoModel? ObterPorMatricula(long matricula)
        {

            var aluno = ListarAlunos().FirstOrDefault(a => a.Matricula == matricula);
            return aluno;
        }

        public long ObterProximaMatriculaDisponivel()
        {
            var alunos = alunoRepository.Listar();
            if (alunos == null || alunos.Count == 0)
            {
                return 1;
            }
            return alunos.Max(a => a.Matricula) + 1;
        }
    }
}
