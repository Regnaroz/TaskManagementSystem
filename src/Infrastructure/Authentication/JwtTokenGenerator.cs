using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SampleProject.Application.Common.Interfaces;
using SampleProject.Domain.Constants;
using SampleProject.Domain.Entities;

namespace SampleProject.Infrastructure.Authentication;
public class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly IDateTimeProvider _dateTime;
    private readonly JwtSettings jwtSettings;
    public JwtTokenGenerator(IDateTimeProvider dateTime, IOptions<JwtSettings> jwtSettings)
    {
        _dateTime = dateTime;
        this.jwtSettings = jwtSettings.Value;
    }
    public string GenerateToken(ApplicationUser user)
    {
        var signingCredentials = new SigningCredentials(
                           new SymmetricSecurityKey(
                           Encoding.UTF8.GetBytes(jwtSettings.SecretKey)),//must be 16 at least
                           SecurityAlgorithms.HmacSha256);
        var claims = new[]
        {
                new Claim(JwtRegisteredClaimNames.Sub,user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.GivenName,user.FirstName),
                new Claim(JwtRegisteredClaimNames.FamilyName,user.LastName),
                new Claim(JwtRegisteredClaimNames.Jti,new Guid().ToString()),
                new Claim(ClaimTypes.Role,GetUserRole(user))
            };

        var securityToken = new JwtSecurityToken(
            issuer: jwtSettings.Issuer,
            expires: _dateTime.UtcNow.AddHours(jwtSettings.ExpiryInMinutes),
            audience: jwtSettings.Audience,
            claims: claims,
            signingCredentials: signingCredentials);

        return new JwtSecurityTokenHandler().WriteToken(securityToken);
    }

    private string GetUserRole(ApplicationUser user)
    {
       var role = user.ApplicationUserRoles.First();
       if(role != null)
        {
            return role.Role.Name!;
        }

      throw new ArgumentNullException(nameof(role));
    }
}
