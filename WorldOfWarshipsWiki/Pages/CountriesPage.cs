using GeneralClasses;
using WorldOfWarshipsWiki.Pages.Commanders;
using WorldOfWarshipsWiki.Pages.Ships;

namespace WorldOfWarshipsWiki.Pages;

public class CountriesPage : ContentPage
{
    public CountriesPage(GeneralConstant.GeneralObjectFromDB typeObjectNext)
    {
        var imageGestureRecognizer = new TapGestureRecognizer();
        switch (typeObjectNext)
        {
            case GeneralConstant.GeneralObjectFromDB.Ships:
                imageGestureRecognizer.Tapped += ToShipsOnButtonClicked;
                break;
            case GeneralConstant.GeneralObjectFromDB.Commanders:
                imageGestureRecognizer.Tapped += ToCommandersOnButtonClicked;
                break;
        }

        Content = GeneratorPage.GetObjectOfListPage(GeneralConstant.GeneralObjectFromDB.Countries, imageGestureRecognizer);
    }

    private async void ToShipsOnButtonClicked(object sender, EventArgs e)
    {
        var countryId = (int)((Image)sender).BindingContext;
        await Navigation.PushAsync(new ShipsPage(countryId));
    }

    private async void ToCommandersOnButtonClicked(object sender, EventArgs e)
    {
        var countryId = (int)((Image)sender).BindingContext;
        await Navigation.PushAsync(new CommandersPage(countryId));
    }
}