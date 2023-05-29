using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeatherTracker : MonoBehaviour
{
    [SerializeField] private ChargeType currentWeather = ChargeType.FIRE;

    public ChargeType GetCurrentWeather()
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
                currentWeather = ChargeType.FIRE;
                break;
            case ChargeType.ICE:
                currentWeather = ChargeType.ICE;
                break;
            case ChargeType.ELECTRIC:
                currentWeather = ChargeType.ELECTRIC;
                break;
            default:
                currentWeather = ChargeType.FIRE;
                break;
        }
    }
}

