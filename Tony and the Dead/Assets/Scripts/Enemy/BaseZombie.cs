using UnityEngine;
using UnityEngine.AI;
using Unity.Behavior;
using Unity.Behavior.Example;
using Mono.Cecil.Cil;

public class BaseZombie : MonoBehaviour
{
    //[SerializeField] private float speed = 1.0f;
    [SerializeField] private int health = 100;
    public int Health { get { return health; } }

    private bool isDead = false;
    public float HealthFloat
    {
        get { return (float)health; }
        set { health = Mathf.RoundToInt(value); }
    }

    public float KillPoints = 10;
    public float PointsMultiplierPerWave = 0.1f;

    private NavMeshAgent navMeshAgent;
    private BehaviorGraphAgent behaviorAgent;
    


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = 1.0f;
        behaviorAgent = GetComponent<BehaviorGraphAgent>();
        SetHealth(health);
    
    }

    // Update is called once per frame
    void Update()
    {
    
        if (behaviorAgent != null)
        {
            behaviorAgent.BlackboardReference.SetVariableValue("Health", (float)health);
        }
        else
        {
            Debug.LogWarning("BehaviorGraphAgent component not found on " + gameObject.name);
        }
    }

    public void TakeDamage(int damage)
    {
        if (isDead)
            return;
        //reduce health
        health -= damage;
        //update behavior agent health if exists


        if (health <= 0)
        {
            navMeshAgent.isStopped = true;
            Die();

        }
    }

    public void SetHealth(int newHealth)
    {
        health = newHealth;
    }

     public void Die()
    {
        var WaveSpawner = FindFirstObjectByType<WaveSpawner>();
        if (isDead )return;
        isDead = true;

        //      Notify the spawner that this enemy has died
        if (WaveSpawner != null)
            WaveSpawner.EnemyDied();
            var GlobalReferences = FindFirstObjectByType<GlobalReferences>();
            int waveIndex = WaveSpawner.waveIndex;
            float CalculatedKillPoints = KillPoints * (1 + (waveIndex * PointsMultiplierPerWave));
        // Add points to the global reference
        GlobalReferences.AddPoints(CalculatedKillPoints);
            Debug.Log("Enemy Alive " + WaveSpawner.enemiesAlive);
            //Debug.Log("Enemy Died. Points Awarded: " + CalculatedKillPoints);

            //GlobalReferences.instance.AddPoints(KillPoints * (waveIndex / 100));
    }
}
