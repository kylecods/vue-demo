using Infrastructure.Identity;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using vue_api.Models;
using vue_api.Services;

namespace vue_api.Controllers
{
    [ApiController]
    [Route("token")]
    public class AuthController : ControllerBase
    {
        private readonly ITokenService _tokenService;

        private readonly UserManager<ApplicationUser> _userManager;

        public AuthController(ITokenService tokenService, UserManager<ApplicationUser> userManager)
        {
            _tokenService = tokenService;

            _userManager = userManager;
        }

        [HttpPost]
        [Route("get")]
        public async Task<Results<Ok<AuthTokenModel>, BadRequest<AuthTokenResponse>>> Token([Required] UserModel userModel)
        {
            var user = await _userManager.FindByNameAsync(userModel.Username);

            if (user is null || !await _userManager.CheckPasswordAsync(user, userModel.Password))
            {
                return TypedResults.BadRequest(new AuthTokenResponse("Either the user does not exist or the password is invalid"));
            }

            var token = _tokenService.GenerateToken(userModel.Username);

            return TypedResults.Ok(new AuthTokenModel(token));
        }
    }
}
