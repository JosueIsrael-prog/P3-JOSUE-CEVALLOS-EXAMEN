using System.Collections.ObjectModel;
using System.ComponentModel;
using SQLite;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace ExamenP3JCevallos.ViewModels;

public class HistoryViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    public ObservableCollection<string> CountriesHistory { get; } = new ObservableCollection<string>();
    private SQLiteAsyncConnection database;
    private static readonly string DbPath = Path.Combine(FileSystem.AppDataDirectory, "countries.db");

    public HistoryViewModel()
    {
        database = new SQLiteAsyncConnection(DbPath);
        LoadHistory();
    }

    private async void LoadHistory()
    {
        try
        {
            var countries = await database.Table<CountryDbModel>().ToListAsync();
            CountriesHistory.Clear();

            foreach (var country in countries)
            {
                CountriesHistory.Add($"Nombre País: {country.Name}, Región: {country.Region}, Link: {country.MapsLink} NombreBD: Josue Cevallos");
            }
        }
        catch (Exception ex)
        {
            await App.Current.MainPage.DisplayAlert("Error", $"No se pudo cargar el historial: {ex.Message}", "OK");
        }
    }
}
