using GeneralClasses;
using WorldOfWarshipsWiki.Pages.Commanders;
using WorldOfWarshipsWiki.Pages.Ships;

namespace WorldOfWarshipsWiki.Pages.Countries;

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
            case GeneralConstant.GeneralObjectFromDB.Country:
                imageGestureRecognizer.Tapped += ToCountryOnButtonClicked;
                break;
        }

        Content = GeneratorPage.GetObjectOfListPage(GeneralConstant.GeneralObjectFromDB.Countries, imageGestureRecognizer);
    }

    private async void ToShipsOnButtonClicked(object sender, EventArgs e)
    {
        var id = (int)((Image)sender).BindingContext;
        await Navigation.PushAsync(new ShipsPage(id));
    }

    private async void ToCommandersOnButtonClicked(object sender, EventArgs e)
    {
        var id = (int)((Image)sender).BindingContext;
        await Navigation.PushAsync(new CommandersPage(id));
    }
    private async void ToCountryOnButtonClicked(object sender, EventArgs e)
    {
        var id = (int)((Image)sender).BindingContext;
        await Navigation.PushAsync(new CountryPage(id));
    }
}