using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using GameStore.Models;
using GameStore.Shared.Errors;
using GameStore.Shared.Errors.Auth;
using GameStore.Shared.Errors.Game;
using GameStore.Shared.Responses;

namespace GameStore.Shared.Helpers;

public class FakeBackendHandler : HttpClientHandler
{
    List<Game> games =
    [
        new()
        {
            Id = 1,
            Name = "Street Fighter II",
            Genre = "Fighting",
            Price = 19.99M,
            ReleaseDate = new DateTime(1991, 1, 23)
        },

        new()
        {
            Id = 2,
            Name = "Final Fantasy XIV",
            Genre = "RolePlaying",
            Price = 59.99M,
            ReleaseDate = new DateTime(2010, 9, 30)
        },

        new()
        {
            Id = 3,
            Name = "FIFA 23",
            Genre = "Sports",
            Price = 69.99M,
            ReleaseDate = new DateTime(2022, 9, 2)
        }
    ];

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        var method = request.Method;
        var path = request.RequestUri.AbsolutePath;

        return await handleRoute();

        async Task<HttpResponseMessage> handleRoute()
        {
            if (path == "/api/v1/users/authenticate" && method == HttpMethod.Post)
                return await Authenticate();
            if (path == "/api/v1/games" && method == HttpMethod.Get)
                return await GetGames();
            if (path == "/api/v1/games" && method == HttpMethod.Post)
                return await AddGame();
            if (path == "/api/v1/clients" && method == HttpMethod.Get)
                return await GetClients();
            if (Regex.Match(path, @"\/api/v1/games\/\d+$").Success && method == HttpMethod.Get)
                return await GetGameById();
            if (Regex.Match(path, @"\/api/v1/games\/\d+$").Success && method == HttpMethod.Put)
                return await UpdateGame();
            if (Regex.Match(path, @"\/api/v1/games\/\d+$").Success && method == HttpMethod.Delete)
                return await DeleteGame();

            return await Error(HttpStatusCode.NotFound,
                ResponseWrapper<string>.Fail(AppError.PathError()));
        }

        async Task<HttpResponseMessage> Authenticate()
        {
            await Task.Delay(1000, cancellationToken).ConfigureAwait(false);
            var bodyJson = await request.Content.ReadAsStringAsync();
            var body = Serialization.Deserialize<UserAccess>(bodyJson);

            if (!body.UserName.Equals("edo") || !body.Password.Equals("123456"))
                return await Error(HttpStatusCode.Unauthorized,
                    ResponseWrapper<Client>.Fail(AuthErrors.UnauthorizedUser()));

            var client = new Client
            {
                Id = 1,
                FirstName = "Edo",
                LastName = "",
                Address = "123 Main Street",
                Email = "edo@gmail.com",
                Phone = "123456"
            };
            return await Ok(ResponseWrapper<Client>.Success(client));
        }

        async Task<HttpResponseMessage> GetGames()
        {
            await Task.Delay(1000, cancellationToken).ConfigureAwait(false);
            return await Ok(ResponseWrapper<Game[]>.Success(games.ToArray()));
        }

        async Task<HttpResponseMessage> GetGameById()
        {
            await Task.Delay(1000, cancellationToken).ConfigureAwait(false);
            var id = int.Parse(path.Split('/').Last());
            var game = games.FirstOrDefault(g => g.Id == id);
            if (game == null)
            {
                return await Error(HttpStatusCode.NotFound,
                    ResponseWrapper<Game>.Fail(GameErrors.GameNotFound()));
            }

            return await Ok(ResponseWrapper<Game>.Success(game));
        }

        async Task<HttpResponseMessage> AddGame()
        {
            await Task.Delay(1000, cancellationToken).ConfigureAwait(false);
            var bodyJson = await request.Content.ReadAsStringAsync();
            var body = Serialization.Deserialize<Game>(bodyJson);
            body.Id = games.Max(g => g.Id) + 1;
            games.Add(body);

            return await Ok(ResponseWrapper<Game>.Success(body));
        }

        async Task<HttpResponseMessage> UpdateGame()
        {
            await Task.Delay(1000, cancellationToken).ConfigureAwait(false);
            var id = int.Parse(path.Split('/').Last());
            var game = games.FirstOrDefault(g => g.Id == id);
            if (game == null)
            {
                return await Error(HttpStatusCode.NotFound,
                    ResponseWrapper<Game>.Fail(GameErrors.GameNotFound()));
            }

            var bodyJson = await request.Content.ReadAsStringAsync();
            var body = Serialization.Deserialize<Game>(bodyJson);
            game.Name = body.Name;
            game.Genre = body.Genre;
            game.Price = body.Price;
            game.ReleaseDate = body.ReleaseDate;
            return await Ok(ResponseWrapper<Game>.Success(game));
            // return await Error(HttpStatusCode.InternalServerError,
            // ResponseWrapper<Game>.Fail(AppError.GeneralError("Insufficient Storage")));
        }

        async Task<HttpResponseMessage> DeleteGame()
        {
            await Task.Delay(1000, cancellationToken).ConfigureAwait(false);
            var id = int.Parse(path.Split('/').Last());
            var game = games.FirstOrDefault(g => g.Id == id);
            if (game == null)
            {
                return await Error(HttpStatusCode.NotFound,
                    ResponseWrapper<int>.Fail(GameErrors.GameNotFound()));
            }

            games.Remove(game);
            return await Ok(ResponseWrapper<int>.Success(game.Id));
        }

        async Task<HttpResponseMessage> GetClients()
        {
            await Task.Delay(1000, cancellationToken).ConfigureAwait(false);
            List<Client> clients = [];
            return await Ok(ResponseWrapper<List<Client>>.Success(clients));
        }

        async Task<HttpResponseMessage> Ok(object? body = null)
        {
            return await JsonResponse(HttpStatusCode.OK, body ?? new { });
        }

        async Task<HttpResponseMessage> Error(HttpStatusCode statusCode, object? body = null)
        {
            return await JsonResponse(statusCode, body ?? new { });
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