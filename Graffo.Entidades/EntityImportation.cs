using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graffo.Entidades
{
    public class ImportableEntity : Entity
    {
        public long IdImportation { get; set; }
        public string IdTrello { get; set; }
    }
}
