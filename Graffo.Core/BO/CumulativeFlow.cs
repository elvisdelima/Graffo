using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*
    https://api.trello.com/1/cards/1XdYdkPa/actions?key=67bd83087dc827f6821ab0ae5dd0c625&token=8a0ff7249c98a8300dc627c9824b527e6c47deda80e820a563c21e9b12fc8b08
    https://api.trello.com/1/lists/52f2261143e3b701215cafb0/actions?key=67bd83087dc827f6821ab0ae5dd0c625&token=8a0ff7249c98a8300dc627c9824b527e6c47deda80e820a563c21e9b12fc8b08


    https://api.trello.com/1/lists/52f2261143e3b701215cafb0/actions?key=67bd83087dc827f6821ab0ae5dd0c6267bd83087dc827f6821ab0ae5dd0c625&token=8a0ff7249c98a8300dc627c9824b527e6c47deda80e820a563c21e9b12fc8b08
        */
using Graffo.Core.DTO;
using Graffo.Core.Extensions;
using TrelloNet;
using TrelloNet.Extensions;
using ActionType = Manatee.Trello.ActionType;
using Board = Manatee.Trello.Board;


namespace Graffo.Core.BO
{
    public class CumulativeFlow
    {
        public ChartReturn GetChart()
        {
            var result = new ChartReturn {Series = new List<Serie>()};

            #region
            //Autenticacao
            const string appKey = "67bd83087dc827f6821ab0ae5dd0c625";
            const string userToken = "8a0ff7249c98a8300dc627c9824b527e6c47deda80e820a563c21e9b12fc8b08";

            var trello = new Trello(appKey);
            trello.Authorize(userToken);
            
            #endregion

            var dataFinal = DateTime.Now.Date;
            var dataInicial = dataFinal.AddDays(-5).Date;

            //var trelloboard = trello.Boards.WithId("53d66174a6e160be230d1cf1"); //Nash - Treinamentos
            var trelloboard = trello.Boards.WithId("533034b3dfd7c4df4f3431e9"); // Fortes conecta
            result.Title = "Quadro: "+trelloboard.Name;
            
            var lists = trello.Lists.ForBoard(trelloboard);
            
            var categories = new List<string>();
            
            foreach (var list in lists)
            {
                var data = dataFinal.Date;
                var dados = new List<double>();

                var quantidadeDeCartoesNaData = 0;

                while (data >= dataInicial)
                {
                    var ini = data.InicioDoDia();
                    var fim = data.FimDoDia();

                    if (fim.Equals(dataFinal.FimDoDia()))
                    {
                        quantidadeDeCartoesNaData = trello.Cards.ForList(list).Count();
                    }
                    else
                    {
                        var updateCardMoveActions = trello.Actions.ForList(list).OfType<UpdateCardMoveAction>().Where(a => a.Date >= ini && a.Date <= fim).ToList();
                        var adicionados = updateCardMoveActions.Count(a => a.Data.ListAfter.Id == list.Id);
                        var removidos = updateCardMoveActions.Count(a => a.Data.ListBefore.Id == list.Id);

                        quantidadeDeCartoesNaData -= adicionados + removidos;
                    }
                    
                    dados.Add(quantidadeDeCartoesNaData);

                    var shortDate = data.Date.ToShortDateString(); 
                    if (!categories.Contains(shortDate))
                        categories.Add(shortDate);
                    
                    data = data.AddDays(-1);
                }

                dados.Reverse();
                result.Series.Add(new Serie { name = list.Name, data = dados });
            }

            categories.Reverse();
            result.categories = categories;



        
            /*
            while (data >= dataInicial)
            {
                var acoesNaData = actions.Where(a => a.Date == data).ToList();
                
                foreach (var list in lists)
                {
                    result.Series.Add(new Serie { name = list.Name });

                    var quantidadeDeCartoesNaData = trello.Cards.ForList(list).ToList().Count();
                    
                    var adicionados = acoesNaData.Count(a => a.Data.ListBefore.Id == list.Id);
                    var removidos = acoesNaData.Count(a => a.Data.ListAfter.Id == list.Id);

                    var quantidadeCartoesNaData = quantidadeDeCartoesNaData + adicionados - removidos;
                    dados.Add(quantidadeCartoesNaData);
                }

                
                categories.Add(data.Date.ToShortDateString());
                data = data.AddDays(-1);
            }
             */

            return result;








            /*
            var board = new Board("cSCNsmRV");

            var dataFinal = DateTime.Now;
            var dataInicial = dataFinal.AddDays(-10);

            if (board != null)
            {
                //var actions = board.Actions; //.Where(a => a.Type == ActionType.UpdateCardIdList && a.Date > dataInicial);

                var i = 0;
                var x = 0;
                var texto = new List<object>();

                foreach (var list in board.Lists)
                {
                    foreach (var action in list.Actions)
                    {
                      //  texto.Add(   );
                        x = x + 1;
                        if (action.Type == ActionType.UpdateCardIdList)
                            i = i + 1;
                    }
                }
                var dataAtual = dataInicial;

                while (dataAtual <= dataFinal)
                {
                    
                    dataAtual = dataAtual.AddDays(1);
                }
            }*/
            return new ChartReturn();
        }
    }
}
