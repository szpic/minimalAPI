using minimalAPI.Webservices.Interfaces;
using minimalAPI.Dtos;
using Newtonsoft.Json;
using System.Xml.Serialization;
using minimalAPI.Webservices.Extensions;

namespace minimalAPI.Webservices;

public class CommonWebservice :ICommonWebservice
{       
    public async Task<Players> GetPlayers(HttpClient client)
    {
        var list = await client.GetAsAsync<Players>("players.xml");
        return list;
    }

    public async Task<Player> GetPlayer(HttpClient client, string name)
    {
        var list = await client.GetAsAsync<Players>("players.xml");
        var player = list.Player.Where(w => w.Name.ToLower() == name).FirstOrDefault();
        return player;
    }

    public async Task<PlayerHighscore> GetPlayerPoints(HttpClient client, string id)
    {
        //type = 0 is total points
        var data = await client.GetAsAsync<HighScore>("highscore.xml?category=1&type=0");
        var player =  data.Player.Where(x => x.Id == id).FirstOrDefault();

        return player;
    }

    public async Task<HighScore> GetPlayersPoints(HttpClient client)
    {
        var data = await client.GetAsAsync<HighScore>("highscore.xml?category=1&type=0");

        return data;
    }
}

