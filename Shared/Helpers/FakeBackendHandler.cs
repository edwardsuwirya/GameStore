using System.Net;
using System.Text;
using GameStore.Models;

namespace GameStore.Shared.Helpers;

public class FakeBackendHandler : HttpClientHandler
{
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

            return await Ok(new Client
            {
                Id = 1,
                FirstName = "Edo",
                LastName = "",
                Address = "123 Main Street",
                Email = "edo@gmail.com",
                Phone = "123456"
            });
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
}