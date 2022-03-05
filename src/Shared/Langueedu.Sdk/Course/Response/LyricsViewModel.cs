using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.Json.Serialization;

namespace Langueedu.Sdk.Course.Response
{
  public class LyricsViewModel
  {
    private string _text;

    public string Text
    {
      get
      {
        return _text;
      }
      set
      {
        _text = value;
      }
    }

    public string Answer { get; set; }

    public double Duration { get; set; }

    public int Id { get; set; }

    [JsonIgnore]
    public bool IsShowAnswer { get; set; } = true;

    [JsonIgnore]
    public bool? IsAnswerCorrect { get; set; }

    public List<StylishModel> Stylish { get; set; } = new List<StylishModel>();

    public string ToTitleCase(string text) =>
      CultureInfo.CurrentCulture.TextInfo.ToTitleCase(text.ToLower());
  }
}