namespace Langueedu.Components.Models;

public class MenuItem
{
  public string Title { get; set; }
  public string Icon { get; set; }
  public string Href { get; set; }
  public bool IsActive { get; set; }

  public static List<MenuItem> MenuItems
  {
    get
    {
      return new List<MenuItem> {
                        new MenuItem { Title = "Learn", Href = "/Learn", Icon = "fa-solid fa-music", IsActive = true },
                        new MenuItem { Title = "Explore", Href = "/Explore", Icon = "fa-regular fa-compass", IsActive = false }
                    };
    }
  }
}
