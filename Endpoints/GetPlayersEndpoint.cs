using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using minimalAPI.Contracts.Requests;
using minimalAPI.Contracts.Responses;
using minimalAPI.Webservices.Interfaces;

namespace minimalAPI.Endpoints;

[HttpGet("player"), AllowAnonymous]
public class GetPlayersEndpoint : EndpointWithoutRequest<PlayersResponse>
{
    private readonly IAntaresWebservice antaresWebservice;

    public GetPlayersEndpoint(IAntaresWebservice antaresWebservice)
    {
        this.antaresWebservice = antaresWebservice;
    }
    public override async Task HandleAsync(CancellationToken ct)
    {
        var players = await antaresWebservice.GetPlayers();
        if(players is not null)
        {
            var playerPoints = await antaresWebservice.GetPlayersPoints();
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

