using System;
using NUnit.Framework;
using WeatherAPI.data_handling;
using WeatherAPI.HTTP_management;
using System.Collections.Generic;

namespace WeatherAPI.Test
{
    [TestFixture]
    public class WeatherAPITest
    {
        DataManager manager;
        ApiCaller apiCaller;
        AppConfigReader config;

        static List<int> WeatherIdArray = new List<int>(){ 200, 201, 202, 210, 211, 212, 221, 230,
        231, 232, 300, 301, 302, 310, 311, 312, 313, 314, 321, 500, 501,
        502, 503, 504, 511, 520, 521, 522, 531, 600, 601, 602, 611, 612, 613, 615,
        616, 620, 621, 622, 701, 711, 721, 731, 741, 751, 761, 762, 771, 781,
        800, 801, 802, 803, 804};

        static List<string> WeatherMainArray = new List<string>()
        {
            "Thunderstorm", "Drizzle", "Rain", "Snow", "Mist", "Smoke", "Haze", "Dust", "Fog",
            "Sand", "Dust", "Ash", "Squall", "Tornado", "Clear", "Clouds"
        };

        static List<string> WeatherIconArray = new List<string>()
        {
            "01d", "02d", "03d", "04d", "09d", "10d", "11d", "13d", "50d", "01n", "02n", "03n",
            "04n", "09n", "10n", "11n", "13n", "50n"
        };

        public WeatherAPITest()
        {
            config = new AppConfigReader();
            apiCaller = new ApiCaller(config);
            manager = new DataManager();
            manager.DeserializeWeather(apiCaller.GetWheatherReport("london"));
        }

        /**
         * Assert coordinates are not null
         */
        [Test]
        public void CoordinateNotNull()
        {
            Assert.IsNotNull(manager.WeatherModel.coord.lon);
            Assert.IsNotNull(manager.WeatherModel.coord.lat);
        }

        /**
         * Assert that the returned coordinates are of type double
         */
        [Test]
        public void ValidCoordinateType()
        {
            Assert.IsInstanceOf<double>(manager.WeatherModel.coord.lon);
            Assert.IsInstanceOf<double>(manager.WeatherModel.coord.lat);
        }

        /**
         * Assert correct coordinates are returned
         */
        [Test]
        public void CorrectCoordinate()
        {
            Assert.AreEqual(-0.13, manager.WeatherModel.coord.lon);
            Assert.AreEqual(51.51, manager.WeatherModel.coord.lat);
        }

        /**
         * Assert that the weather id is not null
         */
        [Test]
        public void WeatherIdNotNull()
        {
            Assert.IsNotNull(manager.WeatherModel.weather[0].id);
        }

        /**
         * Assert that the weather id returned is valid
         */
        [Test]
        public void ValdWeatherId()
        {
            bool isValid = false;
            if (WeatherIdArray.Contains(manager.WeatherModel.weather[0].id))
            {
                isValid = true;
            }
            Assert.IsTrue(isValid);
        }

        /**
         * Assert weather main is not null
         */
        [Test]
        public void WeatherMainNotNull()
        {
            Assert.IsNotNull(manager.WeatherModel.weather[0].main);
        }

        /**
         * Assert that the weather main is valid 
         */
        [Test]
        public void ValidWeatherMain()
        {
            bool isValid = false;
            if (WeatherMainArray.Contains(manager.WeatherModel.weather[0].main))
            {
                isValid = true;
            }
            Assert.IsTrue(isValid);
        }

        /**
         * Assert that the weather description is not null 
         */
        [Test]
        public void WeatherDescriptionNotNull()
        {
            Assert.IsNotNull(manager.WeatherModel.weather[0].description);
        }

        /**
         * Assert that the weather description value is string
         */
        [Test]
        public void WeatherDescriptionType()
        {
            Assert.IsInstanceOf<string>(manager.WeatherModel.weather[0].description);
        }

