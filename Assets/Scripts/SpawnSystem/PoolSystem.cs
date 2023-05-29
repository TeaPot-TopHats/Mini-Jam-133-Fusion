using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class PoolSystem : MonoBehaviour
{

    public GameObject fireEnemyPrefab;
    public GameObject iceEnemyPrefab;
    public GameObject electricEnemyPrefab;
    public GameObject[] allEnemyPrefap;

    public List<GameObject> fireEnemies = new List<GameObject>();
    public List<GameObject> iceEnemies = new List<GameObject>();
    public List<GameObject> electricEnemies = new List<GameObject>();
    public List<GameObject> allEnemies = new List<GameObject>();

    public int totalEnemiesInMap;

    private Dictionary<EnemyType, List<GameObject>> enemyPool = new Dictionary<EnemyType, List<GameObject>>();

    private void Awake()
    {
        enemyPool.Add(EnemyType.Fire, fireEnemies);
        enemyPool.Add(EnemyType.Ice, iceEnemies);
        enemyPool.Add(EnemyType.Electric, electricEnemies);
        enemyPool.Add(EnemyType.ALL, allEnemies);
    }

    private void Update()
    {
        // GetTotalEnemies();
    }

    public int GetTotalEnemies()
    {
        totalEnemiesInMap = fireEnemies.Count + iceEnemies.Count + electricEnemies.Count + allEnemies.Count;
        return totalEnemiesInMap;
    }
   

    public GameObject GetEnemy(EnemyType enemyType, Transform spawnPoint)
    {
        List<GameObject> enemyList = enemyPool[enemyType];

        for (int i = 0; i < enemyList.Count; i++)
        {
            if (!enemyList[i].activeInHierarchy)
            {
                return enemyList[i];
            }
        }

        // If no inactive enemy found in the pool, instantiate a new one
        GameObject[] prefabs = GetEnemyPrefab(enemyType);
        if (prefabs != null && prefabs.Length > 0)
        {
            for (int i = 0; i < prefabs.Length; i++)
            {
                GameObject enemy = Instantiate(prefabs[i], spawnPoint.position, spawnPoint.rotation);
                enemyList.Add(enemy);
            }

            return enemyList[0];
        }
        else
        {
            Debug.LogError("No enemy prefab found for enemy type: " + enemyType.ToString());
            return null;
        };
    }

    private GameObject[] GetEnemyPrefab(EnemyType enemyType)
    {
       
        switch (enemyType)
        {
            case EnemyType.Fire:
                return new GameObject[] { fireEnemyPrefab };
            case EnemyType.Ice:
                return new GameObject[] { iceEnemyPrefab };
            case EnemyType.Electric:
                return new GameObject[] { electricEnemyPrefab };
            case EnemyType.ALL:
                int num = Random.Range(0, 3);
                if(num == 0)
                    return new GameObject[] { fireEnemyPrefab };
                else if(num == 1)
                    return new GameObject[] { iceEnemyPrefab };
                else
                    return new GameObject[] { electricEnemyPrefab };
            default:
                Debug.LogError("Unknown enemy type: " + enemyType.ToString());
                return null;
        }
    }
}


