using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using EmptyStack.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace EmptyStack.Endpoints;

public static class AuthEndpoint
{
    public static void MapAuth(this RouteGroupBuilder app)
    {
        app.MapPost("/login", async (User loginData, EmptyStackDb db) =>
        {
            var user = await db.users.FirstOrDefaultAsync(u =>
                u.username == loginData.username && u.password == loginData.password);
            if (user is null) return Results.Unauthorized();

            var claims = new List<Claim> { new(ClaimTypes.Name, user.username) };

            var jwt = new JwtSecurityToken(
                AuthOptions.ISSUER,
                AuthOptions.AUDIENCE,
                claims,
                expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(10)),
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(),
                    SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = encodedJwt,
                user.username
            };

            return Results.Json(response);
        });
    }
}