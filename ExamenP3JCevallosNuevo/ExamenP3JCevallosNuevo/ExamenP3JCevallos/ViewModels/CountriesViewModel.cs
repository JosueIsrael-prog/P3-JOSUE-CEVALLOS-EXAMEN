using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SQLite;
using Microsoft.Maui.Controls;

namespace ExamenP3JCevallos.ViewModels;

public class CountriesViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    public ObservableCollection<Country> Countries { get; } = new ObservableCollection<Country>();
    private HttpClient client = new HttpClient();

    private SQLiteAsyncConnection database;
    private static readonly string DbPath = Path.Combine(FileSystem.AppDataDirectory, "countries.db");

    private string searchText;
    public string SearchText
    {
        get => searchText;
        set
        {
            if (searchText != value)
            {
                searchText = value;
                OnPropertyChanged(nameof(SearchText));
            }
        }
    }

    public CountriesViewModel()
    {
        database = new SQLiteAsyncConnection(DbPath);
        InitializeDatabase();
    }

    private async void InitializeDatabase()
    {
        await database.CreateTableAsync<CountryDbModel>();
    }

    public Command<string> SearchCountryCommand => new Command<string>(async (name) => await SearchCountry(name));
    public Command ClearCommand => new Command(() => Countries.Clear());

    public async Task SearchCountry(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            await App.Current.MainPage.DisplayAlert("Error", "Por favor, ingresa un nombre de país.", "OK");
            return;
        }

        try
        {
            var response = await client.GetAsync($"https://restcountries.com/v3.1/name/{name}?fields=name,region,maps");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var countries = JsonConvert.DeserializeObject<List<Country>>(json);

                Countries.Clear();
                foreach (var country in countries)
                {
                    Countries.Add(country);

                    var countryDb = new CountryDbModel
                    {
                        Name = country.CommonName,
                        Region = country.Region,
                        MapsLink = country.MapsLink
                    };

                    // Verifica si el país ya está en la base de datos antes de insertarlo
                    var existing = await database.Table<CountryDbModel>()
                                                 .Where(c => c.Name == countryDb.Name)
                                                 .FirstOrDefaultAsync();

                    if (existing == null)
                    {
                        await database.InsertAsync(countryDb);
                    }
                }
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Error", "País no encontrado.", "OK");
            }
        }
        catch (Exception ex)
        {
            await App.Current.MainPage.DisplayAlert("Error", $"Algo salió mal: {ex.Message}", "OK");
        }
    }
}
