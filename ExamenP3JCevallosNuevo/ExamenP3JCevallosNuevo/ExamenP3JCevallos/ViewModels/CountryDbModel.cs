using Microsoft.EntityFrameworkCore;
using SQLite;
using SQLitePCL;
using PrimaryKeyAttribute = SQLite.PrimaryKeyAttribute;

namespace ExamenP3JCevallos.ViewModels;

public class CountryDbModel
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Region { get; set; }
    public string MapsLink { get; set; }
}
