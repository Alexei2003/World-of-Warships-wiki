namespace WorldOfWarshipsWiki.Pages.PlayerLevels;

public class PlayerLevelPage : ContentPage
{
    public PlayerLevelPage(int playerLevelId)
    {
        Content = new VerticalStackLayout
        {
            Children = {
                new Label { HorizontalOptions = LayoutOptions.Center, VerticalOptions = LayoutOptions.Center, Text = "Welcome to .NET MAUI!"
                }
            }
        };
    }
}