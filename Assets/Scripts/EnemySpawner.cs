using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPrefabs;

    [SerializeField] private List<int> Waves = new List<int>() { 5, 10, 15, 20 }; 
    [SerializeField] private float enemiesPerSecond = 0.5f;
    [SerializeField] private float timeBetweenWaves = 5f;

    public static UnityEvent onEnemyDestroy = new UnityEvent();

    private int currentWave = 0;
    private float timeSincelastSpawn;
    private int enemiesAlive;
    private int enemiesLeftToSpawn;
    private bool isSpawning = false;

    private void Awake()
    {
        onEnemyDestroy.AddListener(EnemyDestroyed);
    }

    private void Start()
    {
        StartCoroutine(StartWave());
    }

    private void Update()
    {
        if (!isSpawning) return;
        timeSincelastSpawn += Time.deltaTime;

        if (timeSincelastSpawn >= (1f / enemiesPerSecond) && enemiesLeftToSpawn > 0)
        {
            SpawnEnemy();
            enemiesLeftToSpawn--;
            enemiesAlive++;
            timeSincelastSpawn = 0f;
        }

        if (enemiesAlive == 0 && enemiesLeftToSpawn == 0)
        {
            EndWave();
        }
    }

    private void EnemyDestroyed()
    {
        enemiesAlive--;
    }

    private IEnumerator StartWave()
    {
        if (currentWave < Waves.Count)
        {
            yield return new WaitForSeconds(timeBetweenWaves);
            isSpawning = true;
            enemiesLeftToSpawn = Waves[currentWave]; // Set the number of enemies for the current wave.
        }
        else
        {
            // This is where we ensure the scene transition happens when all waves are done.
            NextScene();
        }
    }

    private void EndWave()
    {
        isSpawning = false;
        timeSincelastSpawn = 0f;
        currentWave++;

        if (currentWave < Waves.Count)
        {
            StartCoroutine(StartWave()); // Start the next wave
        }
        else
        {
            NextScene();
        }
    }

    private void SpawnEnemy()
    {
        int index = Random.Range(0, enemyPrefabs.Length);
        GameObject prefabToSpawn = enemyPrefabs[index];
        Instantiate(prefabToSpawn, LevelManager.main.startPoint.position, Quaternion.identity);
    }
    public void NextScene()

    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }
}
