using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.UI;


public class Enemy_TD : NPC
{
    // Note: This class is responsible for an individual enemy's AI. TD stands for "Tower Defense"

    // Fetch References
    NavMeshAgent agent;
    public Transform target;
    public GameObject tower;
    public string npcName = "EvilBun";
    public int enemyID = 0 ;// Helps slash() identify which NPC to attack

    [Header("Enemy Properties")]
    public float health = 25;

    [Header("References")]
    [SerializeField] private Transform playerChest;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Tower towerScript;
    [SerializeField] private Enemy_TD enemyScript;
    private Button buttonAttack;

    private Coroutine tickCoroutine;
    public UnityAction slashAction;
    


    new void Start()
    {
        // Fetch references
        target = GetComponent<Transform>();
        agent = GetComponent<NavMeshAgent>();
        buttonAttack = GameObject.FindGameObjectWithTag("UI_Attack").GetComponent<Button>();

        /*
        slashAction = new UnityAction(slash);
        slashAction = slash;
        slashAction?.Invoke();
       */

        // WIP

        tower = GameObject.FindWithTag("Tower");

        if (tower != null)
        {
            target = tower.transform;
            towerScript = tower.GetComponent<Tower>();
        }
        else
        {
            Debug.LogError("Tower could not be found!");
            return;
        }

        changeTarget(target);       // 1. Identify the target for the enemy to move towards
        snapToNavMesh();            // 2. Snap the enemy to the NavMesh after it has been spawned
        moveToTarget(target);       // 3. Move the enemy to the target after it has been identified and snapped to the NavMesh
    }


    new void Update()
    {
        base.Update();
    }

    void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Tower"))
            return;

        if (tickCoroutine == null) // Take tower damage every x seconds if it detects collider attached with a tag "Enemy_TD"
        {
            tickCoroutine = StartCoroutine(TickRoutine());
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Tower"))
        {
            if (tickCoroutine != null)
            {
                StopCoroutine(tickCoroutine);
                tickCoroutine = null;
            }
        }
    }

    public void snapToNavMesh()
    {
        if (NavMesh.SamplePosition(
            transform.position,
            out NavMeshHit hit,
            5.0f,
            NavMesh.AllAreas))
        {
            agent.Warp(hit.position); // Suppose the enemy has already spawned, snap it to the nearest point on the NavMesh so it can move
        }
        else
        {
            Debug.LogError(
                $"{gameObject.name} is too far away from a baked NavMesh! " +
                "(Did you bake a NavMesh yet?)"
            );
        }
    }

    public void moveToTarget(Transform t)
    {
        if (target != null && agent.isOnNavMesh)
        {
            agent.SetDestination(t.position);
        }
        else if (target == null)
        {
            Debug.LogError(
                $"{gameObject.name} has no target to move to!"
            );
        }
        else if (!agent.isOnNavMesh)
        {
            Debug.LogError(
                $"{gameObject.name} is not on a NavMesh! " +
                "(Did you bake a NavMesh yet?)"
            );
        }
    }

    public void changeTarget(Transform t)
    {
        target = t; // Change target
    }


   


    IEnumerator TickRoutine()
    {
        while (towerScript != null && towerScript.isOngoing)
        {
            float damage = towerScript.takeDamage();

            Debug.Log($"Damage! {damage}-hp");

            towerScript.towerHealth -= damage;

            yield return new WaitForSeconds(towerScript.enemyDamageTickRate); // Parameter is AKA enemy attack speed.
        }

        tickCoroutine = null;
    }
}