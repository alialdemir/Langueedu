using Langueedu.Core.Enums;
using Langueedu.Core.Entities.PlaylistAggregate;
using Langueedu.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Langueedu.API;

public static class SeedData
{
    public static readonly Playlist Playlist = new("Türkçe pop");
    public static readonly Artist Artist = new("Sıla GençOğlu", "https://i.scdn.co/image/ab67706c0000da84f541aebb4e40b2632f39884a");
    public static readonly Artist ArtistBeduk = new("Bedük", "https://i.scdn.co/image/ab67616d00001e028196de80c29e4b4ed0138b1d");
    public static readonly Album Album = new("Mürekkep - Bedük version", DateTime.Now);
    public static readonly Track Track = new("Engerek", "RhL7B7iXgyE", "tr", 41248);

    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var dbContext = new AppDbContext(
            serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>(), null))
        {
            // Look for any TODO items.
            if (dbContext.Artists.Any())
            {
                return; // DB has been seeded
            }

            PopulateTestData(dbContext);
        }
    }
    public static void PopulateTestData(AppDbContext dbContext)
    {
        foreach (var item in dbContext.Artists)
        {
            dbContext.Remove(item);
        }
        dbContext.SaveChanges();

        Track.Id = 1;

        Track
        .ChangeContentStatus(ContentStatus.Active)
        .AddPerformsOnSong(Artist)
        .AddPerformsOnSong(ArtistBeduk);

        Album.Id = 1;

        Album
        .ChangeContentStatus(ContentStatus.Active)
        .AddTrack(Track);

        Artist
        .ChangeContentStatus(ContentStatus.Active)
        .AddAlbum(Album);

        Playlist.Id = 1;

        Playlist
        .ChangeContentStatus(ContentStatus.Active)
        .AddTrack(Track);

        dbContext.Artists.Add(Artist);
        dbContext.Playlists.Add(Playlist);

        dbContext.SaveChanges();
    }
}

