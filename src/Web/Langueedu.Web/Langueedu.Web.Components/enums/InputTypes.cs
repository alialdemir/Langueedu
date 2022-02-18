using System.ComponentModel;

namespace Langueedu.Web.Components;

public enum InputTypes
{
  [Description("text")]
  Text,
  [Description("password")]
  Password,
  [Description("email")]
  Email,
  [Description("hidden")]
  Hidden,
  [Description("number")]
  Number
}
