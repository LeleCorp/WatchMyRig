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
        public string GetAddressWallet()
        {
            return adrWallet;
        }

        public string GetCoin()
        {
            return coin;
        }

        public string GetAddressPool()
        {
            return adrPool;
        }
        #endregion
    }
}