namespace WorldOfWarshipsWiki.Pages.Achievements;

public class AchievementPage : ContentPage
{
	public AchievementPage()
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