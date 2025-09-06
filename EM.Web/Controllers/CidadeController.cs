using EM.Domain.Enum;
using EM.Domain.Models;
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

        public IActionResult UpsertCidade(int? cidadeID) {

            CidadeModel cidade;
            if (cidadeID.HasValue)
            {
                cidade = cidadeService.ListarCidades().FirstOrDefault(c=> c.cidadeID == cidadeID);

                if(cidade == null)
                {
                    TempData["Erro"] = "Cidade não encontrada";
                    return RedirectToAction("ListarCidades");

                }
            }
            else
            {
                cidade = new CidadeModel();
            }

            var ufs = Enum.GetValues(typeof(UF)).Cast<UF>().ToList();
            ViewBag.Ufs = ufs;

            return View(cidade);
        }

        

        [HttpPost]
        public IActionResult UpsertCidade(CidadeModel cidadeModel) {

            try
            {
                if (cidadeModel.cidadeID == 0)
                {
                    cidadeService.CadastrarCidade(cidadeModel.cidadeNome, cidadeModel.cidadeUF);
                    TempData["Sucesso"] = "Cidade cadatrada com sucesso";

                }
                else
                {
                    cidadeService.EditarCidade(cidadeModel);
                    TempData["Sucesso"] = "Cidade editada com sucesso";


                }
                return RedirectToAction("ListarCidades");
            }
            catch (Exception ex) {
                var ufs = Enum.GetValues(typeof(UF)).Cast<UF>().ToList();
                ViewBag.UFs = ufs;
                TempData["Erro"] = ex.Message;
                return View(cidadeModel);
            }
        }


        [HttpGet]

        public IActionResult DeletarCidade(int ? cidadeID)
        {
            var ufs = Enum.GetValues(typeof(UF)).Cast<UF>().ToList();
            ViewBag.Ufs = ufs;
            var cidade = cidadeService.ListarCidades().FirstOrDefault(c=>c.cidadeID == cidadeID);
            return View(cidade);
            
        }

        [HttpPost]
        public IActionResult DeletarCidade(CidadeModel cidadeModel) {

            try {

               
                cidadeService.DeletarCidade(cidadeModel);
                return RedirectToAction("ListarCidades");

            }catch(Exception ex)
            {
                var ufs = Enum.GetValues(typeof(UF)).Cast<UF>().ToList();
                ViewBag.Ufs = ufs;
                TempData["Erro"] = ex.Message;
                return View(cidadeModel);   
            }
            
        }
        

                [HttpGet]
        public IActionResult ListarCidades(string? cidadeNome)
        {
            List<CidadeModel> cidades;
            if (string.IsNullOrEmpty(cidadeNome))
            {
                cidades = cidadeService.ListarCidades();
                
            }
            else
            {
                cidades = cidadeService.buscarPorNome(cidadeNome.ToUpper());
                
            }
            ViewBag.Cidade = cidadeNome;
            return View(cidades);
        }


    }
    }

