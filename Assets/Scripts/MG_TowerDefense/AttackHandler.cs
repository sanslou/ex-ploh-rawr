using UnityEngine;
using UnityEngine.UI;

public class AttackHandler : MonoBehaviour
{
    // This script is attached to the player and handles the attack logic. It enables the attack button when the tower defense minigame is ongoing,
    // and calls the slash() method when the button is pressed.

    [Header("References")]
    [SerializeField] private Transform playerChest;
    [SerializeField] private Camera mainCamera;
    private Tower towerScript;
    private Enemy_TD enemyScript;
    private Button buttonAttack;

    void Start()
    {
        buttonAttack = GameObject.FindGameObjectWithTag("UI_Attack").GetComponent<Button>();
    }

    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy_TD") && towerScript.isOngoing)
        {
            enemyScript = other.GetComponent<Enemy_TD>();
            buttonAttack.interactable = true;
            buttonAttack.onClick.AddListener(slash);
            Debug.Log("ONTRIGGERENTER: Tower Defense minigame is ongoing. Attack button enabled.");

        }
    }
    

    public void slash()  // Player slashes the NPC, summons VFX and SFX, and destroys the NPC
    {
        // TODO: Health system for enemies - FIN
        // TODO: Health system for tower - FIN
        // TODO: Health system for players.
        // TODO: Knockback physics effect
        // TODO: Interact button sometimes does not engage

        //Debug.Log($"Player slashed {gameObject.name} (ID: {enemyID})!");
        GameObject vfx =
            Resources.Load<GameObject>("Prefabs/VFX_DefaultSlash");

        GameObject sfx =
            Resources.Load<GameObject>("Prefabs/SFX_Hit");

        playerChest = GameObject.FindWithTag("Player").transform.Find("Player Sprite");
        mainCamera = Camera.main;

        // Spawn VFX and SFX
        GameObject spawnedVFX = Instantiate(
            vfx,
            playerChest.position + Vector3.up * 1.0f,
            Quaternion.identity
        );


        GameObject spawnedSFX = Instantiate(
            sfx,
            playerChest.position,
            Quaternion.identity
        );


        // Make VFX face the camera
        spawnedVFX.transform.forward =
            mainCamera.transform.forward;


        // Destroy after playing
        Destroy(spawnedVFX, 0.12f);
        Destroy(spawnedSFX, 2f);


        // Temporary placeholder

    }
}
