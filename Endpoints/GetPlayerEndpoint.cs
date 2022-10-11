using FastEndpoints;
using LazyCache;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using minimalAPI.Contracts.Requests;
using minimalAPI.Contracts.Responses;
using minimalAPI.Dtos;
using minimalAPI.Webservices.Interfaces;

namespace minimalAPI.Endpoints;

[HttpGet("/{universe}/player/{name}"), AllowAnonymous]
public class GetPlayerEndpoint : Endpoint<PlayerReq, PlayerResponse>
{
    private readonly ICommonWebservice _commonWebservice;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IAppCache _appCache;

    public GetPlayerEndpoint(ICommonWebservice commonWebservice, IHttpClientFactory httpClientFactory, IAppCache appCache)
    {
        _commonWebservice = commonWebservice;
        _httpClientFactory = httpClientFactory;
        _appCache = appCache;
    }
    public override async Task HandleAsync(PlayerReq req, CancellationToken ct)
    {
        var client = _httpClientFactory.CreateClient(req.universe);
        if(client.BaseAddress== null)
        {
            AddError(req => req.universe, "Asked universe is not defined");
        }
        //if not defined universe return with error;
        ThrowIfAnyErrors();
        Func<Task<Dtos.Player>> playerFactory = () => GetPlayer(client, req.name);
        var player = await _appCache.GetOrAddAsync($"{req.universe}_player_{req.name}", playerFactory, DateTimeOffset.Now.AddHours(1));
        if (player == null)
        {
            await SendNotFoundAsync();
        }
        else
        {
            Func < Task < Dtos.PlayerHighscore >> playerHighScoreFactory = () => GetPlayerPoints(client, player.Id);
            var points = await _appCache.GetOrAddAsync($"{req.universe}_player{player.Id}_points", playerHighScoreFactory, DateTimeOffset.Now.AddHours(1));
            if (points == null)
            {
                await SendNotFoundAsync();
            }
            else
            {
                await SendAsync(new PlayerResponse
                {
                    name = player.Name,
                    points = points.Score
                });
            }
        }
    }
    protected async Task<Dtos.Player> GetPlayer(HttpClient client, string name)
    {
        return await _commonWebservice.GetPlayer(client, name);
    }
    protected async Task<Dtos.PlayerHighscore> GetPlayerPoints(HttpClient client, string id)
    {
        return await _commonWebservice.GetPlayerPoints(client, id);
    }
}

