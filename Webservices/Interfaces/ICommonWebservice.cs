using minimalAPI.Dtos;
namespace minimalAPI.Webservices.Interfaces
{
    public interface ICommonWebservice
    {
        public Task<Players> GetPlayers(HttpClient client);
        public Task<Player> GetPlayer(HttpClient client, string name);
        public Task<PlayerHighscore> GetPlayerPoints(HttpClient client, string id);
        public Task<HighScore> GetPlayersPoints(HttpClient client);
    }
}
