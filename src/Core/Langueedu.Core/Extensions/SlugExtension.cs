﻿using System.Globalization;
using System.Text;

namespace Langueedu.Core;

public static class SlugExtension
{
  /// <summary>
  /// Seo friendly slug
  /// </summary>
  /// <param name="text">Slug text</param>
  /// <param name="maxLength"></param>
  /// <returns></returns>
  public static string GenerateSlug(this string text)
  {
    // Return empty value if text is null
    if (text == null) return "";

    var normalizedString = text
        // Make lowercase
        .ToLowerInvariant();
    // Normalize the text
    // .Normalize(NormalizationForm.FormD);

    var stringBuilder = new StringBuilder();
    var stringLength = normalizedString.Length;
    var prevdash = false;
    var trueLength = 0;

    char c;

    for (int i = 0; i < stringLength; i++)
    {
      c = normalizedString[i];

      switch (CharUnicodeInfo.GetUnicodeCategory(c))
      {
        // Check if the character is a letter or a digit if the character is a
        // international character remap it to an ascii valid character
        case UnicodeCategory.LowercaseLetter:
        case UnicodeCategory.UppercaseLetter:
        case UnicodeCategory.DecimalDigitNumber:
          if (c < 128)
            stringBuilder.Append(c);
          else
            stringBuilder.Append(ConstHelper.RemapInternationalCharToAscii(c));

          prevdash = false;
          trueLength = stringBuilder.Length;
          break;

        // Check if the character is to be replaced by a hyphen but only if the last character wasn't
        case UnicodeCategory.SpaceSeparator:
        case UnicodeCategory.ConnectorPunctuation:
        case UnicodeCategory.DashPunctuation:
        case UnicodeCategory.OtherPunctuation:
        case UnicodeCategory.MathSymbol:
          if (!prevdash)
          {
            stringBuilder.Append('-');
            prevdash = true;
            trueLength = stringBuilder.Length;
          }
          break;
      }

    }

    // Trim excess hyphens
    var result = stringBuilder.ToString().Trim('-');

    return result;

  }

  internal static class ConstHelper
  {
    /// <summary>
    /// Remaps international characters to ascii compatible ones
    /// based of: https://meta.stackexchange.com/questions/7435/non-us-ascii-characters-dropped-from-full-profile-url/7696#7696
    /// </summary>
    /// <param name="c">Charcter to remap</param>
    /// <returns>Remapped character</returns>
    public static string RemapInternationalCharToAscii(char c)
    {
      string s = c.ToString().ToLowerInvariant();
      if ("àåáâäãåą".Contains(s))
      {
        return "a";
      }
      else if ("èéêëę".Contains(s))
      {
        return "e";
      }
      else if ("ìíîïı".Contains(s))
      {
        return "i";
      }
      else if ("òóôõöøőð".Contains(s))
      {
        return "o";
      }
      else if ("ùúûüŭů".Contains(s))
      {
        return "u";
      }
      else if ("çćčĉ".Contains(s))
      {
        return "c";
      }
      else if ("żźž".Contains(s))
      {
        return "z";
      }
      else if ("śşšŝ".Contains(s))
      {
        return "s";
      }
      else if ("ñń".Contains(s))
      {
        return "n";
      }
      else if ("ýÿ".Contains(s))
      {
        return "y";
      }
      else if ("ğĝ".Contains(s))
      {
        return "g";
      }
      else if (c == 'ř')
      {
        return "r";
      }
      else if (c == 'ł')
      {
        return "l";
      }
      else if (c == 'đ')
      {
        return "d";
      }
      else if (c == 'ß')
      {
        return "ss";
      }
      else if (c == 'þ')
      {
        return "th";
      }
      else if (c == 'ĥ')
      {
        return "h";
      }
      else if (c == 'ĵ')
      {
        return "j";
      }
      else
      {
        return "";
      }
    }
  }
}

