using GeneralClasses.Data.ToServer.Request;
using GeneralClasses.Messages.FromServer.DB.DBObjects;
using GeneralClasses;
using Newtonsoft.Json;

namespace WorldOfWarshipsWiki.Pages.Containers;

public class ContainerPage : ContentPage
{
    public ContainerPage(int containerId)
    {
        var request = new RequestObjectMessage()
        {
            Action = GeneralConstant.GeneralServerActions.Get,
            TopicFromServer = RabbitMQ.TopicFromServer,
            ObjectName = GeneralConstant.GeneralObjectFromDB.Container,
            ObjectId = containerId
        };

        RabbitMQ.Publisher.SendMessage(request.ToJson());
        var json = RabbitMQ.Consumer.GetMessage();

        var message = JsonConvert.DeserializeObject<DBContainerMessage>(json);

        var vStack = GeneratorPage.GetBasePartOfObjectPage(message, GeneralConstant.GeneralObjectFromDB.Container);

        vStack.Add(new Label());

        var characteristics = new Label()
        {
            Text = "Список лута контейнера " + message.Name,
        };
        vStack.Add(characteristics);

        foreach(var item in message.LootList)
        {
            var loot = new Label()
            {
                Text = item.LootChance.ToString() + "% " + item.TypeItemName + ": " + item.ItemName,
            };
            vStack.Add(loot);
        }

        var scrollView = new ScrollView
        {
            Content = vStack
        };

        Content = scrollView;
    }
}