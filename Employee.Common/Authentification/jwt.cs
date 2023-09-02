using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Common;

public static class jwt
{
    public  static string GenerateToken(string Name,string mobile ,string key, String Issuer,string Audience,string role)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claimList = new[]
        {
            new Claim(ClaimTypes.NameIdentifier,Name),
            new Claim(ClaimTypes.MobilePhone,mobile),
            new Claim(ClaimTypes.Role,role)

        };

        var token = new JwtSecurityToken(Issuer,
          Audience,
          claimList,
          expires: DateTime.Now.AddMinutes(15),
          signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
