using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using minimalAPI.Contracts.Requests;
using minimalAPI.Contracts.Responses;
using minimalAPI.Webservices.Interfaces;

namespace minimalAPI.Endpoints;

[HttpGet("player/{name}"), AllowAnonymous]
public class GetPlayerEndpoint : Endpoint<PlayerReq, PlayerResponse>
{
    private readonly IAntaresWebservice antaresWebservice;

    public GetPlayerEndpoint(IAntaresWebservice antaresWebservice)
    {
        this.antaresWebservice = antaresWebservice;
    }
    public override async Task HandleAsync(PlayerReq req, CancellationToken ct)
    {
        var player = await antaresWebservice.GetPlayer(req.name);
        if(player == null)
        {
            await SendNotFoundAsync();
        }
        else
        {
            var points = await antaresWebservice.GetPlayerPoints(player.Id);
            if(points == null)
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
}

