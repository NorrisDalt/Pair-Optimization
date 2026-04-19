using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    public static EnemyPool Instance;

    public GameObject enemyPrefab;
    public int poolSize = 20;

    private List<GameObject> pool = new List<GameObject>();
    
    // Static variables for all enemies to use
    public Transform player;
    public float enemyMoveSpeed = 2f;

    void Awake()
    {
        Instance = this;

        for (int i = 0; i < poolSize; i++)
        {
            GameObject e = Instantiate(enemyPrefab);
            e.SetActive(false);
            pool.Add(e);
        }
    }

    public GameObject GetEnemy()
    {
        foreach (GameObject e in pool)
        {
            if (!e.activeInHierarchy)
                return e;
        }

        // fallback if pool runs out
        return Instantiate(enemyPrefab);
    }
}
