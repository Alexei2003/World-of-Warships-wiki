using GeneralClasses;
using GeneralClasses.Data.ToServer.Request;
using GeneralClasses.Messages.FromServer.DB.DBObjects;
using Newtonsoft.Json;

namespace WorldOfWarshipsWiki.Pages.Maps;

public class MapPage : ContentPage
{
    public MapPage(int mapId)
    {
        var request = new RequestObjectMessage()
        {
            Action = GeneralConstant.GeneralServerActions.Get,
            TopicFromServer = RabbitMQ.TopicFromServer,
            ObjectName = GeneralConstant.GeneralObjectFromDB.Map,
            ObjectId = mapId
        };

        RabbitMQ.Publisher.SendMessage(request.ToJson());
        var json = RabbitMQ.Consumer.GetMessage();

        var message = JsonConvert.DeserializeObject<DBMapMessage>(json);

        var vStack = GeneratorPage.GetBasePartOfObjectPage(message, GeneralConstant.GeneralObjectFromDB.Map);

        vStack.Add(new Label());

        var characteristics = new Label()
        {
            Text = "Характеристики карты " + message.Name,
        };
        vStack.Add(characteristics);

        var battletiers = new Label()
        {
            Text = "Уровни сражений: " + message.Battletiers,
        };
        vStack.Add(battletiers);

        var size = new Label()
        {
            Text = "Размер: " + message.Size,
        };
        vStack.Add(size);

        var replyfilename = new Label()
        {
            Text = "Имя файла воспроизведения: " + message.Replyfilename,
        };
        vStack.Add(replyfilename);

        Content = vStack;
    }
}