
namespace BackEnd.ApiClients
{
    public class WorldOfTanksApi
    {
        private readonly string _applicationId;
        public HttpClient HttpClient { get; }
        public const string BaseUrl = "https://api.worldoftanks.eu/wot/";

        // HttpClient-et befogadó konstruktor a teszteléshez
        public WorldOfTanksApi(string applicationId, HttpClient httpClient = null)
        {
            _applicationId = applicationId;
            HttpClient = httpClient ?? new HttpClient();
        }

        public string ApplicationId => _applicationId;
    }
}
