using UnityEngine;

public class SlashAttack : MonoBehaviour
{
    // This script is attached to the SlashAttack prefab, which is spawned when the player slashes an enemy. It detects collisions with enemies and destroys them.

    private bool hit;
    private Enemy_TD enemyScript;
    private GameObject target;
    
    void Start()
    {
        hit = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy_TD"))
        {
            if (hit) { return; }
            Debug.Log("SlashAttack hit an Enemy_TD! " + other.name);
            target = other.gameObject;
            enemyScript = target.GetComponent<Enemy_TD>();

            enemyScript.takeDamage(); // Call the TakeDamage method on the Enemy_TD script
            hit = true;

            if (!enemyScript.isEvil)
            {
                Debug.LogWarning("You've hit an ALLY! OUCH!");
            }
            else
            {
                Debug.Log("You've hit an enemy.");
            }
        }
    }


}
