using EM.Service.Service;
using Microsoft.AspNetCore.Mvc;

namespace EM.Web.Controllers
{
    public class CidadeController : Controller
    {
        private CidadeService cidadeService;

        public CidadeController(CidadeService cidadeService)
        {

            this.cidadeService = cidadeService;
        }

        [HttpGet]
        public IActionResult ListarCidades()
        {
            var cidades = cidadeService.ListarCidades();
            return View(cidades);
        }
    }
}
