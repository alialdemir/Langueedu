using System.ComponentModel;

namespace Langueedu.Web.Components;

public enum LinkTarget
{
  [Description("_blank")]
  Blank,
  [Description("_self")]
  Self,
  [Description("_parent")]
  Parent,
  [Description("_top")]
  Top,
  [Description("framename")]
  Framename,
}
