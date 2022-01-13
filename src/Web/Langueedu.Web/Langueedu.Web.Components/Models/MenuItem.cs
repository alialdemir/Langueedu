using System;
namespace Langueedu.Web.Components.Models;

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
                        new MenuItem { Title = "Home", Href = "/", Icon = "pe-7s-home", IsActive = true },
                        new MenuItem { Title = "Explore", Href = "/Explore", Icon = "pe-7s-compass", IsActive = false }
                    };
        }
    }
}