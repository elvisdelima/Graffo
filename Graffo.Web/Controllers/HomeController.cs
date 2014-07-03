using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Graffo.Core.BO;
using Graffo.Entidades;

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
