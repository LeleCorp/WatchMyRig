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

namespace watchmyrig.Class
{
    class Wallet
    {
        private string adrWallet;
        private string adrPool;
        private string coin;
        private ApiClient apiClient;
        public StatsWallet stats;

        public Wallet (string _adrWallet, string _adrPool,string _coin)
        {
            adrPool = _adrPool;
            adrWallet = _adrWallet;
            coin = _coin;

            switch (adrPool)
            {
                case "https://api.ethermine.org" :
                    apiClient = new EtherMine(this);
                    break;
                case "https://api.nanopool.org":
                    apiClient = new Nanopool(this);
                    break;
            }

            
        }

        #region Getters
        public string getAddressWallet()
        {
            return adrWallet;
        }

        public string getCoin()
        {
            return coin;
        }

        public string getAddressPool()
        {
            return adrPool;
        }
        #endregion
    }
}