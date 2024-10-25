using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Interactable))]
[RequireComponent(typeof(Inventory))]
[RequireComponent(typeof(CharacterHealth))]
[RequireComponent(typeof(Rigidbody))]

public class AiNavigationScript : MonoBehaviour
{
    [SerializeField] Transform playerCapsule; //Add player child object named "Capsule and Movement"
    private NavMeshAgent agent;
    private AIState currentState;
    private int currentIndex = 0;
    private bool isIdle = false;
    private bool isShooting = false;

    private int maxDistance = 100;

    private GameObject target;

    public PickableObject weapon;

    [Header("Weapon Specifics")]
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform weaponFirePoint;

    [Header("Weapon Stats")]
    public int damage;
    public int speed;

    [System.Serializable]
    public class AIStatePosition
    {
        public AIState state;
        public Transform position;
    }

    // List to hold AI state and corresponding positions
    public List<AIStatePosition> statePositions = new List<AIStatePosition>(); //For Now only use Idle and Patrol in this list

    //AiState it can be in. Feel free to add more if needed.
    public enum AIState
    {
        Idle,
        Patrol,
        Chase,
        Attack,
        Flee
    }

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        currentState = statePositions[currentIndex].state;
        agent.destination = statePositions[currentIndex].position.position;
        damage = (int)weapon.damage;
        speed = weapon.attackSpeed;
        GetComponent<Rigidbody>().isKinematic = true; //Just so if they die they fall on the ground correctly
    }

    void Update()
    {
        switch (currentState)
        {
            case AIState.Idle:
                //Goes to the next State in the list after reaching it's destination and being idle from 2 to 10 seconds
                if (!agent.pathPending && agent.remainingDistance < 0.1f)
                {
                    if(isIdle == false) 
                    { 
                        isIdle = true;
                        StartCoroutine(WaitRandomSeconds()); 
                    }
                    
                }
                break;
            case AIState.Patrol:
                //Goes to the next State in the list after reaching it's destination
                if (!agent.pathPending && agent.remainingDistance < 0.1f)
                {
                    SetNextState();
                }
                break;
            case AIState.Chase:
                agent.destination = playerCapsule.position;
                break;
            case AIState.Attack:
                if (target != null) // Continuously check if the target is still valid
                {
                    AttackTarget();
                    Debug.Log(": Attacking");
                }
                else
                {
                    SetNextState(); // If for some reason the target is null, transition to the next state
                }
                break;
            case AIState.Flee:
                Debug.Log(": Fleeing");
                break;
            default:
                break;
        }
    }

    // Function to set the next state in the list
    void SetNextState()
    {
        currentIndex = (currentIndex + 1) % statePositions.Count;
        currentState = statePositions[currentIndex].state;
        agent.destination = statePositions[currentIndex].position.position;
    }

    IEnumerator WaitRandomSeconds()
    {
        float waitTime = Random.Range(4f, 15f); // Generate a random wait time between 4 to 15 seconds
        yield return new WaitForSeconds(waitTime);

        // Code to execute after waiting
        Debug.Log(": Waited for " + waitTime + " seconds.");

        //Change AIState after waiting
        if(currentState != AIState.Attack)
        {
            SetNextState();
            isIdle = false;
        }
    }

    public void TriggerAttackState(GameObject attacker)
    {
        Debug.Log("Triggering attack state");

        if (currentState != AIState.Attack) // Only trigger attack if not already attacking
        {
            Debug.Log("Setting AI state to Attack.");
            currentState = AIState.Attack;
            target = attacker; // Assign the attacker as the target
            StartAttack(); // Immediate reaction to state change
        }
        else
        {
            Debug.Log("Attack state is already active.");
        }
    }


    void StartAttack()
    {
        if (target != null && agent.isActiveAndEnabled && gameObject.activeInHierarchy)
        {
            if (agent.isOnNavMesh) // Ensure the agent is on the NavMesh
            {
                agent.isStopped = true; // Stop the agent from moving need to change this later so they move while attacking
                AttackTarget();
            }
            else
            {
                Debug.LogWarning("Agent is not on NavMesh!");
                PlaceOnNavMeshAndAttack();
            }
        }
    }


    void AttackTarget()
    {
        if (target != null && !isShooting)
        {
            Debug.Log("AI is preparing to shoot at the target.");
            isShooting = true;

            // Aim and shoot towards the target
            Vector3 directionToTarget = (target.transform.position - transform.position).normalized;
            GameObject bullet = Instantiate(bulletPrefab, weaponFirePoint.position, Quaternion.LookRotation(directionToTarget));
            Bullet bulletComponent = bullet.GetComponent<Bullet>();

            if (bulletComponent != null)
            {
                bulletComponent.damage = damage;
                bulletComponent.speed = speed;
                bulletComponent.attacker = gameObject; // Set AI as the attacker
                bulletComponent.SetTarget(target.transform.position); // Set target position
                StartCoroutine(ShootAtPlayer()); // Control shooting interval
            }
        }
    }


    void TriggerChaseState(GameObject chase)
    {
        currentState = AIState.Chase;
        agent.destination = chase.transform.position;
    }

    private IEnumerator ShootAtPlayer()
    {
        Debug.Log("ShootAtPlayer coroutine started.");
        yield return new WaitForSeconds(1f); // Delay for shooting interval
        isShooting = false; // Reset here to allow future shots
        Debug.Log("AI can shoot again.");
    }

    private void PlaceOnNavMeshAndAttack()
    {
        NavMeshHit hit;
        if (NavMesh.SamplePosition(transform.position, out hit, maxDistance, NavMesh.AllAreas))
        {
            transform.position = hit.position;
            agent.Warp(hit.position);
            agent.isStopped = false;
            agent.destination = target.transform.position;
        }
    }
}
