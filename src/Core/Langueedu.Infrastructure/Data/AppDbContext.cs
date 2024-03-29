﻿using Ardalis.EFCore.Extensions;
using Langueedu.Core.Entities.BalanceAggregate;
using Langueedu.Core.Entities.CourseAggregate;
using Langueedu.Core.Entities.LanguageAggregate;
using Langueedu.Core.Entities.PlaylistAggregate;
using Langueedu.SharedKernel;
using MediatR;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Langueedu.Infrastructure.Data;

public class AppDbContext : IdentityDbContext<User>
{
  private readonly IMediator _mediator;

  public AppDbContext(DbContextOptions<AppDbContext> options, IMediator mediator)
      : base(options)
  {
    _mediator = mediator;
  }

  public DbSet<Balance> Balances => Set<Balance>();
  public DbSet<BalanceHistory> BalanceHistories => Set<BalanceHistory>();

  public DbSet<Course> Courses => Set<Course>();
  public DbSet<CourseParticipant> CourseParticipants => Set<CourseParticipant>();
  public DbSet<CourseParticipantDetail> CourseParticipantDetails => Set<CourseParticipantDetail>();

  public DbSet<Language> Languages => Set<Language>();

  public DbSet<Album> Albums => Set<Album>();
  public DbSet<AlbumGenre> AlbumGenres => Set<AlbumGenre>();
  public DbSet<Artist> Artists => Set<Artist>();
  public DbSet<ArtistGenre> ArtistGenres => Set<ArtistGenre>();
  public DbSet<FollowerTrack> FollowerTracks => Set<FollowerTrack>();
  public DbSet<Genre> Genres => Set<Genre>();
  public DbSet<Image> Images => Set<Image>();
  public DbSet<PlayList> Lyrics => Set<PlayList>();
  public DbSet<PerformsOnSong> PerformsOnSongs => Set<PerformsOnSong>();
  public DbSet<PlayList> Playlists => Set<PlayList>();
  public DbSet<Track> Tracks => Set<Track>();

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);

    modelBuilder.ApplyAllConfigurationsFromCurrentAssembly();
  }

  public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
  {
    int result = await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

    // ignore events if no dispatcher provided
    if (_mediator == null)
      return result;

    // dispatch events only if save was successful
    var entitiesWithEvents = ChangeTracker.Entries<BaseEntity>()
        .Select(e => e.Entity)
        .Where(e => e.Events.Any())
        .ToArray();

    foreach (var entity in entitiesWithEvents)
    {
      var events = entity.Events.ToArray();
      entity.Events.Clear();
      foreach (var domainEvent in events)
      {
        await _mediator.Publish(domainEvent).ConfigureAwait(false);
      }
    }

    return result;
  }

  public override int SaveChanges()
  {
    return SaveChangesAsync().GetAwaiter().GetResult();
  }
}

