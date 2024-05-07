using GeneralClasses.Data.ToServer.Request;
using GeneralClasses.Messages.FromServer.DB.DBObjects;
using GeneralClasses;
using Newtonsoft.Json;
using System.ComponentModel;

namespace WorldOfWarshipsWiki.Pages.Commanders;

public class CommanderPage : ContentPage
{
    public CommanderPage(int commanderId)
    {
        var request = new RequestObjectMessage()
        {
            Action = GeneralConstant.GeneralServerActions.Get,
            TopicFromServer = RabbitMQ.TopicFromServer,
            ObjectName = GeneralConstant.GeneralObjectFromDB.Commander,
            ObjectId = commanderId
        };

        RabbitMQ.Publisher.SendMessage(request.ToJson());
        var json = RabbitMQ.Consumer.GetMessage();

        var message = JsonConvert.DeserializeObject<DBCommanderMessage>(json);

        var vStack = GeneratorPage.GetBasePartOfObjectPage(message, GeneralConstant.GeneralObjectFromDB.Commander);

        vStack.Add(new Label());

        var origins = new Label()
        {
            Text = "Способ получения: " + message.Origins,
        };
        vStack.Add(origins);

        vStack.Add(new Label());

        if(message.TalentList.Count > 0)
        {
            var talents = new Label()
            {
                Text = "Список талантов " + message.Name,
            };
            vStack.Add(talents);
        }

        foreach (var talent in message.TalentList)
        {
            var hStack = new HorizontalStackLayout();

            var image = new Image()
            {
                Source = GeneratorPage.GetUrlImageFromPath(talent.PicturePath,GeneralConstant.GeneralObjectFromDB.Commander),
            };
            hStack.Add(image);

            var description = new Label()
            {
                Text = talent.Name + " - " +talent.Description,
            };
            hStack.Add(description);

            vStack.Add(hStack);
        }

        var scrollView = new ScrollView
        {
            Content = vStack
        };

        Content = scrollView;
    }
}