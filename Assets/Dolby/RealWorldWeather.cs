using System;
using System.Collections;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.Networking;
using System.Net;

//THIS IS ESSENTIALLY THE SUBJECT (CONTAINS OBSERVERS)


public class RealWorldWeather : MonoBehaviour
{
	/// <summary>
	/// We communicate with two API's in order to obtain
	/// 1. The user's current location in longitude and latitude
	/// 2. The weather report from the user's current location
	/// 
	/// We then store this information into our variables, which can be 
	/// accessed by other files through our static object "current"
	/// </summary>

	//Variable for the first API, stores our lon and lat
	public Coordinates myCoordinates;

	//Variables for our second API, weather variables
	public string Key = "e41c38654e85e0905ccd46f91177afba";
	public string city;
	public string latitude;
	public string longitude;
	public int weatherId;
	public string main;
	public string description;
	public float temperature; // in kelvin
	public float pressure;
	public float windSpeed;

	//variable to use in order to set our own main weather and test our environment
	public string OVERRIDE_MAIN;

	//global variable accessible to other files
	public static RealWorldWeather current;

	//set our variable 
    private void Awake()
	{

		current = this;
	}

	//Start our function and set our testing variable
	public void Start()
	{
		StartCoroutine(GetCoordinates());

		OVERRIDE_MAIN = "Light Drizzle";
		
	}

	//access API -> our Coordinates object -> RealWorldWeather variables
	//Use these variables to start the call to our next API
	public IEnumerator GetCoordinates()
	{
		var myLocation = new UnityWebRequest("http://ip-api.com/json/")
		{
			downloadHandler = new DownloadHandlerBuffer()
		};

		yield return myLocation.SendWebRequest();
		if (myLocation.result == UnityWebRequest.Result.ConnectionError)
		{
			//error
			yield break;
		}



		myCoordinates = JsonUtility.FromJson<Coordinates>(myLocation.downloadHandler.text);
		latitude = myCoordinates.lat;
		longitude = myCoordinates.lon;

		string uri = "api.openweathermap.org/data/2.5/weather?";
		uri += "lat=" + latitude + "&lon=" + longitude + "&appid=" + Key;
		StartCoroutine(GetWeatherCoroutine(uri));

	}

	//Repeat the same process
	//once we access the JSON file, we send it to our next function to be parsed
	public IEnumerator GetWeatherCoroutine(string uri)
	{
		using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
		{
			yield return webRequest.SendWebRequest();
			if (webRequest.result == UnityWebRequest.Result.ConnectionError)
			{
				Debug.Log("Web request error: " + webRequest.error);
			}
			else
			{
				ParseJson(webRequest.downloadHandler.text);
			}
		}
	}

	//We use a dynamic object to access the JSON file's fields
	//assign those fields to our own variables
	//Override main HERE!!!!
	public void ParseJson(string json)
	{
		dynamic obj = JObject.Parse(json);

		weatherId = obj.weather[0].id;
		main = obj.weather[0].main;
		description = obj.weather[0].description;
		temperature = obj.main.temp;
		temperature = Celsius(temperature);
		temperature = Fahrenheit(temperature);
		pressure = obj.main.pressure;
		windSpeed = obj.wind.speed;
		//main = OVERRIDE_MAIN;
	}

	//conversion to Celcius
	public float Celsius(float temperature)
	{
		return temperature - 273.15f;
	}

	//conversion to Farenheit
	public float Fahrenheit(float temp)
	{
		return temp * 9.0f / 5.0f + 32.0f;
	}

}



[Serializable]
public class Coordinates
{
	public string lat;
	public string lon;
}