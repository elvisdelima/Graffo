using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Graffo.Core.BO;

namespace Graffo.Web.Controllers
{
    public class TrelloController : Controller 
    {
        public void Import()
        {
            var importerFromTrello = new ImporterFromTrello();
            importerFromTrello.Import();
        }

    }
}
