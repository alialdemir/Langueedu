using System;
using AutoMapper;
using Langueedu.Core.Features.Commands.Account.SignUp;
using Langueedu.Infrastructure.Data;

namespace Langueedu.Infrastructure.Mapping
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, SignUpCommand>()
                .ReverseMap();
        }
    }
}

