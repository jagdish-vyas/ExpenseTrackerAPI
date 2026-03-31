using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ExpenseTracker.Api.Models;

namespace ExpenseTracker.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;

        // Injecting IConfiguration to read secret key from appsettings.json
        public AuthController(IConfiguration config)
        {
            _config = config;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserLogin userLogin)
        {
            // Dummy user validation (In a production app, verify against the database)
            if (userLogin.Username == "admin" && userLogin.Password == "password123")
            {
                var token = GenerateToken(userLogin.Username);
                return Ok(new { token });
            }

            return Unauthorized("Invalid credentials. Please try again.");
        }

        private string GenerateToken(string username)
        {
            // Fetch the secret key from configuration
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // Add claims (user-specific data payload)
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            // Create the token structure
            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30), // Token valid for 30 minutes
                signingCredentials: credentials);

            // Return the serialized token string
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}