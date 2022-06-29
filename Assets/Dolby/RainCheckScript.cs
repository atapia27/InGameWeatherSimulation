using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainCheckScript : MonoBehaviour
{
    /// <summary>
    /// We use our Forecast to enable, disable, or make changes to environment
    /// </summary>

    public GameObject myGameObject;
    public string currentForecast;
    public bool isRain = true;

    //Check if our forecast contains any word that would make this
    //particular condition true/false
    public void checkCondition()
    {
        if (currentForecast.Contains("Drizzle") == true || currentForecast.Contains("Rain") == true || currentForecast.Contains("Thunder") == true)
            isRain = true;
        else
            isRain = false;
    }

    //if any of these conditions are met, we want them to affect our world
    public void isRaining()
    {
        if (isRain)
            myGameObject.SetActive(true);
        else
            myGameObject.SetActive(false);
    }



    //update our local variable using static, and run our functions
    void Update()
    {
        if (RealWorldWeather.current.main != null)
            currentForecast = RealWorldWeather.current.main;

        checkCondition();
        isRaining();
    }
}
