using System;
using UnityEngine;

public class NPC_EvilBun : NPC
{

    string npcName = "EvilBun";
    public int enemyID = 0; // Helps slash() identify which NPC to attack
    [SerializeField] private Transform playerChest;
    [SerializeField] private Camera mainCamera;

    new void Start() { 
    
    }
    new void Update()
    {
        base.Update();
    }

    // This function is called when a player interacted this NPC

    public override void Interact()
    {
        playerChest = GameObject.FindWithTag("Player").transform.Find("Player Sprite");
        mainCamera = Camera.main;
        slash();

    }

    public void slash() // Attacks the enemy NPC
        {

        // TODO: Health system for the enemies and the player
        // TODO: Health system for the tower
        // TODO: Knockback physics effect for hit enemy
        // TODO: The interact button sometimes does not engage. It is inconsistent.

        GameObject vfx = Resources.Load<GameObject>("Prefabs/VFX_DefaultSlash"); // Load the prefab VFX default slash
        GameObject sfx = Resources.Load<GameObject>("Prefabs/SFX_Hit"); // Load this for sound effects

        // Spawn the VFX and SFX (audio plays on awake) at the same time
        GameObject spawnedVFX = Instantiate(
            vfx,
            playerChest.position + Vector3.up * 1.0f, // Make the slash VFX play above the player model
            Quaternion.identity
        );

        GameObject spawnedSFX = Instantiate(
            sfx,
            playerChest.position,
            Quaternion.identity);

        // Make the VFX face the camera
        spawnedVFX.transform.forward = mainCamera.transform.forward;

        // Destroys them after a certain amount of time. SpawnedSFX takes more than usual to play the entire sound.
        Destroy(spawnedVFX, (float).12);
        Destroy(spawnedSFX, 2f);

        //Debug.Log("Hit: " + "EvilBun_" + enemyID);

        //Destroys the player model itself. Temporary placeholder for the health system.
        Destroy(GameObject.Find("EvilBun_" + enemyID)); 
    }


}
