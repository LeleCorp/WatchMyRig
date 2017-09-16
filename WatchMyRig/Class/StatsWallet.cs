namespace watchmyrig.Class
{
    class StatsWallet
    {
        private float reportedHashrate;
        private float currentHashrate;
        private float averageHashrate;

        public StatsWallet() { }

        #region Getters

        public float GetReportedHashrate()
        {
            return reportedHashrate;
        }

        public float GetCurrentHashrate()
        {
            return currentHashrate;
        }

        public float GetAverageHashrate()
        {
            return averageHashrate;
        }

        #endregion
        #region Setters

        public void SetReportedHasrate(float _reportedHashrate)
        {
            reportedHashrate = _reportedHashrate;
        }

        public void SetCurrentHasrate(float _currentHashrate)
        {
            currentHashrate = _currentHashrate;
        }

        public void SetAverageHasrate(float _averageHashrate)
        {
            averageHashrate = _averageHashrate;
        }

        #endregion
    }
}