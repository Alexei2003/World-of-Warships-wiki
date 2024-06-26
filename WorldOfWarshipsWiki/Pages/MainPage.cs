using WorldOfWarshipsWiki.Pages.Achievements;
using WorldOfWarshipsWiki.Pages.Containers;
using WorldOfWarshipsWiki.Pages.Countries;
using WorldOfWarshipsWiki.Pages.Maps;
using WorldOfWarshipsWiki.Pages.PlayerLevels;

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

        var toCountries = new Button()
        {
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center,
            Text = "������"
        };
        toCountries.Clicked += ToCountriesOnButtonClicked;
        verticalStack.Add(toCountries);

        var toCountriesShips = new Button()
        {
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center,
            Text = "�������"
        };
        toCountriesShips.Clicked += ToCountriesShipsOnButtonClicked;
        verticalStack.Add(toCountriesShips);

        var toCountriesSpecialCommanders = new Button()
        {
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center,
            Text = "������ ���������"
        };
        toCountriesSpecialCommanders.Clicked += ToCountriesSpecialCommandersOnButtonClicked;
        verticalStack.Add(toCountriesSpecialCommanders);

        var toMaps = new Button()
        {
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center,
            Text = "�����"
        };
        toMaps.Clicked += ToMapsOnButtonClicked;
        verticalStack.Add(toMaps);

        var toPlayerLevels = new Button()
        {
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center,
            Text = "������ ������"
        };
        toPlayerLevels.Clicked += ToPlayerLevelsOnButtonClicked;
        verticalStack.Add(toPlayerLevels);

        var toAchievements = new Button()
        {
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center,
            Text = "����������"
        };
        toAchievements.Clicked += ToAchievementsOnButtonClicked;
        verticalStack.Add(toAchievements);

        var toContainers = new Button()
        {
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center,
            Text = "����������"
        };
        toContainers.Clicked += ToContainersOnButtonClicked;
        verticalStack.Add(toContainers);

        Content = verticalStack;
    }

    private async void ToCountriesOnButtonClicked(object sender, System.EventArgs e)
    {
        await Navigation.PushAsync(new CountriesPage(GeneralClasses.GeneralConstant.GeneralObjectFromDB.Country));
    }

    private async void ToCountriesShipsOnButtonClicked(object sender, System.EventArgs e)
    {
        await Navigation.PushAsync(new CountriesPage(GeneralClasses.GeneralConstant.GeneralObjectFromDB.Ships));
    }

    private async void ToCountriesSpecialCommandersOnButtonClicked(object sender, System.EventArgs e)
    {
        await Navigation.PushAsync(new CountriesPage(GeneralClasses.GeneralConstant.GeneralObjectFromDB.Commanders));
    }

    private async void ToMapsOnButtonClicked(object sender, System.EventArgs e)
    {
        await Navigation.PushAsync(new MapsPage());
    }

    private async void ToPlayerLevelsOnButtonClicked(object sender, System.EventArgs e)
    {
        await Navigation.PushAsync(new PlayerLevelsPage());
    }

    private async void ToAchievementsOnButtonClicked(object sender, System.EventArgs e)
    {
        await Navigation.PushAsync(new AchievementsPage());
    }

    private async void ToContainersOnButtonClicked(object sender, System.EventArgs e)
    {
        await Navigation.PushAsync(new ContainersPage());
    }
}