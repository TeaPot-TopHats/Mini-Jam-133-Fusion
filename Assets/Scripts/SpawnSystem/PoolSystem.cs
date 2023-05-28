using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class PoolSystem : MonoBehaviour
{

    public GameObject fireEnemyPrefab;
    public GameObject iceEnemyPrefab;
    public GameObject electricEnemyPrefab;

    public List<GameObject> fireEnemies = new List<GameObject>();
    public List<GameObject> iceEnemies = new List<GameObject>();
    public List<GameObject> electricEnemies = new List<GameObject>();

    private Dictionary<EnemyType, List<GameObject>> enemyPool = new Dictionary<EnemyType, List<GameObject>>();

    private void Awake()
    {
        enemyPool.Add(EnemyType.Fire, fireEnemies);
        enemyPool.Add(EnemyType.Ice, iceEnemies);
        enemyPool.Add(EnemyType.Electric, electricEnemies);
    }

    public GameObject GetEnemy(EnemyType enemyType)
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
        GameObject prefab = GetEnemyPrefab(enemyType);
        GameObject enemy = Instantiate(prefab, Vector3.zero, Quaternion.identity);
        enemyList.Add(enemy);
        return enemy;
    }

    private GameObject GetEnemyPrefab(EnemyType enemyType)
    {
        switch (enemyType)
        {
            case EnemyType.Fire:
                return fireEnemyPrefab;
            case EnemyType.Ice:
                return iceEnemyPrefab;
            case EnemyType.Electric:
                return electricEnemyPrefab;
            default:
                Debug.LogError("Unknown enemy type: " + enemyType.ToString());
                return null;
        }
    }
}


