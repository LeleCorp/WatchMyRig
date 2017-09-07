using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json.Linq;

namespace watchmyrig.Class
{
    class Nanopool : ApiClient
    {
        private string url;

        public Nanopool(Wallet _wallet) : base(_wallet)
        {
            //url = "https://api.ethermine.org/miner//history";
            url = wallet.getAddressPool() + "/v1/" + wallet.getCoin();
            wallet.stats = GetStats();
        }

        public override StatsWallet GetStats()
        {
            StatsWallet stats = new StatsWallet();

            url += "/user/" + wallet.getAddressWallet();
            JObject response = GetResponse(url);
            var lastData = response["data"];

            // float reportedHashrate = lastData["reportedHashrate"].Value<float>();
            float currentHashrate = lastData["hashrate"].Value<float>();
            float averageHashrate = lastData["avgHashrate"]["h24"].Value<float>();

            // stats.SetReportedHasrate(reportedHashrate);
            stats.SetCurrentHasrate(currentHashrate);
            stats.SetAverageHasrate(averageHashrate);

            return stats;
        }
    }
}