using EM.Domain.Enum;
using EM.Domain.Models;
using EM.Service.Service;
using Microsoft.AspNetCore.Mvc;

namespace EM.Web.Controllers
{
    public class CidadeController : Controller
    {
        private readonly CidadeService cidadeService;

        public CidadeController(CidadeService cidadeService)
        {

            this.cidadeService = cidadeService;
        }


        [HttpGet]

        public IActionResult UpsertCidade(int? cidadeID) {

            CidadeModel cidade;
            if (cidadeID.HasValue)
            {
                cidade = cidadeService.obterPorCodigo(cidadeID.Value);

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
        public IActionResult CadastrarCidade(CidadeModel cidadeModel)
        {

            try
            {
                var ufs = Enum.GetValues(typeof(UF)).Cast<UF>().ToList();
                ViewBag.Ufs = ufs;
                cidadeService.CadastrarCidade(cidadeModel.cidadeNome, cidadeModel.cidadeUF);
                TempData["Sucesso"] = "Cidade cadastrada com sucesso";
                

            }
            catch (Exception ex)
            {

                TempData["Erro"] = ex.Message;
                return View("UpsertCidade",cidadeModel);
            }
            return RedirectToAction("ListarCidades");
        }
        [HttpPost]
        public IActionResult EditarCidade(CidadeModel cidadeModel)
        {

            try
            {
                var ufs = Enum.GetValues(typeof(UF)).Cast<UF>().ToList();
                ViewBag.Ufs = ufs;
                cidadeService.EditarCidade(cidadeModel);
                TempData["Sucesso"] = "Cidade editada com sucesso";

            }
            catch (Exception ex)
            {

                TempData["Erro"] = ex.Message;
                return View("UpsertCidade",cidadeModel);
            }
            return RedirectToAction("ListarCidades");

        }


        [HttpGet]
        public IActionResult DeletarCidade(int ? cidadeID)
        {
            var ufs = Enum.GetValues(typeof(UF)).Cast<UF>().ToList();
            ViewBag.Ufs = ufs;
           var cidade = cidadeService.obterPorCodigo(cidadeID.Value);
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
                TempData["ErroDeletar"] = ex.Message;

                return RedirectToAction("ListarCidades");   
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
                cidades = cidadeService.buscarPorNome(cidadeNome);
                
            }
            ViewBag.Cidade = cidadeNome;
            return View(cidades);
        }


    }
    }


