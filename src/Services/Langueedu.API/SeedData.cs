using System.Reflection;
using System.Text;
using Langueedu.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Langueedu.API;

public static class SeedData
{
  public static void Initialize(IServiceProvider serviceProvider)
  {
    using (var dbContext = new AppDbContext(
        serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>(), null))
    {
      // Look for any TODO items.
      if (dbContext.Users.Any())
      {
        return; // DB has been seeded
      }

      dbContext.Database.EnsureDeleted();

      PopulateTestData(dbContext);
    }
  }
  public static async void PopulateTestData(AppDbContext dbContext)
  {
    var assembly = typeof(SeedData).GetTypeInfo().Assembly;
    var sampleDataSql = assembly.GetManifestResourceNames().Where(x => x.StartsWith("Langueedu.API.SampleData"));
    foreach (var sqlFileName in sampleDataSql)
    {

      using (var stream = assembly.GetManifestResourceStream(sqlFileName))
      using (var reader = new StreamReader(stream, Encoding.UTF8))
      {
        string sql = reader.ReadToEnd();

        int changedRows = await dbContext.Database.ExecuteSqlRawAsync(sql);
      }
    }
  }
}