using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Interactable))]
[RequireComponent(typeof(Inventory))]
[RequireComponent(typeof(CharacterHealth))]

public class AiNavigationScript : MonoBehaviour
{
    [SerializeField] Transform playerCapsule; //Add player child object named "Capsule and Movement"
    private NavMeshAgent agent;
    private AIState currentState;
    private int currentIndex = 0;
    private bool isIdle = false;
    private bool isShooting = false;
    private string parentName; //To delete only used for debug.

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
        float waitTime = Random.Range(4f, 15f); // Generate a random wait time between 2 to 8 seconds
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
        Debug.Log("Attempting to TriggerAttackState");

        if (currentState != AIState.Attack) // Only trigger attack if not already attacking
        {
            Debug.Log("Attack state is not active. Setting state to Attack.");
            currentState = AIState.Attack;
            target = attacker;
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
                agent.isStopped = true; // Stop the agent from moving
                AttackTarget();
            }
            else
            {
                Debug.LogWarning("Agent is not on NavMesh!");
                // Attempt to place the agent on the NavMesh
                NavMeshHit hit;
                if (NavMesh.SamplePosition(transform.position, out hit, maxDistance, NavMesh.AllAreas))
                {
                    transform.position = hit.position;
                    agent.Warp(hit.position); // Warp agent to position on the NavMesh
                    agent.isStopped = true;
                    AttackTarget();
                }
            }
        }
    }


    void AttackTarget()
    {
        Debug.Log("AttackTarget method called.");

        if (!isShooting)
        {
            Debug.Log("AI is not currently shooting. Preparing to shoot.");
            isShooting = true; // Set this early to prevent multiple shots in one frame
            Vector3 directionToTarget = (target.transform.position - transform.position).normalized;
            GameObject bullet = Instantiate(bulletPrefab, weaponFirePoint.transform.position, Quaternion.LookRotation(directionToTarget));
            Bullet bulletComponent = bullet.GetComponent<Bullet>();

            if (bulletComponent == null)
            {
                Debug.LogError("Bullet component not found on the instantiated bullet object.");
                return;
            }

            bulletComponent.damage = this.damage;
            bulletComponent.speed = this.speed;
            bulletComponent.attacker = this.gameObject;
            bulletComponent.SetTarget(directionToTarget);

            Debug.Log("Bullet instantiated and set up. Starting coroutine to manage shooting interval.");
            StartCoroutine(ShootAtPlayer());
        }
        else
        {
            Debug.Log("AI is already shooting.");
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
        yield return new WaitForSeconds(1f); // Simulate delay for shooting interval
        isShooting = false;
        Debug.Log("AI can shoot again.");
    }
}
