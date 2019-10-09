# WeatherApiTest

# Motivation
WeatherApiTest is an acamedic project carried out for the purpose of practical learning of software testing at component level.

The aim of the project is to implement and test the API provided by openweathermap.org

# Build status
Status: Passed recent tests

Coverage: Unquantified

# Code style
Code style: Standard

# Technology and framework
Technology: C#, NUnit, Newtonsoft, JSon, Visual studio 2019

Framework: .Net framework, NUnit framework

# Features
Implementation of the API: The API was implemented in a .Net console application project

- API caller object to make api calls 

- Model objects to store api response data 

- Data manager object to transfer data from json to aforementioned model object

Testing of the API: The API testing was done in a .Net unit testing project

- Test methods mostly evaluating valid cases

# Code example
Test set up (done for city london): 

Class TestClass{

  DataManager manager;
  
  ApiCaller apiCaller;
  
  AppConfigReader config;
  
  public TestClass()
  {
  
      config = new AppConfigReader();
      
      apiCaller = new ApiCaller(config);
      
      manager = new DataManager();
      
      manager.DeserializeWeather(apiCaller.GetWheatherReport("london"));
      
  }

 Test using Assert:
 
 Assert.AreEqual("stations",manager.WeatherModel.main.@base);

# Installation
Clone the project in a local repository via git or simply download the project 

# API reference
api.openweathermap.org/data/2.5/weather?

# How to run the code 
- After installation, the project solution can be run in the visual studio

- Select Test from the tool bar -> select windows -> select test explorer

- Run the available tests from the text explorer windows

# How to use 
The project is to be used solely for the purpose of learning on how to carry out component testing of an API.

Existing implementation and testing can be used as a template for carrying out test on other APIs.

To add more tests:

- Add new test methods in the WeatherAPITest class

- Only use NUnit for the testing

