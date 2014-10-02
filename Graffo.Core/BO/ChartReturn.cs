using System.Collections.Generic;
using Graffo.Core.DTO;

namespace Graffo.Core.BO
{
    public class ChartReturn
    {
        public List<string> categories { get; set; }
        public List<Serie> Series { get; set; }
        public string Title { get; set; }
    }
}