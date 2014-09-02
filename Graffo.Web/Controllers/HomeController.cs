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

        public ActionResult LoadChart()
        {
            var chart = new ChartDataFactory(ChartType.QuantidadeDeCartoesPorLista).GetData();
            return Json(chart, JsonRequestBehavior.AllowGet);
        }
    }
}
