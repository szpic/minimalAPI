using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using minimalAPI.Contracts.Requests;
using minimalAPI.Contracts.Responses;
using minimalAPI.Webservices.Interfaces;

namespace minimalAPI.Endpoints;

[HttpGet("/{universe}/player/{name}"), AllowAnonymous]
public class GetPlayerEndpoint : Endpoint<PlayerReq, PlayerResponse>
{
    private readonly ICommonWebservice _commonWebservice;

    public GetPlayerEndpoint(ICommonWebservice commonWebservice)
    {
        _commonWebservice = commonWebservice;
    }
    public override async Task HandleAsync(PlayerReq req, CancellationToken ct)
    {
        var player = await _commonWebservice.GetPlayer(req.universe, req.name);
        if (player == null)
        {
            await SendNotFoundAsync();
        }
        else
        {
            var points = await _commonWebservice.GetPlayerPoints(req.universe, player.Id);
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
}

