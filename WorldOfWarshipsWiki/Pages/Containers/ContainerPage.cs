namespace WorldOfWarshipsWiki.Pages.Containers;

public class ContainerPage : ContentPage
{
	public ContainerPage(int containerId)
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