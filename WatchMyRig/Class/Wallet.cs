namespace watchmyrig.Class
{
    class Wallet
    {
        private string adrWallet;
        private string adrPool;
        private string pool;
        private string coin;
        private ApiClient apiClient;
        public StatsWallet stats;

        public Wallet (string _adrWallet, string _pool,string _coin)
        {
            adrWallet = _adrWallet;
            coin = _coin;
            pool = _pool;

            switch (pool)
            {
                case "Ethermine" :
                    adrPool = "https://api.ethermine.org";
                    apiClient = new EtherMine(this);
                    break;
                case "Nanopool":
                    adrPool = "https://api.nanopool.org";
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

        public string GetAdrPool()
        {
            return adrPool;
        }

        public string GetPool()
        {
            return pool;
        }
        #endregion
    }
}