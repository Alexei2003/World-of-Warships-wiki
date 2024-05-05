using GeneralClasses;
using WorldOfWarshipsWiki.Pages.Ships;

namespace WorldOfWarshipsWiki.Pages;

public class CountriesPage : ContentPage
{
    public CountriesPage(GeneralConstant.GeneralObjectFromDB typeObjectNext)
    {
        var imageGestureRecognizer = new TapGestureRecognizer();
        imageGestureRecognizer.Tapped += ToShipsOnButtonClicked;
        Content = GeneratorPage.GetObjectOfListPage(GeneralConstant.GeneralObjectFromDB.Countries, typeObjectNext, imageGestureRecognizer);
    }

    private async void ToShipsOnButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ShipsPage());
    }
}