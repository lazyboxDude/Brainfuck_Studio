using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.AI;

public class WaveSpawner : MonoBehaviour
{
    public GameObject enemyPrefab; //Enemy Prefabs
    private NavMeshAgent navMeshAgent;

    public static WaveSpawner Instance { get; private set; }
    public float SecondsBetweenWaves = 120f; //Time between waves
    private float countdown = 2f; //Countdown timer

    public int waveIndex = 0; //Current wave index
     
    public float spawnRadius = 20f;
    public int ZombiesPerWave = 5; //Number of zombies per wave
    public int EnemySpawnRatePerWave = 2;
    public int enemiesAlive = 0; //Number of enemies alive
    public int healthIncreasePerWave = 1; // How much to increase health each wave

    public int baseEnemyHealth;

    //UI 
    public TMP_Text waveText; // For TextMeshPro
    
    public void Awake()
    {
        Instance = this;
        SpawnWave();
    }
 
    public void SpawnWave()
    {
        int zombiesToSpawn = ZombiesPerWave + (waveIndex * EnemySpawnRatePerWave);
        for (int i = 0; i < zombiesToSpawn; i++)
        {
            // spawn enemy at random point within spawnRadius
            Vector3 randomDirection = Random.insideUnitSphere * spawnRadius;
            randomDirection += transform.position;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomDirection, out hit, spawnRadius, NavMesh.AllAreas))
            {
                GameObject enemy = Instantiate(enemyPrefab, hit.position, Quaternion.identity);
                
                // Set the health if the script exists
                var zombie = enemy.GetComponent<BaseZombie>();
                if (zombie != null)
                {
                    zombie.HealthFloat = baseEnemyHealth + (waveIndex * healthIncreasePerWave);
                }
                enemiesAlive++;
                Debug.Log("Enemy Spawned. Total Enemies Alive: " + enemiesAlive);
            }
        }

        //increase the Wave Counter
        waveIndex++;
        waveText.text =  ("Wave: " + waveIndex   );
    }
    public void EnemyDied()
    {
        enemiesAlive--;
        if (enemiesAlive <= 0)
        {
            //start the next wave after a delay
            StartCoroutine(SpawnWaveAfterDelay());
        }
    }
    private IEnumerator SpawnWaveAfterDelay()
    {
        yield return new WaitForSeconds(SecondsBetweenWaves);
        SpawnWave();
    }
}