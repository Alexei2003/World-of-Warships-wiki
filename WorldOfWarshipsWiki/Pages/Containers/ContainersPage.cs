using GeneralClasses;

namespace WorldOfWarshipsWiki.Pages.Containers;

public class ContainersPage : ContentPage
{
    public ContainersPage()
    {
        var imageGestureRecognizer = new TapGestureRecognizer();
        imageGestureRecognizer.Tapped += OnButtonClicked;
        Content = GeneratorPage.GetObjectOfListPage(GeneralConstant.GeneralObjectFromDB.Container, imageGestureRecognizer);
    }

    private async void OnButtonClicked(object sender, EventArgs e)
    {
        var id = (int)((Image)sender).BindingContext;
        await Navigation.PushAsync(new ContainerPage(id));
    }
}