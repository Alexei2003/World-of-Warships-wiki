using GeneralClasses;

namespace WorldOfWarshipsWiki.Pages.PlayerLevels;

public class PlayerLevelsPage : ContentPage
{
    public PlayerLevelsPage()
    {
        var imageGestureRecognizer = new TapGestureRecognizer();
        imageGestureRecognizer.Tapped += OnButtonClicked;
        Content = GeneratorPage.GetObjectOfListPage(GeneralConstant.GeneralObjectFromDB.PlayerLevels, imageGestureRecognizer);
    }

    private async void OnButtonClicked(object sender, EventArgs e)
    {
        var id = (int)((Image)sender).BindingContext;
        await Navigation.PushAsync(new PlayerLevelPage(id));
    }
}