using System;
using Langueedu.SharedKernel;
using Langueedu.SharedKernel.Interfaces;

namespace Langueedu.Core.Entities.User
{
    public interface IUser
    {
        bool IsActive { get; set; }
    }
}

