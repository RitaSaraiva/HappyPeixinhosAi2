using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AIUnityExamples.Movement.Core;

public class AIBehaviour : MonoBehaviour
{
    [SerializeField] private float wanderTime;

    private bool hitWall;
    private Vector3 targetEulerAngles;

    // Speed of AI agent movement
    [SerializeField]
    private float movementSpeed;
    
    // Player in sight distance
    [SerializeField] private float fishInSightDistance = 10f;

    // Reference to the agent's rigid body
    private Rigidbody rb;

    [SerializeField] private GameObject fish;
    

    //--------------------------------------------------------------------//

    private void Start() {
        targetEulerAngles = Vector3.zero;
    }
    
    void Awake(){
        
        rb = GetComponent<Rigidbody>();
    }
    
    // Update is called once per frame
    void Update()
    {   
        print($"hitWall? {hitWall}");
        if (wanderTime > 0) {
            if (hitWall) {
                transform.eulerAngles = targetEulerAngles;
                targetEulerAngles = Vector3.zero;
                hitWall = false;
            }
            transform.Translate(Vector3.forward * Time.deltaTime * movementSpeed);
            
            wanderTime -= Time.deltaTime; 
        }
        else {
            wanderTime = Random.Range(3f, 4f);
            Wander ();
        }

        //if (FishInSight()){
        //    SeekAction();
        //    print("im see the bitch");
        //     
        //}
    }


    private void OnTriggerEnter(Collider other) {
        bool wall = other.CompareTag("Wall");
        bool vertLimit = other.CompareTag("VerticalLimits");
        if ((wall || vertLimit) && !hitWall) {
            print($"hit wall {other.name}");
            float eulerX = vertLimit ?
                transform.eulerAngles.x + 180f : transform.eulerAngles.x;
            float eulerY = wall ?
                transform.eulerAngles.y + 180f : transform.eulerAngles.y;
            targetEulerAngles = new Vector3(
                eulerX,
                transform.eulerAngles.y + 135f,
                transform.eulerAngles.z
            );
            hitWall = true;
            wanderTime = 1f;
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Vector3 dir = transform.TransformDirection(Vector3.forward * 200f);
        Gizmos.DrawRay(transform.position, dir);
    }


    void Wander () {
        transform.eulerAngles = new Vector3 (
            Random.Range(-20, 20), Random.Range (transform.eulerAngles.y - 20, transform.eulerAngles.y + 20), 0);
    }

    
    // Check if fish is in sight
    private bool FishInSight()
    {
        Vector3 fishPosition = fish.transform.position;
        float distance = (fishPosition - transform.position).magnitude;
        if (distance < fishInSightDistance) return true;
        return false;
    }
    
        // Seek player action
    private void SeekAction()
    {
        // Move towards player
        MoveTowardsTarget(fish.transform.position);
    }

    private void MoveTowardsTarget(Vector3 targetPos)
    {
        // Determine velocity to the target
        Vector3 vel = (targetPos - transform.position).normalized * movementSpeed;

        // Move towards the target  at the calculated velocity
        rb.MovePosition(transform.position + vel);
        print("im movingbitch");

    }
}


