namespace WorldOfWarshipsWiki.Pages;

public class MainPage : ContentPage
{
    public MainPage()
    {
        Content = new VerticalStackLayout
        {
            Children = {
                new Image
                {
                    Source = "http://192.168.148.22/Images/maxresdefault",
                }
            }
        };
    }
}