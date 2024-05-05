namespace WorldOfWarshipsWiki.Pages.PlayerLevels;

public class PlayerLevelsPage : ContentPage
{
	public PlayerLevelsPage()
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