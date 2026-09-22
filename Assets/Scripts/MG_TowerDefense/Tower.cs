using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class Tower : MonoBehaviour
{
    // This class is responsible for the tower and its properties, as well as enemy spawn properties.
    [Header("Tower Properties")]
    [Tooltip("Tower's health in float values")]
    public float towerHealth; //500;

    [Tooltip("Number of seconds until next enemy spawns")]
    public float enemySpawnRate; // 20.0f;

    [Tooltip("Number of enemies to summon per wave")]
    public int enemiesPerWave; // 4;

    [Tooltip("The total number of waves before the minigame's completion")]
    public int numberOfWaves; // 3;
    public int currentWavesLeft;

    //
    [Header("Individual Enemy Properties")]
    [Tooltip("A singular enemy's tower damage")]
    public float enemyDamage = 15.0f;
    [Tooltip("A singular enemy's tower damage multiplier whenever a critical chance is a success")]
    public float enemyDamageCritMultiplier = 2.0f;
    [Tooltip("Chance to crit (out of 100).")]
    public float enemyDamageCritChance = 10f;
    [Tooltip("Enemy's attack speed (per second).")]
    public float enemyDamageTickRate = 2.0f;

    [Header("Player Properties")]
    [Tooltip("Player's minimum damage against enemies not yet influenced by critical strikes.")]
    public float playerDamage = 26.0f;
    [Tooltip("Player's attack speed per second")] // NOT YET IMPLEMENTED
    public float playerAtkSpeed = 1.0f; // in seconds

    //
    [Header("Miscellaneous")]
    public int nextEnemyID = 0;
    public bool isOngoing = true;
    
    

    // <Private>
    [Header("Serialized References")] // Debugging purposes.
    [SerializeField] private Transform spawnLocation;
    [SerializeField] private Transform[] spawnChildren;
    [SerializeField] private GameObject enemy;
    [SerializeField] private Enemy_TD enemyScript;
    [SerializeField] private NametagManager nametagScript;
    [SerializeField] private TextMeshProUGUI td_question; // UI     
    public Slider towerHpBar;
    private Button buttonAttack;
    private List<int> selectedIndexes = new List<int>(); // attachNametag(): Lists all selected indexes to avoid duplicates
    // </Private>


    void Start()
    {
        spawnLocation = GameObject.Find("enemySpawns").transform;
        buttonAttack = GameObject.FindGameObjectWithTag("UI_Attack").GetComponent<Button>();
        nametagScript = GetComponent<NametagManager>();
        currentWavesLeft = numberOfWaves;
        
        towerHpBar = GameObject.Find("TowerHPBar").GetComponent<Slider>();
        towerHpBar.value = towerHealth;
        towerHpBar.maxValue = towerHealth;


        if (isOngoing)
        {
            buttonAttack.interactable = true;
            Debug.Log("Tower Defense minigame is ongoing. Attack button enabled.");

            td_question = GameObject.Find("TowerDefenseUI").GetComponent<TextMeshProUGUI>();
        }
        

        foreach (Transform child in spawnLocation) // Get all spawn locations
        {
            spawnChildren = spawnLocation.GetComponentsInChildren<Transform>().Where(t => t != spawnLocation).ToArray(); 
            // .Where() ensures that the parent object is not included in the array
            //Debug.Log("Spawn location: " + child.name);
        }
        enemy = Resources.Load<GameObject>("Prefabs/Evil Bun");
        
        StartCoroutine(Loop()); // 1. Initiate loop that continuously spawns enemies every x seconds
    }

    public void updateHpBar()
    {
        towerHpBar.value = towerHealth;
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

        attachNametag(spawnedEnemy, enemyScript);
        td_question.text = "Category: \n" + nametagScript.GetWinningSO().question;
    }
    
    
    
    void attachNametag(GameObject enemy, Enemy_TD es)
    {
        TextMeshPro nametag = enemy.GetComponentInChildren<TextMeshPro>();

        if (nametag == null)
        {
            Debug.LogError(enemy.name + ": Nametag not loaded.");
            return;
        }

        if (nametagScript == null)
        {
            Debug.LogError("NametagManager not loaded.");
            return;
        }

        string[] correctAnswers = nametagScript.GetWinningSO().correctAnswers;
        string[] wrongAnswers = nametagScript.GetWinningSO().wrongAnswers;

        string[] answers = correctAnswers.Concat(wrongAnswers).ToArray();

        if (answers.Length == 0)
        {
            Debug.LogError("No answers available.");
            return;
        }

        // Clears the list if everything has already been selected
        if (selectedIndexes.Count >= answers.Length)
        {
            selectedIndexes.Clear();
            Debug.Log("All nametag answers used. Resetting selected indexes.");
        }

        int winningIndex;

        do
        {
            winningIndex = Random.Range(0, answers.Length);
        }
        while (selectedIndexes.Contains(winningIndex));

        // Assign nametag
        nametag.text = answers[winningIndex];

        // Determine if the answer came from wrongAnswers
        if (winningIndex >= correctAnswers.Length)
        {
            es.isEvil = true;

            Debug.Log($"{enemy.name} (aka {answers[winningIndex]}) is EVIL!");

            //nametag.text = "EVIL: " + nametag.text;
        }
        else
        {
            es.isEvil = false;

            Debug.Log($"{enemy.name} (aka {answers[winningIndex]}) is an ally");
        }

        // Remember this answer
        selectedIndexes.Add(winningIndex);

    }

    public float takeDamage() // this is for the Tower object, not an individual enemy!
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

        updateHpBar();
    }

    IEnumerator Loop() 
    {
        while (isOngoing == true && currentWavesLeft > 0)
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
            currentWavesLeft--;
            yield return new WaitForSeconds(enemySpawnRate);
        }
    }

    

}
    