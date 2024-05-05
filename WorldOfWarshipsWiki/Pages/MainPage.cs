namespace WorldOfWarshipsWiki.Pages;

public class MainPage : ContentPage
{
    public MainPage()
    {
        var verticalStack = new VerticalStackLayout()
        {
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center,
            Spacing = 10,
        };

        var toCountriesShips = new Button()
        {
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center,
            Text = "Коробли"
        };
        toCountriesShips.Clicked += ToCountriesShipsOnButtonClicked;
        verticalStack.Add(toCountriesShips);

        var toCountriesSpecialCommanders = new Button()
        {
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center,
            Text = "Особые командиры"
        };
        toCountriesSpecialCommanders.Clicked += ToCountriesSpecialCommandersOnButtonClicked;
        verticalStack.Add(toCountriesSpecialCommanders);

        var toCountry = new Button()
        {
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center,
            Text = "Особые командиры"
        };
        toCountry.Clicked += ToCountriesSpecialCommandersOnButtonClicked;
        verticalStack.Add(toCountry);

        Content = verticalStack;
    }

    private async void ToCountriesShipsOnButtonClicked(object sender, System.EventArgs e)
    {
        await Navigation.PushAsync(new CountriesPage(GeneralClasses.GeneralConstant.GeneralObjectFromDB.Ships));
    }

    private async void ToCountriesSpecialCommandersOnButtonClicked(object sender, System.EventArgs e)
    {
        await Navigation.PushAsync(new CountriesPage(GeneralClasses.GeneralConstant.GeneralObjectFromDB.Commanders));
    }

    private async void ToCountryOnButtonClicked(object sender, System.EventArgs e)
    {
        //await Navigation.PushAsync(new CountriesPage(GeneralClasses.GeneralConstant.GeneralObjectFromDB.SpecialCommanders));
    }
}