using GeneralClasses;
using GeneralClasses.Data.FromServer.DB;
using GeneralClasses.Data.ToServer.Request;
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
                    namePageText = "Список короблей";
                    break;
                case GeneralConstant.GeneralObjectFromDB.Commanders:
                    namePageText = "Список уникальных командиров";
                    break;

            }

            var namePage = new Label()
            {
                HorizontalOptions = LayoutOptions.Center,
                FontSize = 20,
                Text = namePageText,
            };
            vStack.Add(namePage);

            foreach (var message in messageList.List)
            {
                var vObjectStack = new VerticalStackLayout()
                {
                    HorizontalOptions = LayoutOptions.Center,
                };

                if (message.Name != null)
                {
                    vObjectStack.Add(new Label()
                    {
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

        public static HorizontalStackLayout GetBasePartOfObjectPage(DBBaseDataMessage message, GeneralClasses.GeneralConstant.GeneralObjectFromDB typeObjectShow)
        {
            var hStack = new HorizontalStackLayout();

            var vStack = new VerticalStackLayout();
            if (message.Name != null)
            {
                vStack.Add(new Label()
                {
                    Text = message.Name,
                });
            }

            if (message.PicturePath != null)
            {
                vStack.Add(new Image()
                {
                    Source = GetUrlImageFromPath(message.PicturePath, typeObjectShow)
                });
            }

            hStack.Add(vStack);

            if (message.Description != null)
            {
                hStack.Add(new Label()
                {
                    Text = message.Description,
                });
            }

            return hStack;
        }

        public static string GetUrlImageFromPath(string path, GeneralClasses.GeneralConstant.GeneralObjectFromDB typeObjectShow)
        {
            string add = "";

            switch (typeObjectShow)
            {
                case GeneralConstant.GeneralObjectFromDB.Countries:
                    add = "Countries/";
                    break;
                case GeneralConstant.GeneralObjectFromDB.Ships:
                    add = "Ships/";
                    break;
                case GeneralConstant.GeneralObjectFromDB.Commanders:
                    add = "Commanders/";
                    break;
            }
            return "http://" + GeneralConstant.SERVER_IP + "/WorldOfWarships/Images/" + add + path;
        }

        public class NextObjectData()
        {
            public GeneralConstant.GeneralObjectFromDB? NextObjectType { get; set; } = null;

            public int? IdPrevObject { get; set; } = null;
        }
    }
}
