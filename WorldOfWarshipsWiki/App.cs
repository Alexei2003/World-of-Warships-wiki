using WorldOfWarshipsWiki.Pages;

public class App : Application
{
    public App()
    {
        MainPage = new NavigationPage(new MainPage());
    }
}
