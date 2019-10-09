using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using System.Configuration;

namespace WeatherAPI.HTTP_management
{
    public class AppConfigReader
    {
        public string BaseUri = ConfigurationManager.AppSettings["base_uri"];
        public string Version = ConfigurationManager.AppSettings["version"];
        public string ApiKey = ConfigurationManager.AppSettings["apikey"];
        public string Weather = ConfigurationManager.AppSettings["weather"];
    }

    public class ApiCaller
    {
        public RestClient Client { get; set; }
        public string CitySelected { get; set; }
        //public string CountrySelected { get; set; }
        public AppConfigReader config;
        public ApiCaller(AppConfigReader config)
        {
            this.config = config;
            Client = new RestClient
            {
                BaseUrl = new Uri(config.BaseUri)
            };
        }

        public string GetWheatherReport(string city)
        {
            var request = new RestRequest();
            request.AddHeader("Content-Type", "application/json");
            CitySelected = city.ToLower().Replace(" ","");
            //CountrySelected = country.Replace(" ","");
            request.Resource = $"/{config.Version}/{config.Weather}q={CitySelected}&appid={config.ApiKey}";
            IRestResponse response = Client.Execute(request);
            return response.Content;
        }
        
    }
}
