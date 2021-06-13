﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBehaviour : MonoBehaviour
{
    // Reference to gamearea object
    [SerializeField] private GameArea gameArea;
    // Paddings on area limits
    [SerializeField] private Vector3 LimitPaddings;
    [SerializeField] private Vector3 targetPos;

    // Speed of AI agent movement
    [SerializeField] private float movementSpeed;
    
    // Player in sight distance
    [SerializeField] private float targetInSightDistance = 10f;
    [SerializeField] private float targetInEatingDistance = 7.5f;
    [SerializeField] private GameObject currentTarget;
    [SerializeField] private GameObject currentDanger;
    [SerializeField] private BasicFish fishScript;

    // Reference to the agent's rigid body
    private Rigidbody rb;
    private bool hitWall;

    private SphereCollider sphereCol;
    private List<GameObject> bigFishNearby = new List<GameObject>();
    private List<GameObject> mediumFishNearby = new List<GameObject>();
    private List<GameObject> smallFishNearby = new List<GameObject>();
    private List<GameObject> algaeNearby = new List<GameObject>();
    
    //--------------------------------------------------------------------//

    private void Start() {
        sphereCol.radius = targetInSightDistance;
    }
    
    void Awake() {
        rb = GetComponent<Rigidbody>();
        fishScript = GetComponent<BasicFish>();
        gameArea = FindObjectOfType<GameArea>();
        sphereCol = GetComponent<SphereCollider>();
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

        // ------------ WANDER (OLD - "WORKING")

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

        // ------ END WANDER (OLD - "WORKING")

        // ------------ SEEK PURSUE

        if (currentTarget != null || TargetInSight()){
            SeekPersueAction();
            //print("im see the bitch");
            EatTarget();
        }

        // ------ END SEEK PURSUE

        // ------------ SEEK FLEE

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
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Color r = Color.yellow; 
        r.a = 0.4f;
        Vector3 dir = transform.TransformDirection(Vector3.forward * 200f);
        Gizmos.DrawRay(transform.position, dir);

        Gizmos.color = r;
        Gizmos.DrawSphere(transform.position, targetInEatingDistance);

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
    private bool TargetInSight()
    {
        if (currentTarget == null) {

            (GameObject obj, float dist) closestTgt =
                (null, float.PositiveInfinity);

            // if bigfish calls this
            if (this.CompareTag("BigFish")) {
                if (mediumFishNearby.Count > 0) {
                    for (int i = 0; i < mediumFishNearby.Count; i++) {
                        if (mediumFishNearby[i] == null) continue;
                        float dist = Vector3.Distance(transform.position,
                            mediumFishNearby[i].transform.position);
                        if (dist < closestTgt.dist) {
                            closestTgt = (mediumFishNearby[i], dist);
                        }
                    }
                }
                else if (smallFishNearby.Count > 0) {
                    for (int i = 0; i < smallFishNearby.Count; i++) {
                        if (smallFishNearby[i] == null) continue;
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
                        if (smallFishNearby[i] == null) continue;
                        float dist = Vector3.Distance(transform.position,
                            smallFishNearby[i].transform.position);
                        if (dist < closestTgt.dist) {
                            closestTgt = (smallFishNearby[i], dist);
                        }
                    }
                }
                else if (algaeNearby.Count > 0) {
                    for (int i = 0; i < algaeNearby.Count; i++) {
                        if (algaeNearby[i] == null) continue;
                        float dist = Vector3.Distance(transform.position,
                            algaeNearby[i].transform.position);
                        if (dist < closestTgt.dist) {
                            closestTgt = (algaeNearby[i], dist);
                        }
                    }
                }
            }
            // if smallfish calls this
            else if (this.CompareTag("SmallFish")) {
                if (algaeNearby.Count > 0) {
                    for (int i = 0; i < algaeNearby.Count; i++) {
                        if (algaeNearby[i] == null) continue;
                        float dist = Vector3.Distance(transform.position,
                            algaeNearby[i].transform.position);
                        if (dist < closestTgt.dist) {
                            closestTgt = (algaeNearby[i], dist);
                        }
                    }
                }
            }

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
        // Move towards the target  at the calculated velocity
        transform.LookAt(currentTarget.transform);
        transform.position = Vector3.MoveTowards(transform.position, 
            currentTarget.transform.position, movementSpeed * Time.deltaTime);
        //print("im movingbitch");
    }

    // --------------------------FLEE MOVEMENT-------------------------------  //
    private void MoveAwayTarget(Vector3 targetPos)
    {
        Vector3 vel = (transform.position - targetPos).normalized * movementSpeed * Time.deltaTime;
        Vector3 nextPos = transform.position + vel;
        // calcular a distancia do vidro e distancia do tubarão e calcular o angulo de 90 em relação ao tubarão
        if (nextPos.x > gameArea.MaxVec.x - LimitPaddings.x) {
            print("x limit hit");
            vel = new Vector3(vel.x * -0.15f, vel.y, vel.z);
        }
        if (nextPos.y > gameArea.MaxVec.y - LimitPaddings.y) {
            print("y limit hit");
            vel = new Vector3(vel.x, vel.y * -0.15f, vel.z);
        }
        if (nextPos.z > gameArea.MaxVec.z - LimitPaddings.z) {
            print("z limit hit");
            vel = new Vector3(vel.x, vel.y, vel.z * -0.15f);
        }
        if (nextPos.x < gameArea.MinVec.x + LimitPaddings.x) {
            print("x limit hit");
            vel = new Vector3(vel.x * -0.50f, vel.y, vel.z);
        }
        if (nextPos.y < gameArea.MinVec.y + LimitPaddings.y) {
            print("y limit hit");
            vel = new Vector3(vel.x, vel.y * 0.15f, vel.z);
        }
        if (nextPos.z < gameArea.MinVec.z + LimitPaddings.z) {
            print("z limit hit");
            vel = new Vector3(vel.x, vel.y, vel.z * 0.15f);
        }
        
        transform.LookAt(transform.position + vel);
        transform.position = Vector3.MoveTowards(transform.position, 
            transform.position + vel, movementSpeed * Time.deltaTime);
        //print("im running bitch");
    }

    private void EatTarget() {
        if (Vector3.Distance(transform.position, currentTarget.transform.position) < targetInEatingDistance) {
            BasicFish tgt;
            if (currentTarget.TryGetComponent<BasicFish>(out tgt)) {
                print($"energy before eating: {fishScript.energy}");
                fishScript.Eat(tgt as IFood);
                switch(currentTarget.tag) {
                    case "MediumFish":
                        mediumFishNearby.Remove(currentTarget);
                        break;
                    case "SmallFish":
                        smallFishNearby.Remove(currentTarget);
                        break;
                    case "Algae":
                        algaeNearby.Remove(currentTarget);
                        break;
                }
                currentTarget = null;
                tgt.Death();

                print($"energy after eating: {fishScript.energy}");
            }
        }
    }
}