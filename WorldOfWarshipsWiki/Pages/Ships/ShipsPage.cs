using GeneralClasses;

namespace WorldOfWarshipsWiki.Pages.Ships;

public class ShipsPage : ContentPage
{
    public ShipsPage(int contryId)
    {
        var imageGestureRecognizer = new TapGestureRecognizer();
        imageGestureRecognizer.Tapped += ToShipOnButtonClicked;
        Content = GeneratorPage.GetObjectOfListPage(GeneralConstant.GeneralObjectFromDB.Ships, imageGestureRecognizer, contryId);
    }

    private async void ToShipOnButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ShipPage());
    }
}