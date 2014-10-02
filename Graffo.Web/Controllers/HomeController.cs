using System.Web.Mvc;
using Graffo.Core.BO;
using Graffo.Entidades;

namespace Graffo.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [OutputCache(Duration = 120)]
        public ActionResult LoadChart()
        {
            //var chart = new ChartDataFactory(ChartType.QuantidadeDeCartoesDasListasPorImportacao).GerDataQuantidadeDeCartoesDasListasPorImportacao();
            //var chart = new ChartDataFactory(ChartType.QuantidadeDeCartoesPorLista).GetDataQuantidadeDeCartoesPorLista();

            var chart = new CumulativeFlow().GetChart();
            
            return Json(chart, JsonRequestBehavior.AllowGet);
        }
    }

}
