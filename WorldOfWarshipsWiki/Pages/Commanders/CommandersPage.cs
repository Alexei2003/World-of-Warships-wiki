namespace WorldOfWarshipsWiki.Pages.Commanders;

public class CommandersPage : ContentPage
{
    public CommandersPage()
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
        await Navigation.PushAsync(new CommanderPage());
    }
}