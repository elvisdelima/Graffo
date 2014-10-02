using System;
using System.Collections.Generic;
using System.Linq;
using Graffo.Core.DTO;
using Graffo.Data;
using Graffo.Entidades;

namespace Graffo.Core.BO
{
    public class ChartDataFactory
    {
        public QuantidadeDeCartoesDasListasPorImportacao QuantidadeDeCartoesPorListaPorImportacao { get; set; }
        public QuantidadeDeCartoesPorLista QuantidadeDeCartoesPorLista { get; set; }
        
        private GraffoContext Context { get; set; }
        private Repository<Importation> ImportationRepository { get; set; }
        private Repository<Board> BoardRepository { get; set; }
        private Repository<Column> ColumnRepository { get; set; }
        private Repository<Card> CardRepository { get; set; }
        
        //public ChartDataFactory(ChartType chartType)
        //{
        //    Context = new GraffoContext();
        //    ImportationRepository = new Repository<Importation>(Context);
        //    ColumnRepository = new Repository<Column>(Context);
        //    CardRepository = new Repository<Card>(Context);

        //    var data = DateTime.Now.AddDays(1);
        //    var importation = ImportationRepository.Query.OrderByDescending(i => i.Id).First();

        //    QuantidadeDeCartoesPorLista = new QuantidadeDeCartoesPorLista
        //    {
        //        categories = new List<string>()
        //    };

        //    var dados = new List<double>();

        //    switch (chartType)
        //    {
        //        case ChartType.QuantidadeDeCartoesPorLista:
        //        {
        //            var columns = ColumnRepository.Query.Where(c => c.IdImportation == importation.Id && c.IdBoard == "533034b3dfd7c4df4f3431e9");

        //            foreach (var column in columns)
        //            {
        //                QuantidadeDeCartoesPorLista.categories.Add(column.Name); 
                     
        //                var qtdCards = CardRepository.Query.Count(c => c.IdColumn == column.IdTrello );
        //                dados.Add(qtdCards);
        //            }

        //            var serie = new Serie { name = "Quatidade", data = dados };

        //            QuantidadeDeCartoesPorLista.Series = new List<Serie> { serie };

                    
        //            /*
        //            QuantidadeDeCartoesPorLista = new QuantidadeDeCartoesPorLista
        //            {
        //                categories = new List<string>
        //                {
        //                    "Previsto",
        //                    "Desenvolvimento",
        //                    "Teste",
        //                    "Revisão",
        //                    "Finalizado",
        //                    "Entregue"
        //                }.ToArray()
        //            };

        //            var serie = new Serie {name = "Pendencias", data = new List<double> {10, 20, 30, 40, 60, 70}};

        //            //var serie2 = new Serie {name = "Solicitacoes", data = new List<double> {20, 10, 40, 50, 40, 20}};

        //            QuantidadeDeCartoesPorLista.Series = new List<Serie> {serie};
        //            */
        //            break;
        //        }
        //        case ChartType.QuantidadeDeCartoesDasListasPorImportacao:
        //        {
        //            break;
        //        }
        //    } 
        //}

        public ChartDataFactory(ChartType chartType)
        {
            Context = new GraffoContext();
            ImportationRepository = new Repository<Importation>(Context);
            ColumnRepository = new Repository<Column>(Context);
            CardRepository = new Repository<Card>(Context);
            BoardRepository = new Repository<Board>(Context);

            var importationsIds = BoardRepository.Query.Where(b => b.IdTrello == "533034b3dfd7c4df4f3431e9").Select(b => b.IdImportation);

            var importations = ImportationRepository.Query.Where(i => importationsIds.Contains(i.Id));

            QuantidadeDeCartoesPorListaPorImportacao = new QuantidadeDeCartoesDasListasPorImportacao
            {
                categories = new List<string>()
            };

            var dados = new List<double>();

            switch (chartType)
            {
                case ChartType.QuantidadeDeCartoesPorLista:
                {
                    break;
                }
                case ChartType.QuantidadeDeCartoesDasListasPorImportacao:
                {
                    QuantidadeDeCartoesPorListaPorImportacao.Series = new List<Serie>();
                    var categorias = new List<string>();

                    foreach (var importation in importations)
                    {
                        var columns = ColumnRepository.Query.Where(c => c.IdImportation == importation.Id && c.IdBoard == "533034b3dfd7c4df4f3431e9");

                        foreach (var column in columns)
                        {
                            if (!QuantidadeDeCartoesPorListaPorImportacao.categories.Exists(c => c == column.Name))
                                QuantidadeDeCartoesPorListaPorImportacao.categories.Add(column.Name);

                            var qtdCards = CardRepository.Query.Count(c => c.IdColumn == column.IdTrello && c.IdImportation == importation.Id);
                            dados.Add(qtdCards);
                        }

                        QuantidadeDeCartoesPorListaPorImportacao.Series.Add(new Serie { name = importation.Data.ToShortDateString(), data = dados });
                    }

                    break;
                }
            }
        }


        public QuantidadeDeCartoesPorLista GetDataQuantidadeDeCartoesPorLista()
        {
            return QuantidadeDeCartoesPorLista;
        }

        public QuantidadeDeCartoesDasListasPorImportacao GerDataQuantidadeDeCartoesDasListasPorImportacao()
        {
            return QuantidadeDeCartoesPorListaPorImportacao;
        }
    }
}
