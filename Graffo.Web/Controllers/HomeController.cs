using System.Web.Mvc;
using Graffo.Entidades;
using Graffo.Web.BO;

namespace Graffo.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var chart = new ChartFactory(ChartType.QuantidadeDeCartoesDasListasPorImportacao).GetChart();

            return View(chart);
        }
    }
}
