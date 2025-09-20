using EM.Domain.Models;
using EM.Service.Service;
using Microsoft.AspNetCore.Mvc;

namespace EM.Web.Controllers
{
    public class AlunoController : Controller
    {

        AlunoService alunoService;
        CidadeService cidadeService;
        public AlunoController(AlunoService alunoService, CidadeService cidadeService) {

            this.alunoService = alunoService;
            this.cidadeService = cidadeService;
        }

        [HttpGet]
        public IActionResult UpsertAluno(long? matricula)
        {

            AlunoModel aluno;

            if (matricula.HasValue)
            {

                aluno = alunoService.listarAlunos().FirstOrDefault(a => a.matricula == matricula);

                if (aluno == null)
                {

                    TempData["Erro"] = "Aluno não encontrado";
                    return RedirectToAction("ListarAlunos");
                }

            }
            else
            {

                aluno = new AlunoModel();


            }
            var viewModel = new ViewModel()
            {
                Aluno = aluno,
                Cidade = cidadeService.ListarCidades()
            };
            return View(viewModel);



        }


        [HttpPost]
        public IActionResult CadastrarAluno(ViewModel viewModel) {

            try
            {


                alunoService.cadastrarAluno(viewModel.Aluno.matricula, viewModel.Aluno.nome, viewModel.Aluno.CPF, viewModel.Aluno.dtNascimento, viewModel.Aluno.sexo, viewModel.Aluno.cidadeID_);
                TempData["Sucesso"] = "Cidade cadastrada com sucesso";

            }
            catch (Exception ex) {

                TempData["Erro"] = ex.Message;
                viewModel.Cidade = cidadeService.ListarCidades();

                return View("UpsertAluno", viewModel);
            }

            return RedirectToAction("ListarAlunos");

        }

        [HttpPost]
        public IActionResult EditarAluno(ViewModel viewModel)
        {

            try
            {
                alunoService.editarAluno(viewModel.Aluno);
                TempData["Sucesso"] = "Aluno editado com sucesso";

            }
            catch (Exception ex)
            {
                TempData["Erro"] = ex.Message;
                viewModel.Cidade = cidadeService.ListarCidades();

                return View("UpsertAluno", viewModel);
            }
            return RedirectToAction("ListarAlunos");



        }

        [HttpGet]

        public IActionResult DeletarAluno(long? matricula) {

            var aluno = alunoService.listarAlunos().FirstOrDefault(a => a.matricula == matricula);

            ViewModel viewModel = new ViewModel
            {

                Aluno = aluno,
                Cidade = cidadeService.ListarCidades()
            };

            return View(viewModel);
        
        }
        [HttpPost]
        public IActionResult DeletarAluno(ViewModel viewModel) { 
        
            try
            {

                alunoService.deletarAluno(viewModel.Aluno);
                TempData["Sucesso"] = "Aluno deletado com sucesso";

            }
            catch (Exception ex) {

                TempData["ErroDeletar"] = ex.Message;
                return View(viewModel);
            }
            return RedirectToAction("ListarAlunos");
        
        }


        [HttpGet]
                public IActionResult ListarAlunos(long matricula,string alunoNome)
                {

            ViewBag.Matricula = matricula;
            ViewBag.Nome = alunoNome;

            List<AlunoModel> alunos = new List<AlunoModel>();
            if (!string.IsNullOrEmpty(alunoNome))
            {

                alunos = alunoService.buscarPorNome(alunoNome).ToList();

            }
            else if (matricula != 0) {


                alunos = alunoService.buscarPorMatricula(matricula).ToList();

            }
            else
            {
                alunos = alunoService.listarAlunos();
            }

            ViewBag.Pesquisa = alunoNome;
           
                   return View(alunos);

                }

    }
}

