using System.ComponentModel;

namespace Langueedu.Web.Components;

public enum ButtonTypes
{
    [Description("button")]
    Button,
    [Description("submit")]
    Submit,
    [Description("reset")]
    Reset
}