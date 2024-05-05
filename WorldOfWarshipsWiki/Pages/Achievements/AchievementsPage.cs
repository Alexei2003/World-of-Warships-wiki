using GeneralClasses;

namespace WorldOfWarshipsWiki.Pages.Achievements;

public class AchievementsPage : ContentPage
{
    public AchievementsPage()
    {
        var imageGestureRecognizer = new TapGestureRecognizer();
        imageGestureRecognizer.Tapped += OnButtonClicked;
        Content = GeneratorPage.GetObjectOfListPage(GeneralConstant.GeneralObjectFromDB.Achievements, imageGestureRecognizer);
    }

    private async void OnButtonClicked(object sender, EventArgs e)
    {
        var id = (int)((Image)sender).BindingContext;
        await Navigation.PushAsync(new AchievementPage(id));
    }
}