using ExamenP3JCevallos.ViewModels;

namespace ExamenP3JCevallos;

public partial class HistoryPage : ContentPage
{
	public HistoryPage()
	{
		InitializeComponent();
        BindingContext = new HistoryViewModel();
    }
}