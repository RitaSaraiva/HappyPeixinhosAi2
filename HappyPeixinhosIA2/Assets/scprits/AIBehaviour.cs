using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AIUnityExamples.Movement.Core;

public class AIBehaviour : MonoBehaviour
{
    //[SerializeField] private GameArea gameArea;
    //[SerializeField] private float maxWallDist;
    
    [SerializeField] private float wandertime;
    [SerializeField] private float movementSpeed;

    // Player in sight distance
    [SerializeField]
    private float playerInSightDistance = 10f;

    // Reference to the agent's rigid body
    private Rigidbody rb;

    [SerializeField] private GameObject fish;
    

    //--------------------------------------------------------------------//

    void Awake(){
        
        rb = GetComponent<Rigidbody>();
        fish = GameObject.Find("tunafish");
    }
    
    // Update is called once per frame
    void Update()
    {   
        //if (wandertime > 0){
        //    transform.Translate(Vector3.forward * Time.deltaTime * movementSpeed );
        //    wandertime -= Time.deltaTime; 
        //}
        //    
        //else {
        //    wandertime = Random.Range(5.0f, 5.0f);
        //    Wander ();
        //}

        if (FishInSight()){
            SeekAction(); 
        }
    }

    void Wander (){
        transform.eulerAngles = new Vector3 (0, Random.Range (0,360), 0);
    }

    // Check if fish is in sight
    private bool FishInSight()
    {
        Vector3 fishPosition = fish.transform.position;
        float distance = (fishPosition - transform.position).magnitude;
        if (distance < playerInSightDistance) return true;
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

    }
}


