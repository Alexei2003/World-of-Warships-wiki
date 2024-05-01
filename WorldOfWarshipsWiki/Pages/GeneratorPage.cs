using GeneralClasses;
using GeneralClasses.Data;

namespace WorldOfWarshipsWiki.Pages
{
    public static class GeneratorPage
    {
        public static HorizontalStackLayout GetBasePartOfItemPage(BaseDataMessage message)
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
                    Source = GetUrlImageFromPath(message.PicturePath)
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

        public static string GetUrlImageFromPath(string path)
        {
            return "http://" + GeneralConstant.SERVER_IP + "/" + path;
        }
        public enum CountriesTo
        {
            None, Ships, SpecialCommanders
        }
    }
}
