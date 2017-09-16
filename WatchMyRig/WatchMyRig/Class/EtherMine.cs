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
using System.Net;

namespace watchmyrig.Class
{
    class EtherMine : ApiClient
    {
        private string url;

        public EtherMine(Wallet _wallet) : base(_wallet)
        {
            //url = "https://api.ethermine.org/miner//history";
            url = wallet.getAddressPool();
            wallet.stats = GetStats();
        }

        public override StatsWallet GetStats()
        {
            StatsWallet stats = new StatsWallet();

            url += "/miner/" + wallet.getAddressWallet() + "/history";
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