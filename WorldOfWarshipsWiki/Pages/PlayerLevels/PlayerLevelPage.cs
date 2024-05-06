using GeneralClasses;
using GeneralClasses.Data.ToServer.Request;
using GeneralClasses.Messages.FromServer.DB.DBObjects;
using Newtonsoft.Json;

namespace WorldOfWarshipsWiki.Pages.PlayerLevels;

public class PlayerLevelPage : ContentPage
{
    public PlayerLevelPage(int playerLevelId)
    {
        var request = new RequestObjectMessage()
        {
            Action = GeneralConstant.GeneralServerActions.Get,
            TopicFromServer = RabbitMQ.TopicFromServer,
            ObjectName = GeneralConstant.GeneralObjectFromDB.PlayerLevel,
            ObjectId = playerLevelId
        };

        RabbitMQ.Publisher.SendMessage(request.ToJson());
        var json = RabbitMQ.Consumer.GetMessage();

        var message = JsonConvert.DeserializeObject<DBPlayerLevelMessage>(json);

        var vStack = GeneratorPage.GetBasePartOfObjectPage(message, GeneralConstant.GeneralObjectFromDB.PlayerLevel);

        vStack.Add(new Label());

        var characteristics = new Label()
        {
            Text = "���������� ��� ������ " + message.Name,
        };
        vStack.Add(characteristics);

        var battleNeed = new Label()
        {
            Text = "���������� �������� ����������� ��� ���������� ������: " + message.BattleNeed,
        };
        vStack.Add(battleNeed);

        var size = new Label()
        {
            Text = "����� ���������� �������� ����������� ��� ���������� ������: " + message.BattleTotal,
        };
        vStack.Add(size);


        var scrollView = new ScrollView
        {
            Content = vStack
        };

        Content = scrollView;
    }
}