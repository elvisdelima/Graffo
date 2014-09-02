using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Graffo.Data;
using Manatee.Trello;
using Manatee.Trello.ManateeJson;
using Manatee.Trello.RestSharp;

namespace Graffo.Core.BO
{
    public class ImporterFromTrello
    {
        public void Import()
        {
            var ctx = new GraffoContext();
            var importationRepo = new Repository<Entidades.Importation>(ctx);
            var organizationRepo = new Repository<Entidades.Organization>(ctx);
            var boardRepo = new Repository<Entidades.Board>(ctx);
            var columnRepo = new Repository<Entidades.Column>(ctx);
            var cardRepo = new Repository<Entidades.Card>(ctx);

            const string appKey = "67bd83087dc827f6821ab0ae5dd0c625";
            const string userToken = "8a0ff7249c98a8300dc627c9824b527e6c47deda80e820a563c21e9b12fc8b08";

            var serializer = new ManateeSerializer();
            TrelloConfiguration.Serializer = serializer;
            TrelloConfiguration.Deserializer = serializer;
            TrelloConfiguration.JsonFactory = new ManateeFactory();
            TrelloConfiguration.RestClientProvider = new RestSharpClientProvider();
            TrelloAuthorization.Default.AppKey = appKey;
            TrelloAuthorization.Default.UserToken = userToken;

            var importation = new Entidades.Importation
            {
                Data = DateTime.Now
            };

            importationRepo.Add(importation);
            importationRepo.Save();
            var idImportation = importation.Id;

            var trelloOrganization = new Organization("536955c54113531a42f462f7");

            var organization = new Entidades.Organization
            {
                IdTrello = trelloOrganization.Id,
                Name = trelloOrganization.Name,
                IdImportation = idImportation
            };
            
            organizationRepo.Add(organization);
            organizationRepo.Save();

            foreach (var trelloBoard in trelloOrganization.Boards)
            {
                var board = new Entidades.Board
                {
                    Description = trelloBoard.Description,
                    IdImportation = idImportation,
                    IdOrganization = trelloOrganization.Id,
                    IdTrello = trelloBoard.Id,
                    Name = trelloBoard.Name
                };

                boardRepo.Add(board);
                boardRepo.Save();

                foreach (var trelloList in trelloBoard.Lists)
                {
                    var list = new Entidades.Column()
                    {
                        IdBoard = trelloList.Board.Id,
                        IdImportation = idImportation,
                        IdTrello = trelloList.Id,
                        Name = trelloList.Name
                    };

                    columnRepo.Add(list);
                    columnRepo.Save();

                    foreach (var trelloCard in trelloList.Cards)
                    {
                        var card = new Entidades.Card
                        {
                            IdColumn = trelloCard.List.Id,
                            IdImportation = idImportation,
                            IdTrello = trelloCard.Id,
                            Name = trelloCard.Name
                        };

                        cardRepo.Add(card);
                        cardRepo.Save();
                    }
                }
            }
        }
    }
}
