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
    public int maxEnemyCount = 300;

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

        weatherTracker.UpdateWeather(SkillType.None);
        

        if (spawnCountdown <= 0)
        {
            SpawnEnemy(currentEnemyCount); // Spawn the enemy
            currentEnemyCount += enemyCountIncrement; // increase enemy spawning number
            spawnCountdown = timeBetweenSpawns; // Reset the spawn countdown      
        }
        else
        {
            spawnCountdown -= Time.deltaTime; // run the counter to spawn enemies
        }
    }

    /*
     *  A method that check if the spawn point are in the POV or not
     */
    private bool IsPointInsideCameraView(Vector3 point, float cameraWidth, float cameraHeight)
    {
        Camera mainCamera = Camera.main;
        Vector3 pointInView = mainCamera.WorldToViewportPoint(point);

        return (pointInView.x >= 0 && pointInView.x <= 1 && pointInView.y >= 0 && pointInView.y <= 1);
    }

    /*
     * a Tranform that grabs spawn points in the map that are off camera randomly and use that to spawn enemies in SpawnEnemy()
     */
    private Transform GetRandomOffCameraSpawnPoint()
    {
        Camera mainCamera = Camera.main;
        float cameraHeight = mainCamera.orthographicSize;
        float cameraWidth = cameraHeight * mainCamera.aspect;

        // Find a spawn point that is off-camera
        Transform spawnPoint;
        do
        {
            spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)]; // select spawn point randomly
        }
        while (IsPointInsideCameraView(spawnPoint.position, cameraWidth, cameraHeight)); // 

        return spawnPoint;
    }

    /*
     * A method that spawn enemies using DetermienEnemyType to spawn the correct type of enemy based on Weather/element/color
     */
    private void SpawnEnemy(int enemyCount)
    {
        int totalEnemiesInMap = enemyPool.GetTotalEnemies();
        int enemiesToSpawn = Mathf.Min(enemyCount, maxEnemyCount - totalEnemiesInMap); // Calculate the number of enemies to spawn while considering the maximum limit

        for (int i = 0; i < enemiesToSpawn; i++)
        {
            
                EnemyType enemyType = DetermineEnemyType(); // Determine the enemy type based on weather

                Debug.Log("Spawning enemy: " + enemyType.ToString() + " Current Weather is: " + weatherTracker.GetCurrentWeather().ToString());

                Transform spawnPoint = GetRandomOffCameraSpawnPoint();

                // Get the enemy from the pool based on the enemy type
                GameObject enemy = enemyPool.GetEnemy(enemyType, spawnPoint);// getting enemy obejct from the enemy pool

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
        WeatherType currentWeather = weatherTracker.GetCurrentWeather(); // get the current weather.

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
            case WeatherType.All:
                enemyType = EnemyType.ALL;
                break;
            default: // Default to fire enemies if weather is unknown or not accounted for
                enemyType = EnemyType.Fire;
                break;
        }

        return enemyType;
    }

}// end of class

