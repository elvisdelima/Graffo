using System;
using System.Drawing;
using System.Collections.Generic;
using DotNet.Highcharts;
using DotNet.Highcharts.Options;
using DotNet.Highcharts.Enums;
using DotNet.Highcharts.Helpers;

namespace Graffo.Web.BO
{
    public class HighchartsFactory
    {
        public HighchartsFactory()
        {
        }

        public Highcharts AmountOfCardsByListChart(List<Series> listas, DateTime category)
        {
            return new Highcharts("chart")
                .InitChart(new Chart { Type = ChartTypes.Column })
                .SetTitle(new Title { Text = "Quantidade de Cartões por Lista" })
                .SetYAxis(new YAxis
                {
                    Title = new YAxisTitle { Text = "Qtd." }
                })
                .SetXAxis(new XAxis
                {
                    Categories = new[] { category.ToShortDateString() }
                })
                .SetSeries(listas.ToArray());
        }


        public Highcharts AmountOfCardsFromTheListsByDateChart(List<Series> listas, List<string> categories)
        {
            var chart = new Highcharts("chart")
                .InitChart(new Chart { DefaultSeriesType = ChartTypes.Column, Height = 600 })
                .SetTitle(new Title { Text = "Quantidade de Cartões das Listas por Data" })
                .SetXAxis(new XAxis
                {
                    Categories = categories.ToArray(),

                })
                .SetYAxis(new YAxis
                {
                    Title = new YAxisTitle { Text = "Qtd." }
                })
                .SetLegend(new Legend
                {
                    Layout = Layouts.Vertical,
                    Align = HorizontalAligns.Center,
                    VerticalAlign = VerticalAligns.Bottom,
                    //                    X = 100,
                    //                    Y = 500,
                    Floating = false,
                    BackgroundColor = new BackColorOrGradient(ColorTranslator.FromHtml("#FFFFFF")),
                    Shadow = true
                })
                .SetSeries(listas.ToArray());

            return chart;
        }
    }
}