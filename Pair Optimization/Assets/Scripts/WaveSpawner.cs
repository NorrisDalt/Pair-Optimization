using System.Collections;
using UnityEngine;
using TMPro;

public class WaveSpawner : MonoBehaviour
{
    public static WaveSpawner Instance;

    [Header("References")]
    public Transform player;
    public TMP_Text waveText;

    [Header("Wave Settings")]
    public int enemiesPerWave = 5;
    public float spawnDelay = 0.3f;
    public float waveStartDelay = 2f;

    private int currentWave = 0;
    private int enemiesAlive = 0;

    private Vector3[] spawnPoints;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        // 4 edge spawn points (square map -5 to 5)
        spawnPoints = new Vector3[]
        {
            new Vector3(-5, 0, 0), // left
            new Vector3(5, 0, 0),  // right
            new Vector3(0, 5, 0),  // top
            new Vector3(0, -5, 0)  // bottom
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

        yield return new WaitForSeconds(waveStartDelay);

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
            Invoke(nameof(StartNextWave), 2f);
        }
    }
}