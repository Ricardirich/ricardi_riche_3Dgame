using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
 //   private CharacterController controller;
    public Transform player;
    private Vector3 targetPoint;
    private Vector3 directionToPlayer;

   // public float rotationSpeed = 50;

    public float viewAngle = 120;
    public float viewRange = 5;
    public float dectectionRadius = 0.5f;

    public LayerMask playerLayer;

    // public float walkSpeed = 4;
    //public float rotatioSpee = 5;

    public bool patrolling = true;
    public Transform[] waypoint;
    public Transform targetWaypoint;
    public int waypointIndex;
    
    private bool playerFound = false;
    public float alertDuration = 5;
    private float timeSincedAlerted = 6;

    private NavMeshAgent agent;
    

    void Start()
    {
       // controller = GetComponent<CharacterController>();
       agent = GetComponent<NavMeshAgent>();    
        
    }

    // Update is called once per frame
    void Update()

    {
        targetWaypoint= waypoint[waypointIndex];

       Debug.DrawRay(transform.position,transform.forward,Color.blue);
        //targetPoint = new Vector3(player.position.x, transform.position.y, player.position.z);
        directionToPlayer = (player.position - transform.position).normalized;
        Quaternion rot = Quaternion.LookRotation(directionToPlayer);
        if (playerDetected())
        {
            patrolling = false;
            agent.SetDestination(player.position);
           // transform.localRotation = Quaternion.RotateTowards(transform.localRotation, rot, agent.angularSpeed* Time.deltaTime);
        }
        if(patrolling)
        {
            patrol();
        }
        if (playerFound && !playerDetected())
        {
            if (timeSincedAlerted < alertDuration)
            {
                timeSincedAlerted += Time.deltaTime;
            }
            else
            {
                playerFound = false;
                timeSincedAlerted = 0;
                patrolling = true;
            }
        }


       // controller.Move(transform.forward * walkSpeed * Time.deltaTime);
       
    }

    private bool playerDetected ()
    { 
       
        bool result = false;
        float angle = Vector3.Angle(transform.forward, directionToPlayer);

        playerFound = true;
        if (angle < viewAngle / 2)
        {
            if (Physics.Raycast(transform.position, directionToPlayer, viewRange, playerLayer)) 
            result = true ;
        }
        float dist= Vector3.Distance(transform.position, player.position);
        if (dist < dectectionRadius)
        {
            result = true ;
        }

        return result;
    }
    private void patrol()
    {
        agent.SetDestination(targetWaypoint.position);
        float dist = Vector3.Distance(transform.position, targetWaypoint.position);
        float buffer = 0.01f;
        if (dist <= buffer)
        {
            waypointIndex++;
            if (waypointIndex >= waypoint.Length)
            {
                waypointIndex = 0;
            }
        }

    }

   private void OnTriggerEnter(Collider other)
    {
       if (other.CompareTag("Player"))
       {
           FPC playerScript = other.GetComponent<FPC>();
           if (playerScript != null)
           {
             playerScript.DisableMovement();
           }
      }
   }
}

