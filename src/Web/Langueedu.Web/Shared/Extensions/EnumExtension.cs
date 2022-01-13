using System;
using System.ComponentModel;

namespace Langueedu.Web.Shared.Extensions;

public static class EnumExtension
{
    public static string ToDescriptionString(this Enum val)
    {
#nullable disable
        var attributes = (DescriptionAttribute[])val.GetType().GetField(val.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false);

        return attributes.Length > 0
            ? attributes[0].Description
            : val.ToString().ToLower();
    }
}

