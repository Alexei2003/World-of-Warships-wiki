using GeneralClasses;
using GeneralClasses.Data.ToServer.Request;
using GeneralClasses.Messages.FromServer.DB.DBObjects;
using Newtonsoft.Json;

namespace WorldOfWarshipsWiki.Pages.Ships;

public class ShipPage : ContentPage
{
    public ShipPage(int shipId)
    {
        var request = new RequestObjectMessage()
        {
            Action = GeneralConstant.GeneralServerActions.Get,
            TopicFromServer = RabbitMQ.TopicFromServer,
            ObjectName = GeneralConstant.GeneralObjectFromDB.Ship,
            ObjectId = shipId
        };

        RabbitMQ.Publisher.SendMessage(request.ToJson());
        var json = RabbitMQ.Consumer.GetMessage();

        var message = JsonConvert.DeserializeObject<DBShipMessage>(json);

        var vStack = GeneratorPage.GetBasePartOfObjectPage(message, GeneralConstant.GeneralObjectFromDB.Ship);

        vStack.Add(new Label());

        var level = new Label()
        {
            Text = "������� �������: " + message.Level,
        };
        vStack.Add(level);

        vStack.Add(new Label());

        var characteristics = new Label()
        {
            Text = "�������������� ������� " + message.Name,
        };
        vStack.Add(characteristics);

        if (message.Survivability != null)
        {
            var label = new Label()
            {
                Text = "���������: " + message.Survivability,
            };
            vStack.Add(label);
        }

        if (message.Aircraft != null)
        {
            var label = new Label()
            {
                Text = "�������: " + message.Aircraft,
            };
            vStack.Add(label);
        }

        if (message.Artillery != null)
        {
            var label = new Label()
            {
                Text = "����������: " + message.Artillery,
            };
            vStack.Add(label);
        }

        if (message.Torpedoes != null)
        {
            var label = new Label()
            {
                Text = "�������: " + message.Torpedoes,
            };
            vStack.Add(label);
        }

        if (message.Airdefense != null)
        {
            var label = new Label()
            {
                Text = "���: " + message.Airdefense,
            };
            vStack.Add(label);
        }

        if (message.Maneuverability != null)
        {
            var label = new Label()
            {
                Text = "�������������: " + message.Maneuverability,
            };
            vStack.Add(label);
        }

        if (message.Concealment != null)
        {
            var label = new Label()
            {
                Text = "����������: " + message.Concealment,
            };
            vStack.Add(label);
        }

        if (message.PriceExp != null)
        {
            var label = new Label()
            {
                Text = "���������: " + message.PriceExp + " ����� ",
            };
            vStack.Add(label);
        }

        if (message.PriceMoney != null)
        {
            var label = new Label()
            {
                Text = "���������: " + message.PriceMoney + " ������� ",
            };
            vStack.Add(label);
        }

        vStack.Add(new Label());

        if(message.ModulesList.Count > 0)
        {
            var modules = new Label()
            {
                Text = "������ ������� " + message.Name,
            };
            vStack.Add(modules);
        }

        foreach (var modul in message.ModulesList)
        {
            var hStack = new HorizontalStackLayout();

            var image = new Image()
            {
                Source = GeneratorPage.GetUrlImageFromPath(modul.PicturePath, GeneralConstant.GeneralObjectFromDB.Ship),
            };
            hStack.Add(image);

            var descriptionVStack = new VerticalStackLayout();

            var description = new Label()
            {
                Text = modul.Name + " - " + modul.Description,
            };
            descriptionVStack.Add(description);

            var priceExp = new Label()
            {
                Text = "���������: " + modul.PriceExp + " ����� ",
            };
            descriptionVStack.Add(priceExp);

            var priceMoney = new Label()
            {
                Text = "���������: " + modul.PriceMoney + " ������� ",
            };
            descriptionVStack.Add(priceMoney);

            hStack.Add(descriptionVStack);

            vStack.Add(hStack);
        }

        var scrollView = new ScrollView
        {
            Content = vStack
        };

        Content = scrollView;
    }
}