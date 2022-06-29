using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyCondition : MonoBehaviour
{

    /// <summary>
    /// We use our Forecast to enable, disable, or make changes to environment
    /// </summary>
    
    //Get reference to our gameObject
    public GameObject myGameObject;
    public string currentForecast;
    public bool isClear = false;

    //Check if our forecast contains any word that would make this
    //particular condition true/false
    public void checkCondition()
    {
        if (currentForecast.Contains("Clear") == true)         
            isClear = true;
        else
            isClear = false;
    }

    //if any of these conditions are met, we want them to affect our world
    public void isSkyClear()
    {
        if (isClear)
            myGameObject.SetActive(false);
    }

     void Start()
    {
        myGameObject = this.gameObject;

    }

    //update our local variable using static, and run our functions
    void Update()
    {
        if (RealWorldWeather.current.main != null)
            currentForecast = RealWorldWeather.current.main;

        checkCondition();
        isSkyClear();
    }
}
