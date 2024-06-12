﻿using Cooklee.API.Errors;
using Cooklee.Core.DTOs;
using Cooklee.Data.Entities.Identity;
using Cooklee.Data.Service.Contract;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Cooklee.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : BaseApiController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IAuthService _authService;

        public AccountController(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            IAuthService authService
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _authService = authService;
        }

        #region Login
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user is null)
            {
                return Unauthorized(new ApiResponse(401));
            }
            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
            if (!result.Succeeded)
            {
                return Unauthorized(new ApiResponse(401));
            }

            return Ok(new UserDto
            {
                Email = user.Email,
                Token = await _authService.CreatTokenAsync(user, _userManager)
            });
        }
        #endregion

        #region loginWithGoogle

        [HttpPost("loginWithGoogle")]
        public async Task<ActionResult<UserDto>> LoginWithGoogle([FromBody] GoogleLoginDto googleLoginDto)
        {
            GoogleJsonWebSignature.Payload payload;

            try
            {
                payload = await GoogleJsonWebSignature.ValidateAsync(googleLoginDto.Credential);
            }
            catch (InvalidJwtException ex)
            {
                return Unauthorized(new ApiResponse(401, "Invalid Google token."));
            }

            var user = await _userManager.FindByEmailAsync(payload.Email);
            if (user == null)
            {
                user = new AppUser
                {
                    UserName = payload.Email,
                    Email = payload.Email,
                    DisplayName = payload.Name,
                    EmailConfirmed = true
                };

                var result = await _userManager.CreateAsync(user);
                if (!result.Succeeded)
                {
                    var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                    return BadRequest(new ApiResponse(400, "Failed to create user account. " + errors));
                }
            }

            var token = await _authService.CreatTokenAsync(user, _userManager);

            return Ok(new UserDto
            {
                Email = user.Email,
                Token = token
            });
        }
        #endregion

        #region Register
        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiValidationErrorResponse
                {
                    Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToArray()
                });
            }

            if (CheckEmailExists(model.Email).Result.Value)
            {
                return BadRequest(new ApiValidationErrorResponse
                {
                    Errors = new string[] { "This email is already in use!!" }
                });
            }

            if (model.Password != model.ConfirmPassword)
            {
                return BadRequest(new ApiValidationErrorResponse
                {
                    Errors = new string[] { "The password and confirmation password do not match." }
                });
            }

            var user = new AppUser
            {
                Email = model.Email,
                UserName = model.Email.Split("@")[0]
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                return BadRequest(new ApiResponse(400));
            }

            return Ok(new UserDto
            {
                Email = user.Email,
                Token = await _authService.CreatTokenAsync(user, _userManager)
            });
        }



        #endregion

        #region Logout
        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return NoContent();
        } 
        #endregion

        #region CheckEmailExists
        [HttpGet("emailexists")]
        public async Task<ActionResult<bool>> CheckEmailExists(string email)
        {
            return await _userManager.FindByEmailAsync(email) is not null;
        }
        #endregion

        #region Login with Google
        //private async Task<AuthenticateResult> AuthenticateGoogleAsync()
        //{
        //    return await HttpContext.AuthenticateAsync(GoogleDefaults.AuthenticationScheme);
        //}

        //private (string, string) ProcessClaims(IEnumerable<Claim> claims)
        //{
        //    var email = claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
        //    var name = claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
        //    return (email, name);
        //}

        //[HttpGet]
        //public async Task<IActionResult> GoogleResponse()
        //{
        //    var result = await AuthenticateGoogleAsync();

        //    if (!result.Succeeded || result.Principal == null)
        //    {
        //        return Unauthorized(new { message = "Authentication failed." });
        //    }

        //    var claims = result.Principal.Identities.FirstOrDefault()?.Claims;

        //    if (claims == null || !claims.Any())
        //    {
        //        return BadRequest(new { message = "No claims found." });
        //    }

        //    var (email, name) = ProcessClaims(claims);

        //    return Ok(new
        //    {
        //        message = "Logged in with Google successfully",
        //        email = email,
        //        name = name
        //    });
        //}

        //[HttpGet("login-google")]
        //public IActionResult LoginGoogle()
        //{
        //    var properties = new AuthenticationProperties { RedirectUri = Url.Action("GoogleResponse") };
        //    return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        //}

        //[HttpGet("signin-google")]
        //public async Task<IActionResult> GoogleResponse()
        //{
        //    var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        //    if (!result.Succeeded)
        //    {
        //        return Unauthorized();
        //    }

        //    var claims = result.Principal.Identities.FirstOrDefault()?.Claims;
        //    var (email, name) = ProcessClaims(claims);

        //    var user = await _userManager.FindByEmailAsync(email);
        //    if (user == null)
        //    {
        //        user = new AppUser
        //        {
        //            UserName = email,
        //            Email = email,
        //            DisplayName = name
        //        };
        //        await _userManager.CreateAsync(user);
        //    }

        //    await _signInManager.SignInAsync(user, isPersistent: false);

        //    return Redirect("/ClientForm");
        //}

        //private (string, string) ProcessClaims(IEnumerable<Claim> claims)
        //{
        //    var email = claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
        //    var name = claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
        //    return (email, name);
        //}

        #endregion
    }
}
