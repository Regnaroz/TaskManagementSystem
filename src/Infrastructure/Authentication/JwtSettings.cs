using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleProject.Infrastructure.Authentication;
public class JwtSettings
{
    public const string jwtSettings = nameof(jwtSettings);
    public string SecretKey { get; init; } = null!;
    public int ExpiryInMinutes { get; init; }
    public string Issuer { get; init; } = null!;
    public string Audience { get; init; } = null!;
}
