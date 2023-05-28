using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeatherTracker
{
    private WeatherType currentWeather;

    public WeatherType GetCurrentWeather()
    {
        return currentWeather;
    }

    public void UpdateWeather()
    {
        // Implement Weather/Map change logic here to update the current weather
        
        // For the sake of example, let's assume the weather changes randomly
        int randomIndex = Random.Range(0, 3);
        currentWeather = (WeatherType)randomIndex;
    }
}

