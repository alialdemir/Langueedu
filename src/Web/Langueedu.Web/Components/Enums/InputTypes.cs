using System.ComponentModel;

namespace Langueedu.Components;

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
