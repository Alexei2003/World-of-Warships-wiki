namespace WorldOfWarshipsWiki.Pages.Containers;

public class ContainersPage : ContentPage
{
	public ContainersPage()
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