        /**
         * Assert that the weather icon is not null
         */
        [Test]
        public void WeatherIconNotNull()
        {
            Assert.IsNotNull(manager.WeatherModel.weather[0].icon);
        }

        [Test]
        public void ValidWeatherIcon()
        {
            bool isValid = false;
            if (WeatherIconArray.Contains(manager.WeatherModel.weather[0].icon))
            {
                isValid = true;
            }
            Assert.IsTrue(isValid);
        }

        /**
         * Assert base is not null
         */
        [Test]
        public void BaseNotNull()
        {
            Assert.IsNotNull(manager.WeatherModel.@base);
        }

        /**
         * Assert temperature is of type double
         */
        [Test]
        public void ValidTempType()
        {
            Assert.IsInstanceOf<double>(manager.WeatherModel.main.temp);
        }

        /**
         * Assert temperature is over 273.15 Kelvin
         */
        [Test]
        public void ValidTemp()
        {
            Assert.GreaterOrEqual(manager.WeatherModel.main.temp, 273.15);
        }
        
        /**
         * Assert that pressure is positive
         */
        [Test]
        public void PositivePressure()
        {
            Assert.GreaterOrEqual(manager.WeatherModel.main.pressure, 0);
        }

        /**
        * Assert humidity is between 0 - 100% inclusive
        */
        [Test]
        public void ValidHumidity()
        {
            Assert.GreaterOrEqual(manager.WeatherModel.main.humidity, 0);
            Assert.LessOrEqual(manager.WeatherModel.main.humidity, 100);
        }

        /**
        * Assert minimum temperature is of type double
        */
        [Test]
        public void ValidMinTempType()
        {
            Assert.IsInstanceOf<double>(manager.WeatherModel.main.temp_min);
        }

        /**
         * Assert the minimum temperature is above 273.15 kelvin
         */
        [Test]
        public void ValidMinTemp()
        {
            Assert.Greater(manager.WeatherModel.main.temp_min, 273.15);
        }

        /**
       * Assert maximum temperature is of type double
       */
        [Test]
        public void ValidMaxTempType()
        {
            Assert.IsInstanceOf<double>(manager.WeatherModel.main.temp_max);
        }

        /**
        * Assert the minimum temperature is above 273.15 kelvin
        */
        [Test]
        public void ValidMaxTemp()
        {
            Assert.Greater(manager.WeatherModel.main.temp_max, 273.15);
        }

        /**
         * Assert that the current returned temperature is within the range of max and min temperature returned 
         */
        public void TempBetweenMaxAndMin()
        {
            Assert.GreaterOrEqual(manager.WeatherModel.main.temp, manager.WeatherModel.main.temp_min);
            Assert.LessOrEqual(manager.WeatherModel.main.temp, manager.WeatherModel.main.temp_max);
        }

        [Test]
        public void CorrectVisibility()
        {
            Assert.AreEqual(10000, manager.WeatherModel.visibility);
        }

        /**
         * Assert the wind speed is always positive value
         */
        [Test]
        public void PositiveWindSpeed()
        {
            Assert.GreaterOrEqual(manager.WeatherModel.wind.speed,0);
        }

        /**
         * Assert the wind degree is between 0 and 360 degrees
         */
        [Test]
        public void ValidWindDegree()
        {
            Assert.GreaterOrEqual(manager.WeatherModel.wind.deg, 0);
            Assert.LessOrEqual(manager.WeatherModel.wind.deg, 360);
        }

        /**
         * Assert cloud percentage is between 0 - 100% inclusive
         */
        [Test]
        public void ValidCloudsPercentage()
        {
            Assert.GreaterOrEqual(manager.WeatherModel.clouds.all, 0);
            Assert.LessOrEqual(manager.WeatherModel.clouds.all, 100);
        }

