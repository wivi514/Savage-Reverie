using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]

public class AiNavigationScript : MonoBehaviour
{
    [SerializeField] Transform playerCapsule; //Add player child object named "Capsule and Movement"
    private NavMeshAgent agent;
    private AIState currentState;
    private int currentIndex = 0;
    private bool isIdle = false;
    private string parentName; //To delete

    private GameObject target;

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
        parentName = transform.parent.name; // To delete
        currentState = statePositions[currentIndex].state;
        agent.destination = statePositions[currentIndex].position.position;
    }

    void Update()
    {
        switch (currentState)
        {
            case AIState.Idle:
                Debug.Log(parentName + ": Idle");
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
                Debug.Log(parentName + ": Patrolling");
                //Goes to the next State in the list after reaching it's destination
                if (!agent.pathPending && agent.remainingDistance < 0.1f)
                {
                    SetNextState();
                }
                break;
            case AIState.Chase:
                Debug.Log(parentName + ": Chasing");
                agent.destination = playerCapsule.position;
                break;
            case AIState.Attack:
                //AttackTarget();
                Debug.Log(parentName + ": Attacking");
                break;
            case AIState.Flee:
                Debug.Log(parentName + ": Fleeing");
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
        Debug.Log(parentName + ": Waited for " + waitTime + " seconds.");

        //Change AIState after waiting
        SetNextState();
        isIdle = false;
    }

    public void TriggerAttackState(GameObject attacker)
    {
        currentState = AIState.Attack;
        target = attacker;
        // Transition to attack logic, e.g., move towards attacker
    }

    //void AttackTarget()
    //{
    //    if (target != null)
    //    {
    //        Vector3 directionToTarget = (target.transform.position - transform.position).normalized;
    //        GameObject bullet = Instantiate(bulletPrefab, weaponFirePoint.position, Quaternion.LookRotation(directionToTarget));
    //        Bullet bulletComponent = bullet.GetComponent<Bullet>();
    //        bulletComponent.SetDirection(directionToTarget);
    //    }
    //}

    void TriggerChaseState(GameObject chase)
    {
        currentState = AIState.Chase;
        agent.destination = chase.transform.position;
    }
}
