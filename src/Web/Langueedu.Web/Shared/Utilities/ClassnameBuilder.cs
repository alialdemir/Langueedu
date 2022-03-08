namespace Langueedu.Web.Shared.Utilities;

public struct ClassnameBuilder
{
  private string stringBuffer;

  public static ClassnameBuilder Default(string value) => new(value);

  public static ClassnameBuilder Empty() => new();

  public ClassnameBuilder(string value) => stringBuffer = value;

  public ClassnameBuilder AddValue(string value)
  {
    stringBuffer += value;
    return this;
  }

  public ClassnameBuilder AddClass(string value) => AddValue(" " + value);

  public ClassnameBuilder AddClass(string value, bool when = true) => when ? this.AddClass(value) : this;

  public ClassnameBuilder AddClass(string value, bool? when = true) => when == true ? this.AddClass(value) : this;

  public ClassnameBuilder AddClass(string value, Func<bool> when = null) => this.AddClass(value, when != null && when());

  public ClassnameBuilder AddClass(Func<string> value, bool when = true) => when ? this.AddClass(value()) : this;

  public ClassnameBuilder AddClass(Func<string> value, Func<bool> when = null) => this.AddClass(value, when != null && when());

  public ClassnameBuilder AddClass(ClassnameBuilder builder, bool when = true) => when ? this.AddClass(builder.Build()) : this;

  public ClassnameBuilder AddClass(ClassnameBuilder builder, Func<bool> when = null) => this.AddClass(builder, when != null && when());

  public ClassnameBuilder AddClassFromAttributes(IReadOnlyDictionary<string, object> additionalAttributes) =>
      additionalAttributes == null ? this :
      additionalAttributes.TryGetValue("class", out var c) ? AddClass(c.ToString()) : this;

  public string Build()
  {
    // String buffer finalization code
    return stringBuffer != null ? stringBuffer.Trim() : string.Empty;
  }

  public override string ToString() => Build();

}
