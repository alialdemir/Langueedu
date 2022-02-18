using Langueedu.Core.Entities.PlaylistAggregate;
using Langueedu.Core.Enums;
using Langueedu.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Langueedu.API;

public static class SeedData
{
  public static readonly Playlist Playlist = new("Türkçe pop");
  public static readonly Artist Artist = new("Sıla GençOğlu", "https://i.scdn.co/image/ab67706c0000da84f541aebb4e40b2632f39884a");
  public static readonly Artist ArtistBeduk = new("Bedük", "https://i.scdn.co/image/ab67616d00001e028196de80c29e4b4ed0138b1d");
  public static readonly Album Album = new("Mürekkep - Bedük version");
  public static readonly Track Track = new("Engerek", "RhL7B7iXgyE", "tr", 41248);

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

      PopulateTestData(dbContext);
    }
  }
  public static async void PopulateTestData(AppDbContext dbContext)
  {
    //foreach (var item in dbContext.Artists)
    //{
    //  dbContext.Remove(item);
    //}
    //dbContext.SaveChanges();

    if (!dbContext.Roles.Any())
    {
      dbContext.Roles.AddRange(GetRoles());

      dbContext.SaveChanges();
    }

    if (!dbContext.Users.Any())
    {
      dbContext.Users.AddRange(GetDefaultUser());

      dbContext.SaveChanges();
    }

    //Track.Id = 1;

    ArtistBeduk
    .ChangeCoverPicture("https://i.scdn.co/image/ab67618600001016dc99f737fcb8abd5a063f928");

    Track
    .ChangeContentStatus(ContentStatus.Active)
    //.AddFollowerTrack(new FollowerTrack
    //{
    //  UserId = GetDefaultUser().Last().Id,
    //  TrackId = Track.Id,
    //  Id = 1
    //})
    .AddPerformsOnSong(Artist)
    .AddPerformsOnSong(ArtistBeduk);

    //Album.Id = 1;

    Album
    .ChangeContentStatus(ContentStatus.Active)
    .SetReleaseDate(DateTime.Now)
    .AddTrack(Track)
    ;

    Artist
    .ChangeContentStatus(ContentStatus.Active)
    .AddAlbum(Album)
    .ChangeCoverPicture("https://i.scdn.co/image/ab67618600001016dc99f737fcb8abd5a063f928");

    //Playlist.Id = 1;

    Playlist
    .ChangeContentStatus(ContentStatus.Active)
    .AddTrack(Track);

    dbContext.Artists.Add(Artist);
    dbContext.Playlists.Add(Playlist);

    dbContext.SaveChanges();
  }

  private static List<IdentityRole> GetRoles()
  {
    IdentityRole adminRole = new IdentityRole
    {
      Id = "admin",
      Name = "Admin",
      NormalizedName = "ADMIN"
    };

    IdentityRole userRole = new IdentityRole
    {
      Id = "user",
      Name = "User",
      NormalizedName = "USER"
    };

    return new List<IdentityRole>
            {
                adminRole,
                userRole
            };
  }

  private static IEnumerable<User> GetDefaultUser()
  {
    IPasswordHasher<User> _passwordHasher = new PasswordHasher<User>();

    var witcherUser =
        new User()
        {
          Id = "1111-1111-1111-1111",
          UserName = "witcherfearless",
          Email = "aldemirali93@gmail.com",
          PhoneNumber = "5444261154",
          NormalizedEmail = "ALDEMIRALI93@GMAIL.COM",
          NormalizedUserName = "WITCHERFEARLESS",
          LanguageCode = "tr_TR",
          SecurityStamp = Guid.NewGuid().ToString("D"),
        };

    witcherUser.PasswordHash = _passwordHasher.HashPassword(witcherUser, "12345678");

    var demoUser =
    new User()
    {
      Id = "2222-2222-2222-2222",
      UserName = "demo",
      PhoneNumber = "0000000000",
      NormalizedUserName = "DEMO",
      Email = "demo@demo.com",
      NormalizedEmail = "DEMO@DEMO.COM",
      LanguageCode = "tr_TR",
      SecurityStamp = Guid.NewGuid().ToString("D"),
    };

    demoUser.PasswordHash = _passwordHasher.HashPassword(demoUser, "12345678");

    var guestUser =
    new User()
    {
      Id = "3333-3333-3333-bot",
      UserName = "guest123",
      PhoneNumber = "1234567890",
      NormalizedUserName = "GUEST123",
      Email = "guest@guest.com",
      NormalizedEmail = "GUEST@GUEST.COM",
      LanguageCode = "en_US",
      SecurityStamp = Guid.NewGuid().ToString("D"),
    };

    guestUser.PasswordHash = _passwordHasher.HashPassword(guestUser, "12345678");

    return new List<User>()
            {
               witcherUser,
               demoUser,
               guestUser
};
  }

}

