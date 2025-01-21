using ExamenP3JCevallos.ViewModels;
using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public DbSet<Country> Countries { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string dbPath = Path.Combine(FileSystem.AppDataDirectory, "MyCountriesDb.db");
        optionsBuilder.UseSqlite($"Filename={dbPath}");
    }
}