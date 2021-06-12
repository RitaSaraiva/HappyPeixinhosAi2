using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBehaviour : MonoBehaviour
{
    [SerializeField] private float wanderTime;
    // Reference to gamearea object
    [SerializeField] private GameArea gameArea;
    // Paddings on area limits
    [SerializeField] private float xLimitPadding;
    [SerializeField] private float yLimitPadding;
    [SerializeField] private float zLimitPadding;
    [SerializeField] private Vector3 targetPos;

    // Speed of AI agent movement
    [SerializeField] private float movementSpeed;
    
    // Player in sight distance
    [SerializeField] private float fishInSightDistance = 10f;

    [SerializeField] private GameObject fish;

    // Reference to the agent's rigid body
    private Rigidbody rb;
    private bool hitWall;
    private Vector3 targetEulerAngles;
    private LayerMask targetsMask;
    [SerializeField] private GameObject targetFish;
    [SerializeField] private BasicFish fishScript;

    private SphereCollider sphereCol;
    
    private List<(GameObject tgt, float dist)> mediumFishTargets =
        new List<(GameObject, float)>();
    private List<(GameObject tgt, float dist)> smallFishTargets =
        new List<(GameObject, float)>();
    private List<(GameObject tgt, float dist)> algaeTargets =
        new List<(GameObject, float)>();
    
    //--------------------------------------------------------------------//

    private void Start() {
        targetEulerAngles = Vector3.zero;
        sphereCol.radius = fishInSightDistance;
    }
    
    void Awake() {
        rb = GetComponent<Rigidbody>();
        fishScript = GetComponent<BasicFish>();
        gameArea = FindObjectOfType<GameArea>();
    }
    
    // Update is called once per frame
    void Update()
    {
        // ------------ WANDER (NEW - EXPERIMENTING)

        //if (targetPos == Vector3.zero){
        //    print("fds para lá bro");
        //    targetPos = WanderTargetPosition();
        //}
        //else{
        //    RotateNPC(targetPos, movementSpeed * Time.deltaTime);
        //    transform.position = Vector3.MoveTowards(transform.position, targetPos, movementSpeed * Time.deltaTime);
        //    if (Vector3.Distance(transform.position, targetPos) < 2.5f)
        //        targetPos = Vector3.zero;
        //}

        // ------ END WANDER (NEW - EXPERIMENTING)

        // ------------ WANDER (OLD - WORKING)

        //print($"hitWall? {hitWall}");
        //if (wanderTime > 0) {
        //    if (hitWall) {
        //        transform.eulerAngles = targetEulerAngles;
        //        targetEulerAngles = Vector3.zero;
        //        hitWall = false;
        //    }
        //    transform.Translate(Vector3.forward * Time.deltaTime * movementSpeed);
        //    
        //    wanderTime -= Time.deltaTime; 
        //}
        //else {
        //    wanderTime = Random.Range(3f, 4f);
        //    Wander ();
        //}

        // ------ END WANDER (OLD - WORKING)

        // ------------ SEEK PURSUE

        //if (FishInSight()){
        //    SeekPersueAction();
        //    print("im see the bitch");
        //     
        //}

        // ------ END SEEK PURSUE

        // ------------ SEEK FLEE

        //if (FishInSight ()) {
        //    SeekFleeAction();
        //}

        // ------ END SEEK FLEE

        print($"{FishInSight()}");
    }

    private Vector3 WanderTargetPosition(){
        float targetX = Random.Range(gameArea.MinVec.x + xLimitPadding,
            gameArea.MaxVec.x - xLimitPadding);
        float targetY = Random.Range(gameArea.MinVec.y + yLimitPadding,
            gameArea.MaxVec.y - yLimitPadding);
        float targetZ = Random.Range(gameArea.MinVec.z + zLimitPadding,
            gameArea.MaxVec.z - zLimitPadding);

        return new Vector3(targetX, targetY, targetZ);
    }

    //Rotate the NPC to face new waypoint
    void RotateNPC (Vector3 waypoint, float currentSpeed)
    {
        //get random speed up for the turn
        float TurnSpeed = currentSpeed * Random.Range(1f, 3f);
 
        //get new direction to look at for target
        Vector3 LookAt = waypoint - this.transform.position;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(LookAt), TurnSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("MediumFish")) {
            mediumFishTargets.Add((other.gameObject,
                Vector3.Distance(transform.position, other.transform.position)));
        }
        else if (other.CompareTag("SmallFish")) {
            smallFishTargets.Add((other.gameObject,
                Vector3.Distance(transform.position, other.transform.position)));
        }
        else if (other.CompareTag("Algae")) {
            algaeTargets.Add((other.gameObject,
                Vector3.Distance(transform.position, other.transform.position)));
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Color r = Color.yellow; 
        r.a = 0.4f;
        Vector3 dir = transform.TransformDirection(Vector3.forward * 200f);
        Gizmos.DrawRay(transform.position, dir);

        Gizmos.color = r;
        Gizmos.DrawSphere(transform.position, fishInSightDistance);

        r = Color.red;
        Gizmos.color = r;
        Gizmos.DrawSphere(targetPos, 10f);
    }
    
    // --------------------------WANDER MOVEMENT-------------------------------  //

    void Wander () {
        transform.eulerAngles = new Vector3 (
            Random.Range(-20, 20), Random.Range (transform.eulerAngles.y - 20, transform.eulerAngles.y + 20), 0);
    }
    
    // Check if fish is in sight
    private bool FishInSight()
    {
        if (targetFish == null) {

            (GameObject obj, float dist) closestTgt =
                (this.gameObject, float.PositiveInfinity);

            // if bigfish calls this
            if (this.CompareTag("BigFish")) {
                if (mediumFishTargets.Count > 0) {
                    for (int i = 0; i < mediumFishTargets.Count; i++) {
                        if (mediumFishTargets[i].dist < closestTgt.dist) {
                            closestTgt = mediumFishTargets[i];
                        }
                    }
                }
                else if (smallFishTargets.Count > 0) {
                    for (int i = 0; i < smallFishTargets.Count; i++) {
                        if (smallFishTargets[i].dist < closestTgt.dist) {
                            closestTgt = mediumFishTargets[i];
                        }
                    }
                }
                else return false;
            }
            // if mediumfish calls this
            else if (this.CompareTag("MediumFish")) {
                if (smallFishTargets.Count > 0) {
                    for (int i = 0; i < smallFishTargets.Count; i++) {
                        if (smallFishTargets[i].dist < closestTgt.dist) {
                            closestTgt = mediumFishTargets[i];
                        }
                    }
                }
                else if (algaeTargets.Count > 0) {
                    for (int i = 0; i < algaeTargets.Count; i++) {
                        if (algaeTargets[i].dist < closestTgt.dist) {
                            closestTgt = algaeTargets[i];
                        }
                    }
                }
            }
            // if smallfish calls this
            else if (this.CompareTag("SmallFish")) {
                if (algaeTargets.Count > 0) {
                    for (int i = 0; i < algaeTargets.Count; i++) {
                        if (algaeTargets[i].dist < closestTgt.dist) {
                            closestTgt = algaeTargets[i];
                        }
                    }
                }
            }

            targetFish = closestTgt.obj;
            print($"closestTgt: {closestTgt.obj.name}");
            return true;
        }
        return false;
    }
    
        // Seek player action
    private void SeekPersueAction()
    {
        // Move towards player
        MoveTowardsTarget(fish.transform.position);
    }

    private void SeekFleeAction()
    {
        // Move towards player
        MoveAwayTarget(fish.transform.position);
    }

    // --------------------------PERSUE MOVEMENT-------------------------------  //
    private void MoveTowardsTarget(Vector3 targetPos)
    {
        // Determine velocity to the target
        Vector3 vel = (targetPos - transform.position).normalized * movementSpeed;

        // Move towards the target  at the calculated velocity
        rb.MovePosition(transform.position + vel);
        print("im movingbitch");

    }

    // --------------------------FLEE MOVEMENT-------------------------------  //
    private void MoveAwayTarget(Vector3 targetPos)
    {
        Vector3 dir = transform.position - targetPos;
        
        transform.Translate(dir.normalized * movementSpeed * Time.deltaTime);

    }
}