namespace WorldOfWarshipsWiki.Pages.Achievements;

public class AchievementsPage : ContentPage
{
	public AchievementsPage()
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