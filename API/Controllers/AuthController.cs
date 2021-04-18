using API.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace API.Controllers
{
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly JwtSettings _jwtSettings;

        public AuthController(IOptions<JwtSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;
        }

        [Route("/api/token/wrong")]
        [HttpGet]
        public IActionResult Wrong()
            //=> throw new ArgumentException("The paramters can not be empty or null.");
            => throw new P4pException("The paramters can not be empty or null.");

        [Route("/api/token/request")]
        [HttpPost]
        public IActionResult Token(UserDto user)
        {
            if (user.Password != "123456")
                throw new P4pException("The paramters can not be empty or null.");
            //return BadRequest("The password error.");

            var claims = new List<Claim> { new Claim(ClaimTypes.Name, user.UserName) };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
            var credential = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(new JwtHeader(credential),
                                             new JwtPayload(issuer: _jwtSettings.Issuer,
                                                            audience: _jwtSettings.Audience,
                                                            claims: claims,
                                                            notBefore: DateTime.UtcNow,
                                                            expires: DateTime.UtcNow.AddMinutes(1)));
            var tokenStr = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(new TokenResponse { Token = tokenStr });
        }

        [HttpGet]
        [Authorize]
        [Route("/api/token")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public IActionResult GetData()
        {
            return Ok(new TokenResponse { Token = "N/A" });
        }
    }
}
