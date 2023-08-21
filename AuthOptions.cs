using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace EmptyStack;

public class AuthOptions
{
    public const string ISSUER = "MyAuthServer"; // издатель токена
    public const string AUDIENCE = "http://localhost:3000/"; // потребитель токена
    private const string KEY = "mysupersecret_secretkey!123";

    public static SymmetricSecurityKey GetSymmetricSecurityKey()
    {
        return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
    }
}