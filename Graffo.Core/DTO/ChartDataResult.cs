using System;
using System.Collections.Generic;
using DotNet.Highcharts.Options;

namespace Graffo.Core.DTO
{
    public class ChartDataResult
    {
        public string Title { get; set; }
        public List<Series> Series { get; set; }
        public DateTime Date { get; set; }
        public List<string> Categories { get; set; }
    }
}