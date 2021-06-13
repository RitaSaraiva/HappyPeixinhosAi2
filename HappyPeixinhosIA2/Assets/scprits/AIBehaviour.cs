using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBehaviour : MonoBehaviour
{
<<<<<<< HEAD
    // Reference to gamearea object
    [SerializeField] private GameArea gameArea;
    // Paddings on area limits
    [SerializeField] private Vector3 LimitPaddings;
=======
    [SerializeField] private float wanderTime;
    // Reference to gamearea object
    [SerializeField] private GameArea gameArea;
    // Paddings on area limits
    [SerializeField] private float xLimitPadding;
    [SerializeField] private float yLimitPadding;
    [SerializeField] private float zLimitPadding;
>>>>>>> 1693c6a32c26015ac7f769788a7070a3c55fea52
    [SerializeField] private Vector3 targetPos;

    // Speed of AI agent movement
    [SerializeField] private float movementSpeed;
    
    // Player in sight distance
    [SerializeField] private float fishInSightDistance = 10f;
    [SerializeField] private GameObject currentTarget;
    [SerializeField] private GameObject currentDanger;
    [SerializeField] private BasicFish fishScript;

    [SerializeField] private GameObject fish;

    // Reference to the agent's rigid body
    private Rigidbody rb;
    private bool hitWall;
<<<<<<< HEAD

    
    private List<GameObject> bigFishNearby = new List<GameObject>();
    private List<GameObject> mediumFishNearby = new List<GameObject>();
    private List<GameObject> smallFishNearby = new List<GameObject>();
    private List<GameObject> algaeNearby = new List<GameObject>();
    
    //--------------------------------------------------------------------//

    private void Start() {

=======
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
>>>>>>> 1693c6a32c26015ac7f769788a7070a3c55fea52
    }
    
    void Awake() {
        rb = GetComponent<Rigidbody>();
        fishScript = GetComponent<BasicFish>();
        gameArea = FindObjectOfType<GameArea>();
    }
    
    // Update is called once per frame
    void Update()
    {
<<<<<<< HEAD
        //print($"big fish nearby: {bigFishNearby.Count}\tmedium nearby: {mediumFishNearby.Count}\tsmall fish nearby: {smallFishNearby.Count}\talgae nearby: {algaeNearby.Count}");
        //if (currentTarget != null)
        //    print($"closestTgt: {currentTarget.name}");
        //else print("no target");
        //if (currentDanger != null)
        //    print($"closest danger: {currentDanger.name}");
        //else print("no danger");
        //print(DangerInSight());
        //print($"{FishInSight()}");

=======
>>>>>>> 1693c6a32c26015ac7f769788a7070a3c55fea52
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

<<<<<<< HEAD
        // ------------ WANDER (OLD - "WORKING")
=======
        // ------------ WANDER (OLD - WORKING)
>>>>>>> 1693c6a32c26015ac7f769788a7070a3c55fea52

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

<<<<<<< HEAD
        // ------ END WANDER (OLD - "WORKING")

        // ------------ SEEK PURSUE

        if (currentTarget != null || TargetInSight()){
            SeekPersueAction();
            //print("im see the bitch");
        }
=======
        // ------ END WANDER (OLD - WORKING)

        // ------------ SEEK PURSUE

        //if (FishInSight()){
        //    SeekPersueAction();
        //    print("im see the bitch");
        //     
        //}
>>>>>>> 1693c6a32c26015ac7f769788a7070a3c55fea52

        // ------ END SEEK PURSUE

        // ------------ SEEK FLEE

<<<<<<< HEAD
        if (currentDanger != null || DangerInSight()) {
            SeekFleeAction();
        }

        // ------ END SEEK FLEE
    }

    private Vector3 WanderTargetPosition(){
        float targetX = Random.Range(gameArea.MinVec.x + LimitPaddings.x,
            gameArea.MaxVec.x - LimitPaddings.x);
        float targetY = Random.Range(gameArea.MinVec.y + LimitPaddings.y,
            gameArea.MaxVec.y - LimitPaddings.y);
        float targetZ = Random.Range(gameArea.MinVec.z + LimitPaddings.z,
            gameArea.MaxVec.z - LimitPaddings.z);
=======
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
>>>>>>> 1693c6a32c26015ac7f769788a7070a3c55fea52

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
<<<<<<< HEAD
        if (other.CompareTag("BigFish")) {
            bigFishNearby.Add(other.gameObject);
        }
        else if (other.CompareTag("MediumFish")) {
            mediumFishNearby.Add(other.gameObject);
        }
        else if (other.CompareTag("SmallFish")) {
            smallFishNearby.Add(other.gameObject);
        }
        else if (other.CompareTag("Algae")) {
            algaeNearby.Add(other.gameObject);
        }
        else if (other.CompareTag("Wall")) {
            print("fek, wall");
            hitWall = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject == currentTarget) {
            currentTarget = null;
        }
        if (other.gameObject == currentDanger) {
            currentDanger = null;
        }
        if (other.CompareTag("BigFish")) {
            bigFishNearby.Remove(other.gameObject);
        }
        else if (other.CompareTag("MediumFish")) {
            mediumFishNearby.Remove(other.gameObject);
        }
        else if (other.CompareTag("SmallFish")) {
            smallFishNearby.Remove(other.gameObject);
        }
        else if (other.CompareTag("Algae")) {
            algaeNearby.Remove(other.gameObject);
        }
        else if (other.CompareTag("Wall")) {
            print("bye wall");
            hitWall = false;
=======
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
>>>>>>> 1693c6a32c26015ac7f769788a7070a3c55fea52
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Color r = Color.yellow; 
        r.a = 0.4f;
        Vector3 dir = transform.TransformDirection(Vector3.forward.normalized * 200f);
        Gizmos.DrawRay(transform.position, dir);

        Gizmos.color = r;
        Gizmos.DrawSphere(transform.position, fishInSightDistance);

        r = Color.red;
        Gizmos.color = r;
        Gizmos.DrawSphere(targetPos, 10f);
<<<<<<< HEAD

        Gizmos.DrawRay(transform.position, Vector3.right * 200f);
=======
>>>>>>> 1693c6a32c26015ac7f769788a7070a3c55fea52
    }
    
    // --------------------------WANDER MOVEMENT-------------------------------  //

    void Wander () {
        transform.eulerAngles = new Vector3 (
            Random.Range(-20, 20), Random.Range (transform.eulerAngles.y - 20, transform.eulerAngles.y + 20), 0);
    }
    
    // Check if fish is in sight
    private bool TargetInSight()
    {
<<<<<<< HEAD
        if (currentTarget == null) {

            (GameObject obj, float dist) closestTgt =
                (null, float.PositiveInfinity);

            // if bigfish calls this
            if (this.CompareTag("BigFish")) {
                if (mediumFishNearby.Count > 0) {
                    for (int i = 0; i < mediumFishNearby.Count; i++) {
                        float dist = Vector3.Distance(transform.position,
                            mediumFishNearby[i].transform.position);
                        if (dist < closestTgt.dist) {
                            closestTgt = (mediumFishNearby[i], dist);
                        }
                    }
                }
                else if (smallFishNearby.Count > 0) {
                    for (int i = 0; i < smallFishNearby.Count; i++) {
                        float dist = Vector3.Distance(transform.position,
                            smallFishNearby[i].transform.position);
                        if (dist < closestTgt.dist) {
                            closestTgt = (smallFishNearby[i], dist);
                        }
                    }
                }
            }
            // if mediumfish calls this
            else if (this.CompareTag("MediumFish")) {
                if (smallFishNearby.Count > 0) {
                    for (int i = 0; i < smallFishNearby.Count; i++) {
                        float dist = Vector3.Distance(transform.position,
                            smallFishNearby[i].transform.position);
                        if (dist < closestTgt.dist) {
                            closestTgt = (smallFishNearby[i], dist);
                        }
                    }
                }
                else if (algaeNearby.Count > 0) {
                    for (int i = 0; i < algaeNearby.Count; i++) {
                        float dist = Vector3.Distance(transform.position,
                            algaeNearby[i].transform.position);
                        if (dist < closestTgt.dist) {
                            closestTgt = (algaeNearby[i], dist);
=======
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
>>>>>>> 1693c6a32c26015ac7f769788a7070a3c55fea52
                        }
                    }
                }
            }
            // if smallfish calls this
            else if (this.CompareTag("SmallFish")) {
<<<<<<< HEAD
                if (algaeNearby.Count > 0) {
                    for (int i = 0; i < algaeNearby.Count; i++) {
                        float dist = Vector3.Distance(transform.position,
                            algaeNearby[i].transform.position);
                        if (dist < closestTgt.dist) {
                            closestTgt = (algaeNearby[i], dist);
=======
                if (algaeTargets.Count > 0) {
                    for (int i = 0; i < algaeTargets.Count; i++) {
                        if (algaeTargets[i].dist < closestTgt.dist) {
                            closestTgt = algaeTargets[i];
>>>>>>> 1693c6a32c26015ac7f769788a7070a3c55fea52
                        }
                    }
                }
            }

<<<<<<< HEAD
            currentTarget = closestTgt.obj;
            if (currentTarget != null) return true;
        }
        return false;
    }

    private bool DangerInSight() {
        if (currentDanger == null && !this.CompareTag("BigFish")) {
            (GameObject obj, float dist) closestDanger = (null, float.PositiveInfinity);

            if (this.CompareTag("MediumFish")) {
                if (bigFishNearby.Count > 0) {
                    for (int i = 0; i < bigFishNearby.Count; i++) {
                        float dangerDist = Vector3.Distance(transform.position, 
                            bigFishNearby[i].transform.position);
                        if (dangerDist < closestDanger.dist) {
                            closestDanger = (bigFishNearby[i], dangerDist);
                        }
                    }
                }
            }
            else if (this.CompareTag("SmallFish")) {
                if (bigFishNearby.Count > 0) {
                    for (int i = 0; i < bigFishNearby.Count; i++) {
                        float dangerDist = Vector3.Distance(transform.position, 
                            bigFishNearby[i].transform.position);
                        if (dangerDist < closestDanger.dist) {
                            closestDanger = (bigFishNearby[i], dangerDist);
                        }
                    }
                }
                else if (mediumFishNearby.Count > 0) {
                    for (int i = 0; i < mediumFishNearby.Count; i++) {
                        float dangerDist = Vector3.Distance(transform.position, 
                            mediumFishNearby[i].transform.position);
                        if (dangerDist < closestDanger.dist) {
                            closestDanger = (mediumFishNearby[i], dangerDist);
                        }
                    }
                }
            }

            currentDanger = closestDanger.obj;
            if (currentDanger != null) return true;
        }

=======
            targetFish = closestTgt.obj;
            print($"closestTgt: {closestTgt.obj.name}");
            return true;
        }
>>>>>>> 1693c6a32c26015ac7f769788a7070a3c55fea52
        return false;
    }
    
        // Seek player action
    private void SeekPersueAction()
    {
        // Move towards player
        MoveTowardsTarget(currentTarget.transform.position);
    }

    private void SeekFleeAction()
    {
        // Move towards player
        MoveAwayTarget(currentDanger.transform.position);
    }

    // --------------------------PERSUE MOVEMENT-------------------------------  //
    private void MoveTowardsTarget(Vector3 targetPos)
    {
        // Determine velocity to the target
        //Vector3 vel = (targetPos - transform.position).normalized * movementSpeed;

        // Move towards the target  at the calculated velocity
        transform.LookAt(currentTarget.transform);
        transform.position = Vector3.MoveTowards(transform.position, 
            currentTarget.transform.position, movementSpeed * Time.deltaTime);
        //transform.Translate(transform.position + vel);
        //rb.MoveRotation(Quaternion.LookRotation(currentTarget.transform.position));
        //rb.MovePosition(transform.position + vel);
        //print("im movingbitch");
    }

    // --------------------------FLEE MOVEMENT-------------------------------  //
    private void MoveAwayTarget(Vector3 targetPos)
    {
        Vector3 vel = (transform.position - targetPos).normalized * movementSpeed * Time.deltaTime;
        Vector3 nextPos = transform.position + vel;
        // calcular a distancia do vidro e distancia do tubarão e calcular o angulo de 90 em relação ao tubarão
        //if (nextPos.x > gameArea.MaxVec.x - LimitPaddings.x) {
        //    print("x limit hit");
        //    if (hit.collider.CompareTag("Wall")) {
        //        //vel = transform.right
        //    }
        //    vel = transform.right * movementSpeed;
        //}
        //if (nextPos.y > gameArea.MaxVec.y - LimitPaddings.y) {
        //    print("y limit hit");
        //    vel = new Vector3(vel.x, vel.y * -0.15f, vel.z);
        //}
        //if (nextPos.z > gameArea.MaxVec.z - LimitPaddings.z) {
        //    print("z limit hit");
        //    vel = new Vector3(vel.x, vel.y, vel.z * -0.15f);
        //}
        //if (nextPos.x < gameArea.MinVec.x + LimitPaddings.x) {
        //    print("x limit hit");
        //    vel = new Vector3(vel.x * -0.50f, vel.y, vel.z);
        //}
        //if (nextPos.y < gameArea.MinVec.y + LimitPaddings.y) {
        //    print("y limit hit");
        //    vel = new Vector3(vel.x, vel.y * 0.15f, vel.z);
        //}
        //if (nextPos.z < gameArea.MinVec.z + LimitPaddings.z) {
        //    print("z limit hit");
        //    vel = new Vector3(vel.x, vel.y, vel.z * 0.15f);
        //}
        
        RaycastHit hit1;
        Physics.Raycast(transform.position, -Vector3.right, out hit1, 250);
        RaycastHit hit2;
        Physics.Raycast(transform.position, Vector3.right, out hit2, 250);
        float angle1 = Vector3.Angle(transform.position, hit1.point);
        float angle2 = Vector3.Angle(transform.position, hit2.point);
        print($"angle1: {angle1}\tangle2: {angle2}");
        
        transform.LookAt(transform.position + vel);
        transform.position = Vector3.MoveTowards(transform.position, 
            transform.position + vel, movementSpeed * Time.deltaTime);
        //print("im running bitch");
    }
}