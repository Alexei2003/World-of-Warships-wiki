using GeneralClasses;

namespace WorldOfWarshipsWiki.Pages.Commanders;

public class CommandersPage : ContentPage
{
    public CommandersPage(int contryId)
    {
        var imageGestureRecognizer = new TapGestureRecognizer();
        imageGestureRecognizer.Tapped += ToShipOnButtonClicked;
        Content = GeneratorPage.GetObjectOfListPage(GeneralConstant.GeneralObjectFromDB.Commanders, imageGestureRecognizer, contryId);
    }

    private async void ToShipOnButtonClicked(object sender, EventArgs e)
    {
        var id = (int)((Image)sender).BindingContext;
        await Navigation.PushAsync(new CommanderPage(id));
    }
}