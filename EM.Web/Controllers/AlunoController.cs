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
                public IActionResult ListarAlunos()
                {
                    var alunos = alunoService.listarAlunos();


                    return View(alunos);

                }






    }
}
