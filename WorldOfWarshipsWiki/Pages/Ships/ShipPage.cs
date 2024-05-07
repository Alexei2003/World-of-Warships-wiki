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
            Text = "Уровень коробля: " + message.Level,
        };
        vStack.Add(level);

        vStack.Add(new Label());

        var characteristics = new Label()
        {
            Text = "Характеристики корабля " + message.Name,
        };
        vStack.Add(characteristics);

        if (message.Survivability != null)
        {
            var label = new Label()
            {
                Text = "Живучесть: " + message.Survivability,
            };
            vStack.Add(label);
        }

        if (message.Aircraft != null)
        {
            var label = new Label()
            {
                Text = "Авиация: " + message.Aircraft,
            };
            vStack.Add(label);
        }

        if (message.Artillery != null)
        {
            var label = new Label()
            {
                Text = "Артиллерия: " + message.Artillery,
            };
            vStack.Add(label);
        }

        if (message.Torpedoes != null)
        {
            var label = new Label()
            {
                Text = "Торпеды: " + message.Torpedoes,
            };
            vStack.Add(label);
        }

        if (message.Airdefense != null)
        {
            var label = new Label()
            {
                Text = "ПВО: " + message.Airdefense,
            };
            vStack.Add(label);
        }

        if (message.Maneuverability != null)
        {
            var label = new Label()
            {
                Text = "Маневренность: " + message.Maneuverability,
            };
            vStack.Add(label);
        }

        if (message.Concealment != null)
        {
            var label = new Label()
            {
                Text = "Маскировка: " + message.Concealment,
            };
            vStack.Add(label);
        }

        if (message.PriceExp != null)
        {
            var label = new Label()
            {
                Text = "Стоимость: " + message.PriceExp + " Опыта ",
            };
            vStack.Add(label);
        }

        if (message.PriceMoney != null)
        {
            var label = new Label()
            {
                Text = "Стоимость: " + message.PriceMoney + " Серебра ",
            };
            vStack.Add(label);
        }

        vStack.Add(new Label());

        if(message.ModulesList.Count > 0)
        {
            var modules = new Label()
            {
                Text = "Список модулей " + message.Name,
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
                Text = "Стоимость: " + modul.PriceExp + " Опыта ",
            };
            descriptionVStack.Add(priceExp);

            var priceMoney = new Label()
            {
                Text = "Стоимость: " + modul.PriceMoney + " Серебра ",
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