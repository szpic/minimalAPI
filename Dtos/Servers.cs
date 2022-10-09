namespace minimalAPI.Dtos
{
    public class Servers
    {
        public List<Server> servers { get; set; }
    }

    public class Server
    {
        public string name { get; set; }
        public string url { get; set; }
    }
}