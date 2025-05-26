using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform[] spawnPoints;
    public float spawnInterval = 5f; // Интервал между спавнами
    public int enemiesPerWave = 3;

    private bool spawning = false;
    private float timer = 0f;

    public void StartSpawning()
    {
        spawning = true;
        timer = spawnInterval; // чтобы первый спавн был через 5 секунд
    }
    public void StopSpawning()
    {
        spawning = false;
        timer = 0f; // Сброс таймера
    }

    void Update()
    {
        if (!spawning) return;

        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            SpawnEnemies();
            timer = spawnInterval;
        }
    }

    private void SpawnEnemies()
    {
        for (int i = 0; i < enemiesPerWave; i++)
        {
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        }

        Debug.Log("Волна врагов заспавнена!");
    }
}
