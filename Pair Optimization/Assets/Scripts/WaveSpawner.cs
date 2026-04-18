using System.Collections;
using UnityEngine;
using TMPro;

public class WaveSpawner : MonoBehaviour
{
    public static WaveSpawner Instance;

    public Transform player;
    public TMP_Text waveText;

    public int enemiesPerWave = 5;
    public float spawnDelay = 0.3f;

    private int currentWave = 0;
    private int enemiesAlive = 0;

    private Vector3[] spawnPoints;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        spawnPoints = new Vector3[]
        {
            new Vector3(-5, 0, 0),
            new Vector3(5, 0, 0),
            new Vector3(0, 5, 0),
            new Vector3(0, -5, 0)
        };

        StartNextWave();
    }

    void StartNextWave()
    {
        currentWave++;
        waveText.text = "Wave: " + currentWave;

        StartCoroutine(SpawnWave());
    }

    IEnumerator SpawnWave()
    {
        enemiesAlive = enemiesPerWave;

        for (int i = 0; i < enemiesPerWave; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    void SpawnEnemy()
    {
        int index = Random.Range(0, spawnPoints.Length);

        GameObject enemyObj = EnemyPool.Instance.GetEnemy();
        enemyObj.transform.position = spawnPoints[index];
        enemyObj.SetActive(true);

        enemyObj.GetComponent<Enemy>().SetTarget(player);
    }

    public void OnEnemyKilled()
    {
        enemiesAlive--;

        if (enemiesAlive <= 0)
        {
            Invoke("StartNextWave", 2f); // small break between waves
        }
    }
}