using UnityEngine;
using UnityEngine.AI;

public class Enemy_TD : MonoBehaviour
{
    // The TD stands for "Tower Defense"

    [Header("Character references")]
    NavMeshAgent agent;
    public Transform target;
    GameObject tower;
    
    [Header("Enemy Properties")]
    float health = 25;

    void Start()
    {
        //fetch preferences
        target = GetComponent<Transform>();
        agent = GetComponent<NavMeshAgent>();

        tower = GameObject.FindWithTag("Tower");
        target = tower.transform;
        changeTarget(target);

        if (NavMesh.SamplePosition(transform.position, out NavMeshHit hit, 5.0f, NavMesh.AllAreas))
        {
            agent.Warp(hit.position); // Safely snaps the enemy right onto the floor
        }
        else
        {
            Debug.LogError($"{gameObject.name} is too far away from a baked NavMesh!");
        }

        // Debug.Log("Target: " + target); Debug.Log("Agent: " + agent);
    }

    void Update()
    {
        if (target != null && agent.isOnNavMesh) // Ensure there is a target
        {
            moveToTarget(target); // Continuously tell enemy to move here
        }
    }

    public void moveToTarget(Transform t) { agent.SetDestination(t.position); }      // Move enemy to a target. Requires instantaneous calling.
    public void changeTarget(Transform t) { target = t; }     // Change target
    public virtual void Interact() { }
}
