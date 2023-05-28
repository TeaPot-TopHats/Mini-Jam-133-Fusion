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
    public void UpdateWeather(SkillType skillType)
    {
        /*
        int randomIndex = Random.Range(0, 3);
        currentWeather = (WeatherType)randomIndex;
        */
        switch (skillType)
        {
            case SkillType.FireSlash:
                currentWeather = WeatherType.Sunny;
                break;
            case SkillType.IceSlash:
                currentWeather = WeatherType.Rainy;
                break;
            case SkillType.LightningSlash:
                currentWeather = WeatherType.Stormy;
                break;
            default:
                currentWeather = WeatherType.All;
                break;
        }
    }
}

