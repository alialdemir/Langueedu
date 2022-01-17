using Microsoft.AspNetCore.Components;

namespace Langueedu.Web.Pages;

public partial class TrackDetail
{
    [Parameter]
    public string ArtistSlug { get; set; }

    [Parameter]
    public string AlbumSlug { get; set; }

    [Parameter]
    public string TrackSlug { get; set; }

    [Parameter]
    public int TrackId { get; set; }
}