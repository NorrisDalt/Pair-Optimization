using System.Collections;
using UnityEngine;
using TMPro;

public class WaveSpawner : MonoBehaviour
{
    public static WaveSpawner Instance;

    [Header("References")]
    public Transform player;
    public TMP_Text waveText;

    [Header("Settings")]
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
        // 4 edge spawn points (-5 to 5 arena)
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

        GameManager.Instance.SetRound(currentWave);

        StartCoroutine(SpawnWave());
    }

    IEnumerator SpawnWave()
    {
        yield return new WaitForSeconds(waveStartDelay);

        // scaling enemy count:
        int enemiesThisWave = 3 + currentWave;
        enemiesAlive = enemiesThisWave;

        for (int i = 0; i < enemiesThisWave; i++)
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