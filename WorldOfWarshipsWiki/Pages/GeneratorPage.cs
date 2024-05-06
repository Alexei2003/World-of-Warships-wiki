using GeneralClasses;
using GeneralClasses.Data.FromServer.DB;
using GeneralClasses.Data.ToServer.Request;
using GeneralClasses.Messages.FromServer.DB.DBObjects;
using Newtonsoft.Json;

namespace WorldOfWarshipsWiki.Pages
{
    public static class GeneratorPage
    {
        public static ScrollView GetObjectOfListPage(GeneralConstant.GeneralObjectFromDB typeObjectShow, TapGestureRecognizer funcGoToNextPage, int? contryId = null)
        {
            var request = new RequestListMessage()
            {
                Action = GeneralConstant.GeneralServerActions.Get,
                TopicFromServer = RabbitMQ.TopicFromServer,
                ObjectName = typeObjectShow,
                CountryId = contryId
            };

            RabbitMQ.Publisher.SendMessage(request.ToJson());
            var json = RabbitMQ.Consumer.GetMessage();

            var messageList = JsonConvert.DeserializeObject<DBListMessage>(json);

            var vStack = new VerticalStackLayout()
            {
                HorizontalOptions = LayoutOptions.Center,
            };

            var namePageText = "";

            switch (typeObjectShow)
            {
                case GeneralConstant.GeneralObjectFromDB.Countries:
                    namePageText = "Список стран";
                    break;
                case GeneralConstant.GeneralObjectFromDB.Ships:
                    namePageText = "Список кораблей";
                    break;
                case GeneralConstant.GeneralObjectFromDB.Commanders:
                    namePageText = "Список уникальных командиров";
                    break;
                case GeneralConstant.GeneralObjectFromDB.Maps:
                    namePageText = "Список карт";
                    break;
                case GeneralConstant.GeneralObjectFromDB.PlayerLevels:
                    namePageText = "Список уровней игрока";
                    break;
                case GeneralConstant.GeneralObjectFromDB.Achievements:
                    namePageText = "Список достижений";
                    break;
                case GeneralConstant.GeneralObjectFromDB.Containers:
                    namePageText = "Список контейнеров";
                    break;
            }

            var namePage = new Label()
            {
                HorizontalOptions = LayoutOptions.Center,
                FontSize = 20,
                Text = namePageText,
            };
            vStack.Add(namePage);

            foreach (var message in messageList.ItemList)
            {
                var vObjectStack = new VerticalStackLayout()
                {
                    HorizontalOptions = LayoutOptions.Center,
                };

                if (message.Name != null)
                {
                    vObjectStack.Add(new Label()
                    {
                        HorizontalOptions = LayoutOptions.Center,
                        Text = message.Name,
                    });
                }

                if (message.PicturePath != null)
                {
                    var image = new Image()
                    {
                        Source = GetUrlImageFromPath(message.PicturePath, typeObjectShow),
                        WidthRequest = PagesConstants.SIZE_IMAGE_IN_LIST_PAGE,
                        HeightRequest = PagesConstants.SIZE_IMAGE_IN_LIST_PAGE,
                        BindingContext = message.Id
                    };

                    image.GestureRecognizers.Add(funcGoToNextPage);

                    vObjectStack.Add(image);
                }

                vStack.Add(vObjectStack);
            }

            var scrollView = new ScrollView
            {
                Content = vStack
            };

            return scrollView;
        }

        public static VerticalStackLayout GetBasePartOfObjectPage(DBObjectMessage message, GeneralConstant.GeneralObjectFromDB typeObjectShow)
        {
            var vStack = new VerticalStackLayout()
            {
                Padding = 10,
                HorizontalOptions = LayoutOptions.Center,
            };
            if (message.Name != null)
            {
                vStack.Add(new Label()
                {
                    HorizontalOptions = LayoutOptions.Center,
                    Text = message.Name,
                });
            }

            if (message.PicturePath != null)
            {
                vStack.Add(new Image()
                {
                    Source = GetUrlImageFromPath(message.PicturePath, typeObjectShow),
                    WidthRequest = PagesConstants.SIZE_IMAGE_IN_OBJECT_PAGE,
                    HeightRequest = PagesConstants.SIZE_IMAGE_IN_OBJECT_PAGE,
                });
            }


            if (message.Description != null)
            {
                vStack.Add(new Label()
                {
                    Text = message.Description,
                });
            }

            return vStack;
        }

        public static string GetUrlImageFromPath(string path, GeneralClasses.GeneralConstant.GeneralObjectFromDB typeObjectShow)
        {
            string add = "";

            switch (typeObjectShow)
            {
                case GeneralConstant.GeneralObjectFromDB.Country:
                case GeneralConstant.GeneralObjectFromDB.Countries:
                    add = "Countries/";
                    break;
                case GeneralConstant.GeneralObjectFromDB.Ship:
                case GeneralConstant.GeneralObjectFromDB.Ships:
                    add = "Ships/";
                    break;
                case GeneralConstant.GeneralObjectFromDB.Commander:
                case GeneralConstant.GeneralObjectFromDB.Commanders:
                    add = "Commanders/";
                    break;
                case GeneralConstant.GeneralObjectFromDB.Map:
                case GeneralConstant.GeneralObjectFromDB.Maps:
                    add = "Maps/";
                    break;
                case GeneralConstant.GeneralObjectFromDB.PlayerLevel:
                case GeneralConstant.GeneralObjectFromDB.PlayerLevels:
                    add = "PlayerLevels/";
                    break;
                case GeneralConstant.GeneralObjectFromDB.Achievement:
                case GeneralConstant.GeneralObjectFromDB.Achievements:
                    add = "Achievements/";
                    break;
                case GeneralConstant.GeneralObjectFromDB.Container:
                case GeneralConstant.GeneralObjectFromDB.Containers:
                    add = "Containers/";
                    break;
            }

            var a = "http://" + GeneralConstant.SERVER_IP + "/WorldOfWarships/Images/" + add + path;
            return "http://" + GeneralConstant.SERVER_IP + "/WorldOfWarships/Images/" + add + path;
        }

        public class NextObjectData()
        {
            public GeneralConstant.GeneralObjectFromDB? NextObjectType { get; set; } = null;

            public int? IdPrevObject { get; set; } = null;
        }
    }
}
