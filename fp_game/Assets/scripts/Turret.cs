using UnityEngine;

public class Turret : MonoBehaviour
{
    private CharacterController controller;
    public Transform player;
    private Vector3 targetPoint;
    private Vector3 directionToPlayer;

    public float rotationSpeed = 50;

    public float viewAngle = 120;
    public float viewRange = 5;
    public float dectectionRadius = 0.5f;

    public LayerMask playerLayer;

    

   

    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()

    {
        Debug.DrawRay(transform.position, transform.forward, Color.blue);
        targetPoint = new Vector3(player.position.x, transform.position.y, player.position.z);
        directionToPlayer = (player.position - transform.position).normalized;
        Quaternion rot = Quaternion.LookRotation(directionToPlayer);
        if (playerDetected())
        {
            transform.localRotation = Quaternion.RotateTowards(transform.localRotation, rot, rotationSpeed * Time.deltaTime);

            
        }

         
        // Quaternion.RotateTowards(transform.rotation,)
    }

    private bool playerDetected()
    {
        
        bool result = false;
        float angle = Vector3.Angle(transform.forward, directionToPlayer);
        if (angle < viewAngle / 2)
        {
            if (Physics.Raycast(transform.position, directionToPlayer, viewRange, playerLayer)) 
            result = true;
        }
        float dist = Vector3.Distance(transform.position, player.position);
        if (dist < dectectionRadius)
        {
            result = true;
        }

        return result;
    }
    
}
