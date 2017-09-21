using Newtonsoft.Json.Linq;

namespace watchmyrig.Class
{
    class EtherMine : ApiClient
    {
        private string url;

        public EtherMine(Wallet _wallet) : base(_wallet)
        {
            url = wallet.GetAdrPool();
            wallet.stats = GetStats();
        }

        public override StatsWallet GetStats()
        {
            StatsWallet stats = new StatsWallet();

            url += "/miner/" + wallet.GetAddressWallet() + "/history";
            JObject response = GetResponse(url);
            var lastData = response["data"].Last;

            // float reportedHashrate = lastData["reportedHashrate"].Value<float>();
            float currentHashrate = lastData["currentHashrate"].Value<float>();
            float averageHashrate = lastData["averageHashrate"].Value<float>();

            // stats.SetReportedHasrate(reportedHashrate);
            stats.SetCurrentHasrate(currentHashrate);
            stats.SetAverageHasrate(averageHashrate);

            return stats;
        }
    }
}