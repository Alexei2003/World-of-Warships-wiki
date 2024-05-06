using GeneralClasses.Data.ToServer.Request;
using GeneralClasses.Messages.FromServer.DB.DBObjects;
using GeneralClasses;
using Newtonsoft.Json;

namespace WorldOfWarshipsWiki.Pages.Achievements;

public class AchievementPage : ContentPage
{
    public AchievementPage(int achievementId)
    {
        var request = new RequestObjectMessage()
        {
            Action = GeneralConstant.GeneralServerActions.Get,
            TopicFromServer = RabbitMQ.TopicFromServer,
            ObjectName = GeneralConstant.GeneralObjectFromDB.Achievement,
            ObjectId = achievementId
        };

        RabbitMQ.Publisher.SendMessage(request.ToJson());
        var json = RabbitMQ.Consumer.GetMessage();

        var message = JsonConvert.DeserializeObject<DBAchievementMessage>(json);

        var vStack = GeneratorPage.GetBasePartOfObjectPage(message, GeneralConstant.GeneralObjectFromDB.Achievement);

        vStack.Add(new Label());

        var typeAchievement = new Label()
        {
            Text = "Тип достижения: " + message.TypeAchievementName,
        };
        vStack.Add(typeAchievement);

        var scrollView = new ScrollView
        {
            Content = vStack
        };

        Content = scrollView;
    }
}