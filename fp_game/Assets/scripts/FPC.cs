using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class FPC : MonoBehaviour
{

    private CharacterController characterController;
    public float walkSpeed = 5;
    public float sprintingSpeed = 200;

    public float mouseSensitivity = 5;

    float verticalRotation;

    public float upDownangle = 80;

    public float pickUpRange = 2;

    public Transform holdPoint;

    private Vector3 currentMovement;

    private Vector3 hitPoint;
   // public ParticleSystem impactPS;
    /// <summary>
    /// [Range(10, 30)] public int particleCount = 20;
    /// </summary>

    public float throwForce = 5;
    private float gravity = 9.81f;

   private Camera cam;
   private Item heldItem;
   public float jumpForce = 5; 

    private bool canMove = true;
    void Start()
    {

        characterController = GetComponent<CharacterController>();
        cam = Camera.main;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            Movement();
            MouseLook();
            Jumping();
        }
        
        if (heldItem != null)
        {
            if (Input.GetMouseButtonDown(1))
            {
                heldItem.Throw(throwForce, cam.transform.forward);
                heldItem = null;
            }
        }
        

        if (ObjectInFocus() != null)
        {
            float distanceToObject= Vector3.Distance(cam.transform.position, ObjectInFocus().transform.position);   
            if(Input.GetMouseButtonDown(0))
            {
               // impactPS.transform.position = hitPoint;
               // impactPS.Emit(particleCount);
            }
        if (distanceToObject <= pickUpRange && ObjectInFocus().GetComponent<Item>() != null) 
            {
                heldItem = ObjectInFocus().GetComponent<Item>();
                heldItem.PickUp(cam.transform, holdPoint.position);
            }
        }
        
    }

    void Movement()
    {
        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? sprintingSpeed: walkSpeed;

        float vertInput = Input.GetAxis("Vertical");
        float horInput = Input.GetAxis("Horizontal");
        float vertSpeed = vertInput * currentSpeed;
        float horSpeed = horInput * currentSpeed ;

        Vector3 horizontalMovement = new Vector3(horSpeed, 0, vertSpeed);
        horizontalMovement = transform.rotation * horizontalMovement;
        currentMovement.x = horizontalMovement.x;
        currentMovement.z = horizontalMovement.z;
        characterController.Move(currentMovement * Time.deltaTime);
    }

    void MouseLook()
    {
        float mouseXRotation = Input.GetAxis("Mouse X") * mouseSensitivity;
        transform.Rotate(0, mouseXRotation, 0);

        verticalRotation -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        verticalRotation  = Mathf.Clamp(verticalRotation, -upDownangle, upDownangle);
        cam.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
    }

    void Jumping ()
    {
        if ( characterController.isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                currentMovement.y = jumpForce;
            }

        }
        else
        {
            currentMovement.y -= gravity * Time.deltaTime;
        }
    }

    public GameObject ObjectInFocus()
    {
        GameObject Result = null;
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit)) 
        {
            Result = hit.transform.gameObject;
            hitPoint= hit.point;
        }
        return Result;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
       if(hit.gameObject.CompareTag("Enemy"))
        {
            DisableMovement();
        }
    }

    public void DisableMovement()
    {
        canMove = false; 
        currentMovement = Vector3.zero;
    }
}
