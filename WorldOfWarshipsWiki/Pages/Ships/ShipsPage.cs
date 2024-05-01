namespace WorldOfWarshipsWiki.Pages.Ships;

public class ShipsPage : ContentPage
{
    public ShipsPage()
    {
        var verticalStack = new VerticalStackLayout()
        {
            HorizontalOptions = LayoutOptions.Fill,
            VerticalOptions = LayoutOptions.Start,
        };



        var usersScrollView = new ScrollView
        {
            Content = verticalStack
        };

        Content = usersScrollView;
    }

    private async void ToShipOnButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ShipPage());
    }
}