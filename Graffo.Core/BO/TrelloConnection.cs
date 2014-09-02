using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Manatee.Trello;
using Manatee.Trello.ManateeJson;
using Manatee.Trello.RestSharp;

namespace Graffo.Core.BO
{
    public class TrelloConnection
    {
        public TrelloConnection()
        {
            const string appKey = "67bd83087dc827f6821ab0ae5dd0c625";
            const string userToken = "8a0ff7249c98a8300dc627c9824b527e6c47deda80e820a563c21e9b12fc8b08";

            var serializer = new ManateeSerializer();
            TrelloConfiguration.Serializer = serializer;
            TrelloConfiguration.Deserializer = serializer;
            TrelloConfiguration.JsonFactory = new ManateeFactory();
            TrelloConfiguration.RestClientProvider = new RestSharpClientProvider();
            TrelloAuthorization.Default.AppKey = appKey;
            TrelloAuthorization.Default.UserToken = userToken;
        }
    }
}
