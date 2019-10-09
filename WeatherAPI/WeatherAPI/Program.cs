using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherAPI.HTTP_management;
using WeatherAPI.data_handling;

namespace WeatherAPI
{
    class Program
    {
        static void Main(string[] args)
        {
            AppConfigReader config = new AppConfigReader();
            ApiCaller apc = new ApiCaller(config);            
            DataManager manager = new DataManager();
            manager.DeserializeWeather(apc.GetWheatherReport("london"));
            Console.WriteLine(manager.WeatherModel.@base);
            Console.Read();
        }
    }
}
