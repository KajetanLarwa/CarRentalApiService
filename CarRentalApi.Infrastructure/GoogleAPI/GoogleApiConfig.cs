namespace CarRentalApi.Infrastructure
{
    public class GoogleApiConfig
    {
        public string ApiKey { get; set; }

        public GoogleApiConfig(string key)
        {
            ApiKey = key;
        }
    }
}