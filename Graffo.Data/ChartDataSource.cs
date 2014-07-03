using System;
using System.Collections.Generic;
using System.Linq;
using Graffo.Entidades;
using Newtonsoft.Json.Linq;
using DotNet.Highcharts.Options;

namespace Graffo.Data
{
    public class ChartDataResult
    {
        public List<Series> Series;
        public DateTime Date;
        public List<String> Categories;
    }

    //TODO: Desmembrar esse arquivo em várias classes e criar um builder
    public class ChartDataSource
    {
        private GraffoContext Context { get; set; }
        private Repository<Entidades.Data> DataRepository { get; set; }
        
        public ChartDataSource()
        {
            Context = new GraffoContext();
        }
        
        public int? BoardId(string boardId)
        {
            var boards = DataRepository.Query.Where(d => d.DataType == TrelloDataType.Board).AsEnumerable();

            foreach (var b in boards)
            {
                dynamic dynamicBoard = JObject.Parse(b.Json);

                if (dynamicBoard.Id == boardId)
                    return b.Id;
            }

            return null;

        }

        public ChartDataResult AmountOfCardsByListSeries(String boardId)
        {
            var series = new List<Series>();
            var id = DataRepository.Query.Where(d => d.DataType == TrelloDataType.Board).Max(d => d.Id);
            var date = DataRepository.Find(id).Date;

            var lists = DataRepository.Query.Where(d => d.DataType == TrelloDataType.List && d.DataParentId == id);

            foreach (var jsonList in lists)
            {
                dynamic list = JObject.Parse(jsonList.Json);
                var cards = DataRepository.Query.Where(d => d.DataType == TrelloDataType.Card && d.DataParentId == jsonList.Id);

                var data = new object[] { cards.Count() };

                var serie = new Series
                {
                    Name = list.Name,
                    Data = new DotNet.Highcharts.Helpers.Data(data)
                };

                series.Add(serie);
            }

            return new ChartDataResult
            {
                Series = series,
                Date = date
            };
        }

        public ChartDataResult AmountOfCardsFromTheListsByDateSeries(string boardId)
        {
            var series = new List<Series>();
            var importacoes = DataRepository.Query.Where(d => d.DataType == TrelloDataType.Board);

            var categories = importacoes.ToList().Select(i => i.Date.ToShortDateString());

            var lists = DataRepository.Query.Where(d => d.DataType == TrelloDataType.List);
            // && importacoes.Select(i => i.Id).Contains(d.DataParentId));

            var listsSeries = new List<Series>();

            var listasJaImportadas = new List<String>();

            foreach (var dataList in lists)
            {
                dynamic list = JObject.Parse(dataList.Json);

                var quantidades = new List<object>();

                foreach (var importacao in importacoes)
                {
                    var listaNessaImportacao =
                        DataRepository.Query.FirstOrDefault(d => d.DataType == TrelloDataType.List && d.DataParentId == importacao.Id && d.TrelloId == dataList.TrelloId);

                    var cards = DataRepository.Query.Where(d => d.DataType == TrelloDataType.Card && d.DataParentId == listaNessaImportacao.Id);

                    quantidades.Add(cards.Count());
                }

                var id = dataList.TrelloId;

                if (listasJaImportadas.Contains(id)) continue;

                listasJaImportadas.Add(id);

                listsSeries.Add(new Series
                {
                    Name = list.Name,
                    Data = new DotNet.Highcharts.Helpers.Data(quantidades.ToArray()),
                });
            }

            var dataResult = new ChartDataResult
            {
                Series = listsSeries,
                Date = DateTime.Now,
                Categories = categories.ToList()
            };

            return dataResult;
        }
    }
}
