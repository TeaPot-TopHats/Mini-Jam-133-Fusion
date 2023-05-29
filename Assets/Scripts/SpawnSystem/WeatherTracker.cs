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
    public void UpdateWeather(ChargeType skillType)
    {
        /*
        int randomIndex = Random.Range(0, 3);
        currentWeather = (WeatherType)randomIndex;
        */
        switch (skillType)
        {
            case ChargeType.FIRE:
                currentWeather = WeatherType.Sunny;
                break;
            case ChargeType.ICE:
                currentWeather = WeatherType.Rainy;
                break;
            case ChargeType.ELECTRIC:
                currentWeather = WeatherType.Stormy;
                break;
            default:
                currentWeather = WeatherType.All;
                break;
        }
    }
}

