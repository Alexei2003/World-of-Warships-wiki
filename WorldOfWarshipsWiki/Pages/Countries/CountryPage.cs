
/* Необъединенное слияние из проекта "WorldOfWarshipsWiki (net8.0-android)"
До:
using GeneralClasses.Data.FromServer.DB;
После:
using GeneralClasses;
using GeneralClasses.Data.FromServer.DB;
*/

/* Необъединенное слияние из проекта "WorldOfWarshipsWiki (net8.0-maccatalyst)"
До:
using GeneralClasses.Data.FromServer.DB;
После:
using GeneralClasses;
using GeneralClasses.Data.FromServer.DB;
*/

/* Необъединенное слияние из проекта "WorldOfWarshipsWiki (net8.0-windows10.0.19041.0)"
До:
using GeneralClasses.Data.FromServer.DB;
После:
using GeneralClasses;
using GeneralClasses.Data.FromServer.DB;
*/
using GeneralClasses;
using GeneralClasses.Data.ToServer.Request;

/* Необъединенное слияние из проекта "WorldOfWarshipsWiki (net8.0-android)"
До:
using GeneralClasses;
using Newtonsoft.Json;
using GeneralClasses.Messages.FromServer.DB.DBObjects;
После:
using GeneralClasses.Messages.FromServer.DB.DBObjects;
*/

/* Необъединенное слияние из проекта "WorldOfWarshipsWiki (net8.0-maccatalyst)"
До:
using GeneralClasses;
using Newtonsoft.Json;
using GeneralClasses.Messages.FromServer.DB.DBObjects;
После:
using GeneralClasses.Messages.FromServer.DB.DBObjects;
*/

/* Необъединенное слияние из проекта "WorldOfWarshipsWiki (net8.0-windows10.0.19041.0)"
До:
using GeneralClasses;
using Newtonsoft.Json;
using GeneralClasses.Messages.FromServer.DB.DBObjects;
После:
using GeneralClasses.Messages.FromServer.DB.DBObjects;
*/
using GeneralClasses.Messages.FromServer.DB.DBObjects;
using Newtonsoft.Json;

namespace WorldOfWarshipsWiki.Pages.Countries;

public class CountryPage : ContentPage
{
    public CountryPage(int countryId)
    {
        var request = new RequestObjectMessage()
        {
            Action = GeneralConstant.GeneralServerActions.Get,
            TopicFromServer = RabbitMQ.TopicFromServer,
            ObjectName = GeneralConstant.GeneralObjectFromDB.Country,
            ObjectId = countryId
        };

        RabbitMQ.Publisher.SendMessage(request.ToJson());
        var json = RabbitMQ.Consumer.GetMessage();

        var message = JsonConvert.DeserializeObject<DBCountryMessage>(json);

        Content = GeneratorPage.GetBasePartOfObjectPage(message, GeneralConstant.GeneralObjectFromDB.Country);
    }
}