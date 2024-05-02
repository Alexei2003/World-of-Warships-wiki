using GeneralClasses;
using GeneralClasses.Data.FromServer.DB;
using GeneralClasses.Data.ToServer.Request;
using Newtonsoft.Json;

namespace WorldOfWarshipsWiki.Pages
{
    public static class GeneratorPage
    {
        public static VerticalStackLayout GetObjectOfListPage(GeneralClasses.GeneralConstant.GeneralObjectFromDB typeObjectShow, GeneralClasses.GeneralConstant.GeneralObjectFromDB typeObjectNext)
        {
            var request = new RequestListMessage()
            {
                Action = GeneralConstant.GeneralServerActions.Get,
                TopicFromServer = RabbitMQ.TopicFromServer,
                ObjectName = typeObjectShow
            };

            RabbitMQ.Publisher.SendMessage(request.ToJson());
            var json = RabbitMQ.Consumer.GetMessage();

            var messageList = JsonConvert.DeserializeObject<DBListMessage>(json);

            var vStack = new VerticalStackLayout()
            {
                HorizontalOptions = LayoutOptions.Center,
            };
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
                    vObjectStack.Add(new Image()
                    {
                        Source = GetUrlImageFromPath(message.PicturePath, typeObjectShow),
                        WidthRequest = PagesConstants.SIZE_IMAGE_IN_LIST_PAGE,
                        HeightRequest = PagesConstants.SIZE_IMAGE_IN_LIST_PAGE,
                    });
                }

                vStack.Add(vObjectStack);
            }


            return vStack;
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
                case GeneralConstant.GeneralObjectFromDB.Country:
                    add = "Countries/";
                    break;
            }
            var a = "http://" + GeneralConstant.SERVER_IP + "/WorldOfWarships/Images/" + add + path;
            return "http://" + GeneralConstant.SERVER_IP + "/WorldOfWarships/Images/" + add + path;
        }
    }
}
