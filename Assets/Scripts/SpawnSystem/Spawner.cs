using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Spawner : MonoBehaviour
{
    private WeatherTracker weatherTracker;

    public int startingEnemyCount = 5;
    public int enemyCountIncrement = 2;
    public int currentEnemyCount;

    public float timeBetweenSpawns = 5f;
    public float spawnCountdown;

    public Transform[] spawnPoints;
    public PoolSystem enemyPool;

    private void Start()
    {
        currentEnemyCount = startingEnemyCount;

        weatherTracker = new WeatherTracker();

        spawnCountdown = timeBetweenSpawns;

        if (spawnPoints.Length == 0)
        {
            Debug.LogWarning("No spawn points are found");
        }
    }

    private void Update()
    {

        weatherTracker.UpdateWeather();
        // WeatherType currentWeather = weatherTracker.GetCurrentWeather();
        //Debug.Log("Current Weather is: " + currentWeather.ToString());

        if (spawnCountdown <= 0)
        {
            SpawnEnemy(currentEnemyCount); // Spawn the enemy
            currentEnemyCount += enemyCountIncrement; // increase enemy spawning number
            spawnCountdown = timeBetweenSpawns; // Reset the spawn countdown      
        }
        else
        {
            spawnCountdown -= Time.deltaTime;
        }
    }
    private void SpawnEnemy(int enemyCount)
    {
        for (int i = 0; i < enemyCount; i++)
        {
            EnemyType enemyType = DetermineEnemyType(); // Determine the enemy type based on weather

            Debug.Log("Spawning enemy: " + enemyType.ToString() + " Current Weather is: " + weatherTracker.GetCurrentWeather().ToString());

            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

            // Get the enemy from the pool based on the enemy type
            GameObject enemy = enemyPool.GetEnemy(enemyType);

            // Position and activate the enemy
            enemy.transform.position = spawnPoint.position;
            enemy.transform.rotation = spawnPoint.rotation;
            enemy.SetActive(true);
        }
    }

    private EnemyType DetermineEnemyType()
    {
        // Implement your logic here to determine the enemy type based on weather or tracker
        // Return the appropriate enemy type
        WeatherType currentWeather = weatherTracker.GetCurrentWeather();

        EnemyType enemyType;

        switch (currentWeather)
        {
            case WeatherType.Sunny:
                enemyType = EnemyType.Fire; // Spawn fire enemies for sunny weather
                break;
            case WeatherType.Rainy:
                enemyType = EnemyType.Ice; // Spawn ice enemies for rainy weather
                break;
            case WeatherType.Stormy:
                enemyType = EnemyType.Electric; // Spawn electric enemies for stormy weather
                break;
            default:
                enemyType = EnemyType.ALL; // Default to fire enemies if weather is unknown or not accounted for
                break;
        }

        return enemyType;
    }

}// end of class

