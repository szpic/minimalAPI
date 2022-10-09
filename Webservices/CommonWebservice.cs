using minimalAPI.Webservices.Interfaces;
using minimalAPI.Dtos;
using Newtonsoft.Json;
using System.Xml.Serialization;
using minimalAPI.Webservices.Extensions;

namespace minimalAPI.Webservices;

public class CommonWebservice :ICommonWebservice
{
    private IHttpClientFactory httpClientFactory { get; set; }

    public CommonWebservice(IHttpClientFactory httpClientFactory)
    {
       this.httpClientFactory = httpClientFactory;
    }

    public async Task<Players> GetPlayers(string universe)
    {
        var client = httpClientFactory.CreateClient(universe);
        var list = await client.GetAsAsync<Players>("players.xml");
        return list;
    }

    public async Task<Player> GetPlayer(string universe, string name)
    {
        var client = httpClientFactory.CreateClient(universe);


        var list = await client.GetAsAsync<Players>("players.xml");
        var player = list.Player.Where(w => w.Name.ToLower() == name).FirstOrDefault();
        return player;
    }

    public async Task<PlayerHighscore> GetPlayerPoints(string universe, string id)
    {
        var client = httpClientFactory.CreateClient(universe);
        //type = 0 is total points
        var data = await client.GetAsAsync<HighScore>("highscore.xml?category=1&type=0");
        var player =  data.Player.Where(x => x.Id == id).FirstOrDefault();

        return player;
    }

    public async Task<HighScore> GetPlayersPoints(string universe)
    {
        var client = httpClientFactory.CreateClient(universe);
        var data = await client.GetAsAsync<HighScore>("highscore.xml?category=1&type=0");

        return data;
    }
}

