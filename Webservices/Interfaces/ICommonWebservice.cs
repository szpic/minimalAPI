using minimalAPI.Dtos;
namespace minimalAPI.Webservices.Interfaces
{
    public interface ICommonWebservice
    {
        public Task<Players> GetPlayers(string universe);
        public Task<Player> GetPlayer(string universe, string name);
        public Task<PlayerHighscore> GetPlayerPoints(string universe, string id);
        public Task<HighScore> GetPlayersPoints(string universe);
    }
}
