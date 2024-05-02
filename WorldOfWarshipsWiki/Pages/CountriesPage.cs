using GeneralClasses.Data.Request;
using WorldOfWarshipsWiki.Pages.Ships;

namespace WorldOfWarshipsWiki.Pages;

public class CountriesPage : ContentPage
{
    public CountriesPage(GeneratorPage.CountriesTo countriesTo)
    {

        var request = new RequestListMessage()
        {
            Action = GeneralClasses.GeneralConstant.GeneralServerActions.Get,
            TopicFromServer = RabbitMQ.TopicFromServer,
            ObjectName = GeneralClasses.GeneralConstant.GeneralObjectFromDB.Countries
        };

        switch (countriesTo)
        {
            case GeneratorPage.CountriesTo.Ships:
                break;
            case GeneratorPage.CountriesTo.SpecialCommanders:
                break;
        }


        RabbitMQ.Publisher.SendMessage(request.ToJson());
        RabbitMQ.Consumer.GetMessage();

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

    private async void ToShipsOnButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ShipsPage());
    }
}