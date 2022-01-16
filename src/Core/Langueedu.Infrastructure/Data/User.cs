using System;
using Langueedu.Core.Entities.User;
using Microsoft.AspNetCore.Identity;

namespace Langueedu.Infrastructure.Data
{
    public partial class User : IdentityUser, IUser
    {
        public string LanguageCode { get; set; }
        public bool IsActive { get; set; }
    }
}

