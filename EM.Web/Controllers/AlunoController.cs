using EM.Domain.Models;
using EM.Service.Service;
using Microsoft.AspNetCore.Mvc;

namespace EM.Web.Controllers
{
    public class AlunoController : Controller
    {

        AlunoService alunoService;
        CidadeService cidadeService;
        public AlunoController(AlunoService alunoService,CidadeService cidadeService) {

            this.alunoService = alunoService;
            this.cidadeService = cidadeService;
        }

        [HttpGet]
        public IActionResult UpsertAluno(long? matricula)
        {

            AlunoModel aluno;

            if (matricula.HasValue) {

                aluno = alunoService.listarAlunos().FirstOrDefault(a => a.matricula == matricula);

                if (aluno == null)
                {

                    TempData["Erro"] = "Aluno não encontrado";
                    return RedirectToAction("ListarAlunos");
                }
                
            }
            else{

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
        public IActionResult UpsertAluno(ViewModel viewModel)
        {
            try {
                var alunoExistente = alunoService.listarAlunos().FirstOrDefault(a => a.matricula == viewModel.Aluno.matricula);

                if (alunoExistente == null) {
                    alunoService.cadastrarAluno(viewModel.Aluno.matricula, viewModel.Aluno.nome, viewModel.Aluno.CPF, viewModel.Aluno.dtNascimento, viewModel.Aluno.sexo, viewModel.Aluno.cidadeID_);
                    TempData["Sucesso"] = "Aluno cadastrado com sucesso";
                   

                }
                else
                {
                    alunoService.editarAluno(viewModel.Aluno);
                }
            }
            catch(Exception ex)
            {
                TempData["Erro"] = ex.Message;
                return View(viewModel);

            }
           return RedirectToAction("ListarAlunos");
        }

 
                [HttpGet]
                public IActionResult ListarAlunos()
                {
                    var alunos = alunoService.listarAlunos();


                    return View(alunos);

                }






    }
}
