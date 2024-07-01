using Cooklee.API.Errors;
using Cooklee.Core.DTOs;
using Cooklee.Core.Helpers;
using Cooklee.Data.Entities;
using Cooklee.Data.Entities.Identity;
using Cooklee.Data.Service.Contract;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;

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
        private readonly IEmailSetting _emailService;
        private static readonly Dictionary<string, string> ResetCodes = new();
        public AccountController(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            IAuthService authService,
            IEmailSetting emailService
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _authService = authService;
            _emailService = emailService;
        }

        #region Login
        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(LoginDto model)
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

            return Ok(await _authService.CreatTokenAsync(user, _userManager));
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
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            // Clear the authentication cookie
            HttpContext.Response.Cookies.Delete(".AspNetCore.Identity.Application");

            // Optionally, you can also regenerate the security stamp to invalidate all sessions
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                await _userManager.UpdateSecurityStampAsync(user);
            }

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

        #region Forgot Password
        [HttpPost("forgotpassword")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordDto forgotPasswordDto)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(forgotPasswordDto.Email);

                if (user != null)
                {
                    var resetCode = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); // Generate a 6-digit code
                    ResetCodes[forgotPasswordDto.Email] = resetCode;

                    var email = new Email
                    {
                        To = forgotPasswordDto.Email,
                        Subject = "Reset Your Password",
                        Body = $"Your password reset code is: {resetCode}"
                    };
                    _emailService.SendEmailAsync(email);
                    return Ok(new { Message = "Reset password code has been sent to your email." });
                }

                return BadRequest(new { Error = "Invalid Email" });
            }

            return BadRequest(ModelState);
        }
        #endregion

        #region Reset Password
        [HttpPost("resetpassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto resetPasswordDto)
        {
            if (ModelState.IsValid)
            {
                if (ResetCodes.TryGetValue(resetPasswordDto.Email, out var storedResetCode) && storedResetCode == resetPasswordDto.ResetCode)
                {
                    var user = await _userManager.FindByEmailAsync(resetPasswordDto.Email);
                    if (user != null)
                    {
                        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                        var result = await _userManager.ResetPasswordAsync(user, token, resetPasswordDto.Password);

                        if (result.Succeeded)
                        {
                            ResetCodes.Remove(resetPasswordDto.Email);
                            return Ok(new { Message = "Password has been reset successfully." });
                        }
                        else
                        {
                            return BadRequest(new { Errors = result.Errors.Select(e => e.Description) });
                        }
                    }
                    else
                    {
                        return BadRequest(new { Error = "Invalid Email" });
                    }
                }
                else
                {
                    return BadRequest(new { Error = "Invalid reset code." });
                }
            }

            return BadRequest(ModelState);
        }
        #endregion
    }
}