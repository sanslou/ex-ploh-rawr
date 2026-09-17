using UnityEngine;

public class SlashAttack : MonoBehaviour
{
    // This script is attached to the SlashAttack prefab, which is spawned when the player slashes an enemy. It detects collisions with enemies and destroys them.

    public bool hit = false;
    void Start()
    {
        hit = false;
    }
    void OnTriggerEnter (Collider other)
    {
        if (other.CompareTag("Enemy_TD") && !hit)
        {
            Debug.Log("SlashAttack hit an Enemy_TD! " + other.name);
            Destroy(other.gameObject);
            hit = true;
        }
    }

}
