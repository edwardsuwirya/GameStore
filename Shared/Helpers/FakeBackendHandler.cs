using System.Net;
using System.Text;
using GameStore.Models;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace GameStore.Shared.Helpers;

public class FakeBackendHandler : HttpClientHandler
{
    private const string SecretKey = "h7de3j9y5k2l1m8n4p6q0r7s9t2u5v8w1x4z6a3b5c8d0f2g4i6j9k1l3m";
    private const string Issuer = "GameStore";
    private const string Audience = "GameStoreUsers";
    private static readonly TimeSpan TokenExpiration = TimeSpan.FromHours(1);

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        var method = request.Method;
        var path = request.RequestUri.AbsolutePath;

        return await handleRoute();

        async Task<HttpResponseMessage> handleRoute()
        {
            if (path == "/api/users/authenticate" && method == HttpMethod.Post)
                return await Authenticate();
            // if (path == "/users" && method == HttpMethod.Get)
            //     return await getUsers();
            // if (Regex.Match(path, @"\/users\/\d+$").Success && method == HttpMethod.Get)
            //     return await getUserById();
            // if (Regex.Match(path, @"\/users\/\d+$").Success && method == HttpMethod.Put)
            //     return await updateUser();
            // if (Regex.Match(path, @"\/users\/\d+$").Success && method == HttpMethod.Delete)
            //     return await deleteUser();

            // pass through any requests not handled above
            return await base.SendAsync(request, cancellationToken);
        }

        async Task<HttpResponseMessage> Authenticate()
        {
            await Task.Delay(1000, cancellationToken).ConfigureAwait(false);
            var bodyJson = await request.Content.ReadAsStringAsync();
            var body = Serialization.Deserialize<UserAccess>(bodyJson);

            if (!body.UserName.Equals("edo") || !body.Password.Equals("123456"))
                return await Error("Username or password is incorrect");

            var token = GenerateJwtToken(new Client

            {
                Id = 1,
                FirstName = "Edo",
                LastName = "S",
                Address = "123 Main Street",
                Email = "edo@gmail.com",
                Phone = "123456"
            });
            return await Ok(new { token });
        }

        async Task<HttpResponseMessage> Ok(object? body = null)
        {
            return await JsonResponse(HttpStatusCode.OK, body ?? new { });
        }

        async Task<HttpResponseMessage> Error(string message)
        {
            return await JsonResponse(HttpStatusCode.BadRequest, new { message });
        }

        async Task<HttpResponseMessage> JsonResponse(HttpStatusCode statusCode, object content)
        {
            var response = new HttpResponseMessage
            {
                StatusCode = statusCode,
                Content = new StringContent(Serialization.Serialize(content), Encoding.UTF8, "application/json")
            };

            await Task.Delay(500, cancellationToken);

            return response;
        }
    }
    private static string GenerateJwtToken(Client user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(SecretKey);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(
            [
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.GivenName, user.FirstName),
                new Claim(ClaimTypes.Surname, user.LastName.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.StreetAddress, user.Address),
                new Claim(ClaimTypes.MobilePhone, user.Phone)
            ]),
            Expires = DateTime.UtcNow.Add(TokenExpiration),
            Issuer = Issuer,
            Audience = Audience,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        var tokenString = tokenHandler.WriteToken(token);
        return tokenString;
    }
}