using EM.Domain.Models;
using EM.Service.Service;
using Microsoft.AspNetCore.Mvc;
using static EM.Service.Service.Relatorios.RelatorioAlunos;

namespace EM.Web.Controllers
{
    public class AlunoController(AlunoService alunoService, CidadeService cidadeService) : Controller
    {
        private readonly AlunoService alunoService = alunoService;
        private readonly CidadeService cidadeService = cidadeService;

        [HttpGet]
        public IActionResult UpsertAluno(long? matricula)
        {
            AlunoModel? aluno;

            ViewModel viewModel = new();

            if (matricula.HasValue)
            {
                aluno = alunoService.ObterPorMatricula(matricula.Value);

                if (aluno == null)
                {

                    TempData["Erro"] = "Aluno não encontrado";
                    return RedirectToAction("ListarAlunos");
                }

                viewModel.AlunoNovo = false;
            }
            else
            {
                aluno = new AlunoModel
                {
                    Matricula = alunoService.ObterProximaMatriculaDisponivel()
                };
                viewModel.AlunoNovo = true;
            }

            viewModel.Aluno = aluno;
            viewModel.Cidade = cidadeService.ListarCidades();

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult CadastrarAluno(ViewModel viewModel)
        {
            if (viewModel.Aluno == null)
            {
                TempData["Erro"] = "Informações do aluno ausentes ou inválidas.";
                return View(viewModel);
            }
          
            try
            {
                viewModel.AlunoNovo = true;

                alunoService.CadastrarAluno(matricula: viewModel.Aluno.Matricula, nomeAluno: viewModel.Aluno.Nome, CPF: viewModel.Aluno.CPF, dtNascimento: viewModel.Aluno.DtNascimento, sexo: viewModel.Aluno.Sexo, cidadeID_: viewModel.Aluno.CidadeID_);
                TempData["Sucesso"] = "Cidade cadastrada com sucesso";
            }
            catch (Exception ex)
            {
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
            if (viewModel.Aluno == null)
            {
                TempData["Erro"] = "Informações do aluno ausentes ou inválidas.";
                return View(viewModel);
            }
            try
            {
                viewModel.AlunoNovo = false;
                alunoService.EditarAluno(viewModel.Aluno);
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
        public IActionResult DeletarAluno(long? matricula)
        {
            if (matricula.HasValue)
            {
                var aluno = alunoService.ObterPorMatricula(matricula.Value);

                ViewModel viewModel = new()
                {
                    Aluno = aluno,
                    Cidade = cidadeService.ListarCidades()
                };
                return View(viewModel);
            }
            return RedirectToAction("ListarAlunos");
        }

        [HttpPost]
        public IActionResult DeletarAluno(ViewModel viewModel)
        {

            if (viewModel.Aluno == null)
            {
                TempData["Erro"] = "Informações do aluno ausentes ou inválidas.";
                return View(viewModel);
            }
            try
            {
                alunoService.DeletarAluno(viewModel.Aluno);
                TempData["Sucesso"] = "Aluno deletado com sucesso";
            }
            catch (Exception ex)
            {
                TempData["ErroDeletar"] = ex.Message;
                return View(viewModel);
            }
            return RedirectToAction("ListarAlunos");
        }

        [HttpGet]
        public IActionResult ListarAlunos(long matricula, string alunoNome)
        {
            ViewBag.Matricula = matricula;
            ViewBag.Nome = alunoNome;

            List<AlunoModel> alunos;

            if (!string.IsNullOrEmpty(alunoNome))
            {
                alunos = [.. alunoService.BuscarPorNome(alunoNome)];
            }
            else if (matricula != 0)
            {
                alunos = [.. alunoService.BuscarPorMatricula(matricula)];
            }
            else
            {
                alunos = alunoService.ListarAlunos();
            }
            ViewBag.Pesquisa = alunoNome;

            return View(alunos);
        }

        [HttpGet]
        public IActionResult GerarPdfAlunos()
        {
            List<AlunoModel> alunos = alunoService.ListarAlunos();

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

