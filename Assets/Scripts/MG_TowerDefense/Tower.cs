using UnityEngine;
using System.Collections;

public class Tower : MonoBehaviour
{
    // This class is responsible for the tower's health and damage system. It also spawns enemies at a set interval.
    [Header("Minigame Properties")]
    public float towerHealth = 100;
    public float towerDamage = 2.0f;
    public float towerDamageCritMultiplier = 2.0f;
    public float towerDamageCritChance = 10f; // Out of 100, 10% chance to crit 
    [Tooltip("Enemy's attack speed (per second).")]
    public float towerDamageTickRate = 2.0f; // The enemy's attack speed.
    [Tooltip("Number of seconds until next enemy spawns")]
    public float enemySpawnRate = 10.0f;
    public int nextEnemyID = 0;

    public bool isOngoing = true;
    

    [SerializeField] private Enemy_TD enemyScript;

    Transform spawnLocation;
    GameObject enemy;

    void Start()
    {
        spawnLocation = GameObject.Find("EnemySpawnPoint").transform;
        enemy = Resources.Load<GameObject>("Prefabs/Evil Bun");
        StartCoroutine(Loop()); // 1. Initiate loop that continuously spawns enemies every x seconds
    }

    

    void spawnEnemy(Transform location) // Spawns an enemy at a location
    {
        GameObject spawnedEnemy = Instantiate(
            enemy,
            location.position,
            location.rotation
        );

        Enemy_TD enemyScript = spawnedEnemy.GetComponent<Enemy_TD>(); // Communicates to the Enemy script
       
        if (enemyScript != null)
        {
            enemyScript.enemyID = nextEnemyID;
            spawnedEnemy.name = "EvilBun_" + nextEnemyID; // IDs each spawned enemy to the enemyScript
            nextEnemyID++;
            //Debug.Log("Spawned Evil Bun ID: " + enemyScript.enemyID);
        }
    }

    public float takeDamage()
    {
        
        float rollDice = UnityEngine.Random.Range(0f, 100f);
        if (rollDice <= (towerDamageCritChance)) 
        {
            // TODO: Insert game logic feedback here for when enemy crit damage happens
            return (float)(towerDamage * towerDamageCritMultiplier);
        } else
        {
            return (float)towerDamage;
        }
    }

    IEnumerator Loop() // Wait 5 seconds to spawn an enemy
    {
        while (isOngoing == true)
        {
            spawnEnemy(spawnLocation);
            yield return new WaitForSeconds(enemySpawnRate);
        }
    }

    

}
    