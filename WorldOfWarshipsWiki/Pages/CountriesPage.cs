using WorldOfWarshipsWiki.Pages.Ships;

namespace WorldOfWarshipsWiki.Pages;

public class CountriesPage : ContentPage
{
    public CountriesPage(GeneratorPage.CountriesTo countriesTo)
    {
        switch (countriesTo)
        {
            case GeneratorPage.CountriesTo.Ships:
                break;
            case GeneratorPage.CountriesTo.SpecialCommanders:
                break;
        }

        var verticalStack = new VerticalStackLayout()
        {
            HorizontalOptions = LayoutOptions.Fill,
            VerticalOptions = LayoutOptions.Start,
        };



        var usersScrollView = new ScrollView
        {
            Content = verticalStack
        };

        Content = usersScrollView;
    }

    private async void ToShipsOnButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ShipsPage());
    }
}