namespace WorldOfWarshipsWiki.Pages.Countries;

public class CountryPage : ContentPage
{
    public CountryPage(int countryId)
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