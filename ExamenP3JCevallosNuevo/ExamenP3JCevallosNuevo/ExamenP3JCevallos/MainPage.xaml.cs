using ExamenP3JCevallos.ViewModels;

namespace ExamenP3JCevallos;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
        BindingContext = new CountriesViewModel();
    }
    private async void OnHistoryClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new HistoryPage());
    }

}
