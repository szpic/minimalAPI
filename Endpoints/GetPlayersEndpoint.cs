using FastEndpoints;
using LazyCache;
using Microsoft.AspNetCore.Authorization;
using minimalAPI.Contracts.Requests;
using minimalAPI.Contracts.Responses;
using minimalAPI.Webservices.Interfaces;
using System.Net.Http;

namespace minimalAPI.Endpoints;

[HttpGet("/{universe}/players"), AllowAnonymous]
public class GetPlayersEndpoint : Endpoint<UniverseReq, PlayersResponse>
{
    private readonly ICommonWebservice _commonWebservice;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IAppCache _appCache;
    public GetPlayersEndpoint(ICommonWebservice commonWebservice, IHttpClientFactory httpClientFactory, IAppCache appCache)
    {
        _commonWebservice = commonWebservice;
        _httpClientFactory = httpClientFactory;
        _appCache = appCache;
    }
    public override async Task HandleAsync(UniverseReq req, CancellationToken ct)
    {
        var client = _httpClientFactory.CreateClient(req.universe);
        if (client.BaseAddress == null)
        {
            AddError(req => req.universe, "Asked universe is not defined");
        }
        //if not defined universe return with error;
        ThrowIfAnyErrors();
        Func<Task<Dtos.Players>> playersFactory = () => GetPlayers(client);
        var players = await _appCache.GetOrAddAsync($"{req.universe}_players", playersFactory, DateTimeOffset.Now.AddHours(1));
        if (players is not null)
        {
            Func<Task<Dtos.HighScore>> highscoreFactory  = () => GetPlayersPoints(client);
            var playerPoints = await _appCache.GetOrAddAsync($"{req.universe}_highscore", highscoreFactory, DateTimeOffset.Now.AddHours(1));
            if (players != null)
            {
                PlayersResponse playersResponse = new()
                {
                    Players = new()
                };
                foreach (var player in players.Player)
                {
                    var points = playerPoints.Player.Where(w => w.Id == player.Id).FirstOrDefault();
                    if (points != null)
                    {
                        playersResponse.Players.Add(new PlayerResponse { name = player.Name, points = points.Score });
                    }
                }
                await SendAsync(playersResponse);
            }
            else
            {
                await SendNotFoundAsync();
            }
        }
        else
        {
            await SendNotFoundAsync();
        }
    }

    protected async Task<Dtos.Players> GetPlayers(HttpClient client)
    {
        return await _commonWebservice.GetPlayers(client);
    }

    protected async Task<Dtos.HighScore> GetPlayersPoints(HttpClient client)
    {
        return await _commonWebservice.GetPlayersPoints(client);
    }
}

