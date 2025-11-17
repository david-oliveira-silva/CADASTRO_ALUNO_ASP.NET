using EM.Domain.Models;
using EM.Service.Service;
using EM.Service.Service.Relatorios;
using Microsoft.AspNetCore.Mvc;
using static EM.Service.Service.Relatorios.RelatorioAlunos;

namespace EM.Web.Controllers
{
    public class AlunoController : Controller
    {

        private readonly AlunoService alunoService;
        private readonly CidadeService cidadeService;
        public AlunoController(AlunoService alunoService, CidadeService cidadeService) {

            this.alunoService = alunoService;
            this.cidadeService = cidadeService;
        }

        [HttpGet]
        public IActionResult UpsertAluno(long? matricula)
        {

            AlunoModel aluno;

            ViewModel viewModel = new ViewModel();

            if (matricula.HasValue)
            {

                aluno = alunoService.obterPorMatricula(matricula.Value);

                if (aluno == null)
                {

                    TempData["Erro"] = "Aluno não encontrado";
                    return RedirectToAction("ListarAlunos");
                }

                viewModel.AlunoNovo = false;
               
            }
            else
            {

                aluno = new AlunoModel();

                aluno.matricula = alunoService.ObterProximaMatriculaDisponivel();

                viewModel.AlunoNovo = true;

            }

               viewModel.Aluno = aluno;
               viewModel.Cidade = cidadeService.ListarCidades();
            
            return View(viewModel);



        }


        [HttpPost]
        public IActionResult CadastrarAluno(ViewModel viewModel) {

            try
            {

                viewModel.AlunoNovo = true;

                alunoService.cadastrarAluno(viewModel.Aluno.matricula, viewModel.Aluno.nome, viewModel.Aluno.CPF, viewModel.Aluno.dtNascimento, viewModel.Aluno.sexo, viewModel.Aluno.cidadeID_);
                TempData["Sucesso"] = "Cidade cadastrada com sucesso";

            }
            catch (Exception ex) {

                viewModel.AlunoNovo = true;
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
                viewModel.AlunoNovo = false;
                alunoService.editarAluno(viewModel.Aluno);
                TempData["Sucesso"] = "Aluno editado com sucesso";

            }
            catch (Exception ex)
            {
                viewModel.AlunoNovo = false;

                TempData["Erro"] = ex.Message;
                viewModel.Cidade = cidadeService.ListarCidades();

                return View("UpsertAluno", viewModel);
            }
            return RedirectToAction("ListarAlunos");



        }

        [HttpGet]

        public IActionResult DeletarAluno(long? matricula) {

            var aluno = alunoService.obterPorMatricula(matricula.Value);

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

     

        [HttpGet]
        public IActionResult GerarPdfAlunos()
        {
            
            List<AlunoModel> alunos = alunoService.listarAlunos();

            
            var gerador = new PdfGenerator();
            byte[] pdfBytes = gerador.GerarRelatorioAlunos(alunos);

            
            return File(
                fileContents: pdfBytes,
                contentType: "application/pdf",
                fileDownloadName: "Relatorio_Alunos.pdf"
          
            );
        }

    }
}

