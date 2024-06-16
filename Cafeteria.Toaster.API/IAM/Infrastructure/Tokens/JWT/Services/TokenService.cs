using Microsoft.IdentityModel.JsonWebTokens;
using System.Security.Claims;
using System.Text;
using ACME.LearningCenterPlatform.API.IAM.Application.Internal.OutboundServices;
using ACME.LearningCenterPlatform.API.IAM.Domain.Model.Aggregates;
using ACME.LearningCenterPlatform.API.IAM.Infrastructure.Tokens.JWT.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace ACME.LearningCenterPlatform.API.IAM.Infrastructure.Tokens.JWT.Services
{
    /// <summary>
    /// The token service
    /// </summary>
    /// <remarks>
    /// This class is used to generate and validate tokens.
    /// Author: Jherson Astuyauri
    /// </remarks>
    public class TokenService : ITokenService
    {
        private readonly TokenSettings _tokenSettings;

        /// <summary>
        /// Constructor for the TokenService.
        /// </summary>
        /// <param name="tokenSettings">The token settings for JWT.</param>
        public TokenService(IOptions<TokenSettings> tokenSettings)
        {
            _tokenSettings = tokenSettings?.Value ?? throw new ArgumentNullException(nameof(tokenSettings));
        }

        /// <summary>
        /// Generate token
        /// </summary>
        /// <param name="user">The user for token generation</param>
        /// <returns>The generated token</returns>
        /// <exception cref="ArgumentNullException">Thrown when user or user.Username is null</exception>
        public string GenerateToken(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            if (string.IsNullOrEmpty(user.Username))
            {
                throw new ArgumentNullException(nameof(user.Username));
            }

            if (string.IsNullOrEmpty(_tokenSettings.Secret))
            {
                throw new ArgumentNullException(nameof(_tokenSettings.Secret));
            }

            var key = Encoding.ASCII.GetBytes(_tokenSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Sid, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Username)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var tokenHandler = new JsonWebTokenHandler();

            return tokenHandler.CreateToken(tokenDescriptor);
        }

        /// <summary>
        /// Validate token
        /// </summary>
        /// <param name="token">The token to validate</param>
        /// <returns>The user id if the token is valid, null otherwise</returns>
        public async Task<int?> ValidateToken(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                return null;
            }

            var tokenHandler = new JsonWebTokenHandler();
            var key = Encoding.ASCII.GetBytes(_tokenSettings.Secret);

            try
            {
                var tokenValidationResult = await tokenHandler.ValidateTokenAsync(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                });

                if (!tokenValidationResult.IsValid)
                {
                    return null;
                }

                var jwtToken = (JsonWebToken)tokenValidationResult.SecurityToken;
                var userIdClaim = jwtToken.Claims.First(claim => claim.Type == ClaimTypes.Sid);
                if (userIdClaim == null)
                {
                    return null;
                }

                return int.Parse(userIdClaim.Value);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }
    }
}
