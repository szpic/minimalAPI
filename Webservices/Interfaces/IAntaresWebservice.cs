using minimalAPI.Dtos;
namespace minimalAPI.Webservices.Interfaces
{
    public interface IAntaresWebservice
    {
        public Task<Players> GetPlayers();
        public Task<Player> GetPlayer(string name);
        public Task<PlayerHighscore> GetPlayerPoints(string id);
        public Task<HighScore> GetPlayersPoints();
    }
}
