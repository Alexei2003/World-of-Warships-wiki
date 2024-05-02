using GeneralClasses;
using WorldOfWarshipsWiki.Pages.Ships;

namespace WorldOfWarshipsWiki.Pages;

public class CountriesPage : ContentPage
{
    public CountriesPage(GeneralConstant.GeneralObjectFromDB typeObjectNext)
    {
        Content = GeneratorPage.GetObjectOfListPage(GeneralConstant.GeneralObjectFromDB.Country, typeObjectNext);
    }

    private async void ToShipsOnButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ShipsPage());
    }
}