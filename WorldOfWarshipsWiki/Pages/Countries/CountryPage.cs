
/* �������������� ������� �� ������� "WorldOfWarshipsWiki (net8.0-android)"
��:
using GeneralClasses.Data.FromServer.DB;
�����:
using GeneralClasses;
using GeneralClasses.Data.FromServer.DB;
*/

/* �������������� ������� �� ������� "WorldOfWarshipsWiki (net8.0-maccatalyst)"
��:
using GeneralClasses.Data.FromServer.DB;
�����:
using GeneralClasses;
using GeneralClasses.Data.FromServer.DB;
*/

/* �������������� ������� �� ������� "WorldOfWarshipsWiki (net8.0-windows10.0.19041.0)"
��:
using GeneralClasses.Data.FromServer.DB;
�����:
using GeneralClasses;
using GeneralClasses.Data.FromServer.DB;
*/
using GeneralClasses;
using GeneralClasses.Data.ToServer.Request;

/* �������������� ������� �� ������� "WorldOfWarshipsWiki (net8.0-android)"
��:
using GeneralClasses;
using Newtonsoft.Json;
using GeneralClasses.Messages.FromServer.DB.DBObjects;
�����:
using GeneralClasses.Messages.FromServer.DB.DBObjects;
*/

/* �������������� ������� �� ������� "WorldOfWarshipsWiki (net8.0-maccatalyst)"
��:
using GeneralClasses;
using Newtonsoft.Json;
using GeneralClasses.Messages.FromServer.DB.DBObjects;
�����:
using GeneralClasses.Messages.FromServer.DB.DBObjects;
*/

/* �������������� ������� �� ������� "WorldOfWarshipsWiki (net8.0-windows10.0.19041.0)"
��:
using GeneralClasses;
using Newtonsoft.Json;
using GeneralClasses.Messages.FromServer.DB.DBObjects;
�����:
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