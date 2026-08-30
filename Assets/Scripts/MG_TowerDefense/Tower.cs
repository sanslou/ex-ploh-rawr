using UnityEngine;
using System.Collections;

public class Tower : MonoBehaviour
{
    [Header("Minigame Properties")]
    public float Health = 100;
    public float Damage = 1.0f;
    public float DamageMultiplier = 1.0f;
    public float DamagePerTick = 1.0f;
    public bool isOngoing = true;

    int nextEnemyID = 0;

    [SerializeField] private Enemy_TD enemyScript;
    

    Transform spawnLocation;
    GameObject enemy;

    void Start()
    {
        spawnLocation = GameObject.Find("EnemySpawnPoint").transform;
        enemy = Resources.Load<GameObject>("Prefabs/Evil Bun");
        StartCoroutine(Loop());
    }

    void spawnEnemy(Transform location) // Spawns an enemy at a location
    {
        GameObject spawnedEnemy = Instantiate(
            enemy,
            location.position,
            location.rotation
        );

        NPC_EvilBun enemyScript = spawnedEnemy.GetComponent<NPC_EvilBun>(); // Communicates to the NPC_EvilBun script

        if (enemyScript != null)
        {
            enemyScript.enemyID = nextEnemyID;
            spawnedEnemy.name = "EvilBun_" + nextEnemyID; // IDs each spawned enemy
            nextEnemyID++;

            //Debug.Log("Spawned Evil Bun ID: " + enemyScript.enemyID);
        }
    }   

    IEnumerator Loop() // Wait 5 seconds to spawn an enemy
    {
        while (isOngoing == true)
        {
            spawnEnemy(spawnLocation);
            yield return new WaitForSeconds(5f);
        }
    }
}
    