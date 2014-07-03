using DotNet.Highcharts; 
using Graffo.Entidades;

namespace Graffo.Core.BO
{
    public class ChartFactory
    {
        private Highcharts Chart { get; set; }

        public Highcharts GetChart()
        {
            return Chart;
        }

        public ChartFactory(ChartType chartType)
        {
            Chart = new Highcharts(chartType.ToString());

            var trello = new Trello();
            const string boardId = "s7UpK8Ap";

            var chartDataSource = new ChartDataSource();
            var highchartsFactory = new HighchartsFactory();

            switch (chartType)
            {
                case ChartType.QuantidadeDeCartoesPorLista:
                    {
                        var result = chartDataSource.AmountOfCardsByListSeries(trello, boardId);
                        Chart = highchartsFactory.AmountOfCardsByListChart(result.Series, result.Date);
                        break;
                    }
                case ChartType.QuantidadeDeCartoesDasListasPorImportacao:
                    {
                        var result = chartDataSource.AmountOfCardsFromTheListsByDateSeries(trello, boardId);
                        Chart = highchartsFactory.AmountOfCardsFromTheListsByDateChart(result.Series, result.Categories);
                        break;
                    }

            }
        }
    }
}
