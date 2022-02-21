using Langueedu.Core.Entities.PlaylistAggregate;
using Langueedu.SharedKernel;
using Langueedu.SharedKernel.Interfaces;

namespace Langueedu.Core.Entities.LanguageAggregate;

public class Language : BaseEntityNoId, IAggregateRoot
{
  public Language(string lang,
                  string? langCc = null)
  {
    Lang = lang.ToUpper();

    SetLangCc(langCc);
  }

  public string Lang { get; private set; }

  public string? LangCc { get; private set; }

  public Language SetLangCc(string? langCc)
  {
    if (!string.IsNullOrEmpty(langCc))
      LangCc = langCc.ToUpper();

    return this;
  }

  private readonly List<Track> _tracks = new();

  public IReadOnlyCollection<Track> Tracks => _tracks.AsReadOnly();
}

