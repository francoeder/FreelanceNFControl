﻿using FreelanceNFControl.Core.Responses.Login;
using FreelanceNFControl.Domain.Entities;
using FreelanceNFControl.Domain.Interfaces;
using FreelanceNFControl.Infra.Core.Helpers;
using FreelanceNFControl.Infra.Core.Requests.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace FreelanceNFControl.Domain.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _configuration;

        public UserService(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        public async Task Add(User entity, string password)
        {
            var identityResult = await _userManager.CreateAsync(entity, password);

            if (!identityResult.Succeeded)
            {
                throw new InvalidOperationException("There was a problem creating the user.");
            }
        }

        public async Task<LoginResponse> AuthenticateUser(User user, string password)
        {
            return await SignInAsync(user, password);
        }

        public async Task<LoginResponse> AuthenticateUser(LoginRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.UserNameOrEmail) ??
                await _userManager.FindByNameAsync(request.UserNameOrEmail);

            return await SignInAsync(user, request.Password);
        }

        #region Private Methods

        private async Task<LoginResponse> SignInAsync(User user, string password)
        {
            var signinResult = await _signInManager
                .PasswordSignInAsync(user, password, true, lockoutOnFailure: false);

            if (!signinResult.Succeeded)
            {
                throw new UnauthorizedAccessException("Invalid or unauthorized user.");
            }

            var result = GetLoginResponse(user);

            return result;
        }

        private LoginResponse GetLoginResponse(User user)
        {
            var loginResponse = new LoginResponse() 
            {
                UserId = new Guid(user.Id),
                Email = user.Email,
                Name = user.FirstName,
                AccessToken = GenerateJwtToken(user)
            };

            return loginResponse;
        }

        public string GenerateJwtToken(User user)
        {
            var claimsPrincipal = _signInManager.CreateUserPrincipalAsync(user).Result;

            var secret = _configuration["AuthenticationJWTSecret"];
            var expirationHours = 24;
            var expirationDate = DateTime.UtcNow.AddHours(expirationHours);

            var token = JwtHelper.GenerateToken(claimsPrincipal.Claims, secret, expirationDate);

            return token;
        }

        #endregion

    }
}
