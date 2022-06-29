using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TreeWindController : MonoBehaviour
{
    /// <summary>
    /// We use our Forecast to enable, disable, or make changes to environment
    /// </summary>

    //wind might be light, heavy, or normal (reprsented by default/else)
    //default < light < heavy
    public Material defaultWindMaterial;
    public Material lightWindMaterial;
    public Material heavyWindMaterial;

    public Renderer my_renderer;

    public bool isLight = false;
    public bool isHeavy = false;

    public string currentForecast;


    private void Start()
    {
        //CHANGE MATERIAL
        my_renderer = GetComponent<Renderer>();


    }


    //Check if our forecast contains any word that would make this
    //particular condition true/false
    public void checkCondition()
    {
        //LIGHT
        if (currentForecast.Contains("ight") == true)         //Take out the first letter in case it is capital
            isLight = true;
        else
            isLight = false;

        //HEAVY
        if (currentForecast.Contains("eavy") == true || currentForecast.Contains("ragged") == true || currentForecast.Contains("extreme") == true)
            isHeavy = true;
        else
            isHeavy = false;
        //There seem to be a lot of words for "a lot" lol

    }

    //if any of these conditions are met, we want them to affect our world
    private void OnWindChange()
    {
        //DO THE ACTION
        if (isLight && isHeavy)
            print("STOP MESSING WITH MY DATA"!);

        else if (isHeavy)
            my_renderer.material = heavyWindMaterial;

        else if (isLight)
            my_renderer.material = lightWindMaterial;

        else if (!isLight && !isHeavy)
            my_renderer.material = defaultWindMaterial;


    }

    //update our local variable using static, and run our functions
    public void Update()
    {
        if (RealWorldWeather.current.main != null)
            currentForecast = RealWorldWeather.current.main;

        checkCondition();
        OnWindChange();

    }
}
