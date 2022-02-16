﻿using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using AutoMapper;
using Langueedu.Core.Features.Commands.Account.SignUp;
using Langueedu.Core.Interfaces;
using Langueedu.Core.Validations;
using Langueedu.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;

namespace Langueedu.Infrastructure.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public AccountService(UserManager<User> userManager,
                              IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<Result<string>> SingUpAsync(SignUpCommand signUp)
        {
            if (signUp == null)
                return Result<string>.Error("User information cannot be null.");

            var validator = new SignUpCommandValidator();
            var valid = validator.Validate(signUp);
            if (!valid.IsValid)
                return Result<string>.Invalid(valid.AsErrors());

            var user = _mapper.Map<User>(signUp);

            var result = await _userManager.CreateAsync(user, signUp.Password);

            if (!result.Succeeded && result.Errors.Any())
                return Result<string>.Error(result.Errors.Select(x => x.Description).ToArray());

            bool isInRole = await _userManager.IsInRoleAsync(user, "user");
            if (!isInRole)
            {
                var roleIdentity = await _userManager.AddToRoleAsync(user, "user");

                if (!roleIdentity.Succeeded && roleIdentity.Errors.Any())
                    return Result<string>.Error(roleIdentity.Errors.Select(x => x.Description).ToArray());
            }

            return Result<string>.Success("User registred:");
        }
    }
}
