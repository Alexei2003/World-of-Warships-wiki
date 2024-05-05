using GeneralClasses;

namespace WorldOfWarshipsWiki.Pages.Maps;

public class MapsPage : ContentPage
{
    public MapsPage()
    {
        var imageGestureRecognizer = new TapGestureRecognizer();
        imageGestureRecognizer.Tapped += OnButtonClicked;
        Content = GeneratorPage.GetObjectOfListPage(GeneralConstant.GeneralObjectFromDB.PlayerLevels, imageGestureRecognizer);
    }

    private async void OnButtonClicked(object sender, EventArgs e)
    {
        var id = (int)((Image)sender).BindingContext;
        await Navigation.PushAsync(new MapPage(id));
    }
}