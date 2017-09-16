using System;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;

namespace watchmyrig.Class
{
    abstract class ApiClient
    {
        protected Wallet wallet;

        protected ApiClient (Wallet _wallet)
        {
            wallet = _wallet;
        }

        abstract public StatsWallet GetStats();



        /// <summary>
        /// Retourne le résultat de l'API en JObject
        /// </summary>
        /// <param name="_url"> url de l'API </param>
        /// <returns> JObject </returns>
        protected static JObject GetResponse(string _url)
        {
            HttpWebRequest webRequest = LoadHtppRequest(_url);
            return ReadResponseAsJson(webRequest);
        }

        /// <summary>
        /// Retourne le résultat de l'API en objet voulu
        /// </summary>
        /// <typeparam name="T"> Type de retour </typeparam>
        /// <param name="_url"> url de l'API </param>
        /// <returns> Retourne reponse en objet voulu </returns>
        protected static T GetResponse<T>(string _url)
        {
            HttpWebRequest webRequest = LoadHtppRequest(_url);
            return ReadResponseAs<T>(webRequest);
        }

        #region Private method to load & read API

        // Charger la rq
        private static HttpWebRequest LoadHtppRequest(string _url)
        {
            HttpWebRequest rq = (HttpWebRequest) WebRequest.Create(_url);
            rq.Method = WebRequestMethods.Http.Get;
            return rq;
        }

        // Recevoir une reponse
        private static string ReadResponse(HttpWebRequest _rq)
        {
            using (HttpWebResponse response = _rq.GetResponse() as HttpWebResponse)
            using (var stream = response.GetResponseStream())
            using (var reader = new StreamReader(stream))
            {
                string responseContent = reader.ReadToEnd();
                return responseContent;
            }
        }

        // Recevoir reponse en JSON
        private static JObject ReadResponseAsJson(HttpWebRequest _rq)
        {
            string responseContent = ReadResponse(_rq);
            var jObject = JObject.Parse(responseContent);
            return jObject;
        }

        // Recevoir la reponse sous la forme de n'importe quel obj
        private static T ReadResponseAs<T>(HttpWebRequest _rq)
        {
            return ReadResponseAsJson(_rq).ToObject<T>();
        }

        #endregion
    }
}