        /**
         * Assert that the current day time is within between the previous and next subsequent day
         */
        [Test]
        public void ValidDt()
        {
            int year = DateTime.Now.Year - 1970;
            int month = DateTime.Now.Month - 1;
            int day = DateTime.Now.Day - 2;
            int today = year * 31556926 + month * 2629743 + day * 86400;
            Assert.GreaterOrEqual(manager.WeatherModel.dt, today);
            int nextDay = today + 86400; 
            Assert.LessOrEqual(manager.WeatherModel.dt, nextDay);
        }

        [Test]
        public void CorrectSysType()
        {
            Assert.AreEqual(1, manager.WeatherModel.sys.type);
        }

        /**
         * Assert sys id is not null
         */
        [Test]
        public void CorrectSysId()
        {
            Assert.IsNotNull(manager.WeatherModel.sys.id);
        }

        /**
         * Assert sys message is not null
         */
        [Test]
        public void CorrectSysMessageNotNull()
        {
            Assert.IsNotNull(manager.WeatherModel.sys.message);
        }

        /**
         * Assert that the sys message is a double
         */
        [Test]
        public void ValidSysMessageType()
        {
            Assert.IsInstanceOf<double>(manager.WeatherModel.sys.message);
        }

        /**
         * Assert that the returned country value is a string
         */
        [Test]
        public void ValidCountryType()
        {
            Assert.IsInstanceOf<string>(manager.WeatherModel.sys.country);
        }

        /**
         * Assert correct country is returned
         */
        [Test]
        public void CorrectCountry()
        {
            Assert.AreEqual("GB", manager.WeatherModel.sys.country);
        }
    

        /**
         * Assert the sunrise time is after 4am today
         */
        [Test]
        public void ValidSunrise()
        {
            int year = DateTime.Now.Year - 1970;
            int month = DateTime.Now.Month - 1;
            int day = DateTime.Now.Day - 2;
            int todayFourAm = year * 31556926 + month * 2629743 + day * 86400 + 4*3600;
            Assert.GreaterOrEqual(manager.WeatherModel.sys.sunrise, todayFourAm);
        }

        /**
         * Assert the sunset time is after 4pm today
         */
        [Test]
        public void ValidSunset()
        {
            int year = DateTime.Now.Year - 1970;
            int month = DateTime.Now.Month - 1;
            int day = DateTime.Now.Day - 2;
            int todayFourPm = year * 31556926 + month * 2629743 + day * 86400 + 16 * 3600;
            Assert.GreaterOrEqual(manager.WeatherModel.sys.sunset, todayFourPm);
        }

        /**
         * Assert valid time zone is returned
         */
        [Test]
        public void ValidTimezone()
        {
            Assert.LessOrEqual(manager.WeatherModel.timezone, 50400);
            Assert.GreaterOrEqual(manager.WeatherModel.timezone, -46800);
        }

        /**
         * Assert correct time zone is returned
         */
        [Test]
        public void CorrectTimezone()
        {
            Assert.AreEqual(3600, manager.WeatherModel.timezone);
        }

        /**
         * Assert that data id is not null
         */
        [Test]
        public void CityIdNotNull()
        {
            Assert.IsNotNull(manager.WeatherModel.id);
        }

        /**
         * Assert that city id is always positive
         */
        [Test]
        public void ValidCityId()
        {
            Assert.GreaterOrEqual(manager.WeatherModel.id, 0);
        }

        /**
         * Assert that city id is correct
         */
        [Test]
        public void CorrectCityId()
        {
            Assert.AreEqual(2643743, manager.WeatherModel.id);
        }

        /**
         * Assert that the returned city name is a string
         */
        [Test]
        public void ValidCityNameType()
        {
            Assert.IsInstanceOf<string>(manager.WeatherModel.name);
        }

        /**
         * Assert that the returned city name is correct
         */
        [Test]
        public void CorrectCityName()
        {
            Assert.AreEqual("London", manager.WeatherModel.name);
        }

        /**
         * 
         */
        [Test]
        public void CorrectCod()
        {
            Assert.AreEqual(200, manager.WeatherModel.cod);
        }
    }
}
