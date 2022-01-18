using System.ComponentModel;

namespace Langueedu.Web.Components;

public enum Color
{
    [Description("primary")]
    Primary,
    [Description("secondary")]
    Secondary,
    [Description("success")]
    Success,
    [Description("danger")]
    Danger,
    [Description("warning")]
    Warning,
    [Description("info")]
    Info,
    [Description("light")]
    Light,
    [Description("dark")]
    Dark,
    [Description("Link")]
    Link,
    [Description("transparent")]
    Transparent,
}