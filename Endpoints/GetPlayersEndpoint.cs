using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using minimalAPI.Contracts.Requests;
using minimalAPI.Contracts.Responses;
using minimalAPI.Webservices.Interfaces;

namespace minimalAPI.Endpoints;

[HttpGet("/{universe}/players"), AllowAnonymous]
public class GetPlayersEndpoint : Endpoint<UniverseReq,PlayersResponse>
{
    private readonly ICommonWebservice _commonWebservice;

    public GetPlayersEndpoint(ICommonWebservice commonWebservice)
    {
        _commonWebservice = commonWebservice;
    }
    public override async Task HandleAsync(UniverseReq req, CancellationToken ct)
    {
        var players = await _commonWebservice.GetPlayers(req.universe);
        if(players is not null)
        {
            var playerPoints = await _commonWebservice.GetPlayersPoints(req.universe);
            if(players != null)
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
}

