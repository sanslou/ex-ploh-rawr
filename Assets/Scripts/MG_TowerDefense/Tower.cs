using UnityEngine;
using System.Collections;
using System.Linq;

public class Tower : MonoBehaviour
{
    // This class is responsible for the tower and its properties, as well as enemy spawn properties.
    [Header("Tower Properties")]
    [Tooltip("Tower's health in float values")]
    public float towerHealth = 100;
    
    [Tooltip("Number of seconds until next enemy spawns")]
    public float enemySpawnRate = 10.0f;

    [Tooltip("Number of enemies to summon per wave")]
    public int enemiesPerWave = 4;

    [Tooltip("The total number of waves before the minigame's completion")]
    public int numberOfWaves = 3;

    //
    [Header("Individual Enemy Properties")]
    [Tooltip("A singular enemy's tower damage")]
    public float enemyDamage = 2.0f;

    [Tooltip("A singular enemy's tower damage multiplier whenever a critical chance is a success")]
    public float enemyDamageCritMultiplier = 2.0f;

    [Tooltip("Chance to crit (out of 100).")]
    public float enemyDamageCritChance = 10f;

    [Tooltip("Enemy's attack speed (per second).")]
    public float enemyDamageTickRate = 2.0f;

    //
    [Header("Miscellaneous")]
    public int nextEnemyID = 0;
    public bool isOngoing = true;

    // <Private>
    [Header("Serialized References")] // Debugging purposes.
    [SerializeField] private Enemy_TD enemyScript;
    [SerializeField] private Transform spawnLocation;
    [SerializeField] private Transform[] spawnChildren;
    [SerializeField] private GameObject enemy;
    // </Private>

    void Start()
    {
        spawnLocation = GameObject.Find("enemySpawns").transform;
        foreach (Transform child in spawnLocation) // Get all spawn locations
        {
            spawnChildren = spawnLocation.GetComponentsInChildren<Transform>().Where(t => t != spawnLocation).ToArray(); 
            // .Where() ensures that the parent object is not included in the array
            //Debug.Log("Spawn location: " + child.name);
        }
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
        if (rollDice <= (enemyDamageCritChance)) 
        {
            // TODO: Insert game logic feedback here for when enemy crit damage happens
            return (float)(enemyDamage * enemyDamageCritMultiplier);
        } else
        {
            return (float)enemyDamage;
        }
    }

    IEnumerator Loop() 
    {
        while (isOngoing == true)
        {
            Debug.Log("Spawning enemies in " + enemySpawnRate + " seconds...");
            for (int wave = numberOfWaves; wave>0; wave--)
                Debug.Log("Wave " + wave + " incoming!");
            {
                for (int i = 0; i < enemiesPerWave; i++)
                {
                    spawnEnemy(spawnChildren[i]);
                    Debug.Log("Spawned enemy " + i + " of wave ");
                }
            }
            yield return new WaitForSeconds(enemySpawnRate);
        }
    }

    

}
    