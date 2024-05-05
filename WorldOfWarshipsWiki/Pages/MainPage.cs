using WorldOfWarshipsWiki.Pages.Countries;

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

        var toCountry = new Button()
        {
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center,
            Text = "Страны"
        };
        toCountry.Clicked += ToCountriesOnButtonClicked;
        verticalStack.Add(toCountry);

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

        var toMaps = new Button()
        {
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center,
            Text = "Страны"
        };
        toMaps.Clicked += ToMapsOnButtonClicked;
        verticalStack.Add(toMaps);

        var toPlayerLevels = new Button()
        {
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center,
            Text = "Страны"
        };
        toPlayerLevels.Clicked += ToPlayerLevelsOnButtonClicked;
        verticalStack.Add(toPlayerLevels);

        var toAchievements = new Button()
        {
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center,
            Text = "Страны"
        };
        toAchievements.Clicked += ToAchievementsOnButtonClicked;
        verticalStack.Add(toAchievements);

        var toContainers = new Button()
        {
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center,
            Text = "Страны"
        };
        toContainers.Clicked += ToContainersOnButtonClicked;
        verticalStack.Add(toContainers);

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

    private async void ToCountriesOnButtonClicked(object sender, System.EventArgs e)
    {
        await Navigation.PushAsync(new CountriesPage(GeneralClasses.GeneralConstant.GeneralObjectFromDB.Country));
    }

    private async void ToMapsOnButtonClicked(object sender, System.EventArgs e)
    {
        //await Navigation.PushAsync(new CountriesPage(GeneralClasses.GeneralConstant.GeneralObjectFromDB.Country));
    }

    private async void ToPlayerLevelsOnButtonClicked(object sender, System.EventArgs e)
    {
        //await Navigation.PushAsync(new CountriesPage(GeneralClasses.GeneralConstant.GeneralObjectFromDB.Country));
    }

    private async void ToAchievementsOnButtonClicked(object sender, System.EventArgs e)
    {
        //await Navigation.PushAsync(new CountriesPage(GeneralClasses.GeneralConstant.GeneralObjectFromDB.Country));
    }

    private async void ToContainersOnButtonClicked(object sender, System.EventArgs e)
    {
        //await Navigation.PushAsync(new CountriesPage(GeneralClasses.GeneralConstant.GeneralObjectFromDB.Country));
    }
}