using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherAPI.HTTP_management;
using WeatherAPI.Models;
using Newtonsoft.Json;

namespace WeatherAPI.data_handling
{
    public class DataManager
    {
        public WeatherModel WeatherModel { get; set; }

        public void DeserializeWeather(string apiResponseString)
        {
            WeatherModel = JsonConvert.DeserializeObject<WeatherModel>(apiResponseString);
        }
    }
}
