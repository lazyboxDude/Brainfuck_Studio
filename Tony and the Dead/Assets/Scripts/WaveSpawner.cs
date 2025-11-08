using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.AI;
using System.Collections.Generic;
using DG.Tweening; // Add this line for DOTween



public class WaveSpawner : MonoBehaviour
{
    [System.Serializable]
    public class EnemyType
    {
        public string enemyName;
        public GameObject enemyPrefab;
        public int baseEnemyHealth;
        public int healthIncreasePerWave = 1; // How much to increase health each wave
        public bool isBossEnemy = false;
        public int spawnCount;
        public int startWave = 1;
        public int spawnEveryXWaves = 1;
        public int enemyMultiplierPerWave = 0;
    }

    public EnemyType[] availableZombies;  // All available zombie types
    
    [System.Serializable]
    public class SpawnRegion
    {
        public string regionName;
        public Transform[] spawnWaypoints;  // Mindestens 3 pro Region
        public bool isUnlocked = false;
    }

    public SpawnRegion[] spawnRegions;
    public float waypointRadius = 2f;

    private NavMeshAgent navMeshAgent;

    public static WaveSpawner Instance { get; private set; }
    public float SecondsBetweenWaves = 120f; //Time between waves
    private float countdown = 2f; //Countdown timer

    public int waveIndex = 0; //Current wave index
     
    public float spawnRadius = 20f;

    public int EnemySpawnRatePerWave = 2;
    public int enemiesAlive = 0; //Number of enemies alive
    
    //UI 
    public TMP_Text waveText; // For TextMeshPro
    
    public void Awake()
    {
        Instance = this;
        SpawnWave();
        print("Wave Spawner Initialized");
    }

    public void SpawnWave()
    {
        waveIndex++; // Increase wave index at the start of spawning a new wave
        waveText.text = "Wave " + waveIndex;
        for (int i = 0; i < EnemySpawnRatePerWave * waveIndex; i++)
        {
            SpawnEnemy();
            enemiesAlive++;
        }
    }

    public void SpawnEnemy()
    {
        // Choose a random enemy type from available zombies
        EnemyType enemyType = availableZombies[Random.Range(0, availableZombies.Length)];

        // Get a random spawn position from waypoints
        Vector3 spawnPosition = GetSpawnPosition();


        // Instantiate the enemy prefab at the spawn position
        GameObject enemy = Instantiate(enemyType.enemyPrefab, spawnPosition, Quaternion.identity);
        enemy.transform.localScale = Vector3.zero;
        enemy.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack);
    //    Set enemy health based on wave index
    //    BaseZombie enemyHealth = enemy.GetComponent<BaseZombie>();
    //    if (enemyHealth != null)
    //    {
    //        int healthIncrease = enemyType.healthIncreasePerWave * (waveIndex - 1);
    //        //enemyHealth.SetHealth(enemyType.baseEnemyHealth + healthIncrease);
    //    }
    }

    public Vector3 GetSpawnPosition()
    {
        List<SpawnRegion> unlockedRegions = new List<SpawnRegion>();
        foreach (var region in spawnRegions)
        {
            if (region.isUnlocked)
            {
                unlockedRegions.Add(region);
            }
        }
        if (unlockedRegions.Count > 0)
        {
            SpawnRegion randomRegion = unlockedRegions[Random.Range(0, unlockedRegions.Count)];

            Transform randomWaypoint = randomRegion.spawnWaypoints[Random.Range(0, randomRegion.spawnWaypoints.Length)];

            // Zufällige Position im Radius
            Vector2 randomCircle = Random.insideUnitCircle * waypointRadius;
            Vector3 offset = new Vector3(randomCircle.x, 0, randomCircle.y);

            return randomWaypoint.position + offset;
        }

        return spawnRegions[0].spawnWaypoints[0].position; // Return a default Region if no regions are unlocked

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