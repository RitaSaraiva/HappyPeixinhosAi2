using System.Collections.Generic;
using UnityEngine;
using LibGameAI.DecisionTrees;

public class AIBehaviour : MonoBehaviour
{
    [SerializeField] private Vector3 targetPos;

    // Speed of AI agent movement
    [SerializeField] private float movementSpeed;
    
    // Player in sight distance
    [SerializeField] private float targetInSightDistance = 10f;
    [SerializeField] private float targetInEatingDistance = 7.5f;
    [SerializeField] private GameObject currentTarget;
    [SerializeField] private GameObject currentDanger;
    [SerializeField] private BasicFish fishScript;

    private bool canEatTarget;

    private SphereCollider sphereCol;
    private List<GameObject> bigFishNearby = new List<GameObject>();
    private List<GameObject> mediumFishNearby = new List<GameObject>();
    private List<GameObject> smallFishNearby = new List<GameObject>();
    private List<GameObject> algaeNearby = new List<GameObject>();

    private IDecisionTreeNode rootCanSeeFood;

    [SerializeField] private AIController aiController;
    [SerializeField] private GameArea gameArea;
    //--------------------------------------------------------------------//

    private void Start() {
        
        //create new target position so fishes wont have the same target pos after
        //reproducing
        targetPos = aiController.RandomPosition();

        //change collider radius to match range
        sphereCol.radius = targetInSightDistance;

        // Create decision tree nodes
        IDecisionTreeNode wander = new ActionNode(WanderAction);
        IDecisionTreeNode seekPursue = new ActionNode(SeekPursueAction);
        IDecisionTreeNode seekFlee = new ActionNode(SeekFleeAction);
        IDecisionTreeNode eat = new ActionNode(EatTarget);
        IDecisionTreeNode seggsyTime = new ActionNode(fishScript.Reproduce);

        IDecisionTreeNode canEatFood = new DecisionNode(TargetInEatRange, eat, seekPursue);
        IDecisionTreeNode canSeggs = new DecisionNode(CanReproduce, seggsyTime, wander);
        IDecisionTreeNode canSeeEnemy = new DecisionNode(DangerInSight, seekFlee, canSeggs);
        rootCanSeeFood = new DecisionNode(TargetInSight, canEatFood, canSeeEnemy);
    }
    
    void Awake() {
        fishScript = GetComponent<BasicFish>();
        gameArea = FindObjectOfType<GameArea>();
        sphereCol = GetComponent<SphereCollider>();
        aiController = FindObjectOfType<AIController>();
    }
    
    // Update is called once per frame
    void Update()
    {
        //create and start root node
        ActionNode actionNode = rootCanSeeFood.MakeDecision() as ActionNode;
        actionNode.Execute();
    }

    /// <summary>
    /// The Wander movement behaviour works by using the bounds of the GameArea collider
	///and generating a random position inside it to which the fish will move to.
    /// </summary>
    private void WanderAction() {
        //random position to target pos if there is none
        if (targetPos == Vector3.zero)
        {
            targetPos = aiController.RandomPosition();
        }
        //make fish face target pos and move to it
        else
        {
            //determine fish speed
            switch (this.tag)
            {
                case "BigFish":
                    movementSpeed = aiController.bigFishSpeed;
                    break;
                case "MediumFish":
                    movementSpeed = aiController.mediumFishSpeed;
                    break;
                case "SmallFish":
                    movementSpeed = aiController.smallFishSpeed;
                    break;
            }
            //rotate fish to face target pos
            RotateNPC(targetPos, movementSpeed * Time.deltaTime);
            //move fish to target pos
            transform.position = Vector3.MoveTowards(transform.position,
                targetPos, movementSpeed * Time.deltaTime);
            //reset target pos if its reached
            if (Vector3.Distance(transform.position, targetPos) < 2.5f)
                targetPos = Vector3.zero;
        }
    }

    /// <summary>
    ///The Persue movement behaviour works by calculating the vector of the direction
	///to the nearest target moving the fish towards him. 
    /// </summary>
    private void SeekPursueAction() {
        //checks if theres a current target
        if (currentTarget != null)
        {   
            PursueAction();
        }
    }

    /// <summary>
    /// The Flee movement behaviour works by calculating the vector contrary to the
	///direction to the nearest enemy, while simultaneously keeping the fish inside
	///the GameArea by checking it's bounds and changing course if the fish moves
	///within an area of "padding" as to not leave the game area.
    /// </summary>
    private void SeekFleeAction() {
        //checks if theres a current DANGER !
        if (currentDanger != null)
        {
            FleeAction();
        }
    }

    //rotate the fish to face new waypoint
    void RotateNPC (Vector3 waypoint, float currentSpeed)
    {
        //get random speed up for the turn
        float TurnSpeed = currentSpeed * Random.Range(1f, 3f);
 
        //get new direction to look at for target
        Vector3 LookAt = waypoint - this.transform.position;
        transform.rotation = Quaternion.Slerp(transform.rotation, 
            Quaternion.LookRotation(LookAt), TurnSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other) {
        //checks what got in range and assigns it to the right list 
        
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
        //checks what got out of range and removes it from the right list 
        
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
    
    /// <summary>
    /// The fish knows what other fishes and algae are nearby by making use of the
	///OnTriggerEnter and OnTriggerExit unity methods, adding any fish or algae
	///that gets within range to the appropriate list and removing them from said list
	///when they get out of range. The TargetInSight decision node works by first
	///checking which type of fish is calling it and checks the lists of what it can
	///eat in order of preference. Finally if theres an appropriate target it becomes
	///the current target.
    /// </summary>
    /// <returns>bool</returns>
    private bool TargetInSight()
    {
        //checks if there isnt a current target
        if (currentTarget == null) {
            (GameObject obj, float dist) closestTgt =
                (null, float.PositiveInfinity);

            // if bigfish calls this
            if (this.CompareTag("BigFish")) {
                //runs through the appropriate target lists in order of preference
                //and makes the closest and preferable the current target
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
                //runs through the appropriate target lists in order of preference
                //and makes the closest and preferable the current target
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
                //runs through the appropriate target lists in order of preference
                //and makes the closest and preferable the current target
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

            //make closest and preferable target current target
            currentTarget = closestTgt.obj;
        }
        //return true if theres a current target otherwise return false
        if (currentTarget != null) return true;
        return false;
    }

    /// <summary>
    /// The DangerInSight decision node works in a similar way to TargetInSight,
	///but checking what fish can eat the fish that calls it.
    /// </summary>
    /// <returns>bool</returns>
    private bool DangerInSight() {
        //check if there is a current danger and this fish isnt a big fish
        if (currentDanger == null && !this.CompareTag("BigFish")) {
            (GameObject obj, float dist) closestDanger = (null, float.PositiveInfinity);

            //runs through the appropriate danger lists in order of fish size
            //and makes the closest and biggest the current danger
            if (this.CompareTag("MediumFish")) {
                if (bigFishNearby.Count > 0) {
                    for (int i = 0; i < bigFishNearby.Count; i++) {
                        if (bigFishNearby[i] == null) continue;
                        float dangerDist = Vector3.Distance(transform.position, 
                            bigFishNearby[i].transform.position);
                        if (dangerDist < closestDanger.dist) {
                            closestDanger = (bigFishNearby[i], dangerDist);
                        }
                    }
                }
            }
            //runs through the appropriate danger lists in order of fish size
            //and makes the closest and biggest the current danger
            else if (this.CompareTag("SmallFish")) {
                if (bigFishNearby.Count > 0) {
                    for (int i = 0; i < bigFishNearby.Count; i++) {
                        if (bigFishNearby[i] == null) continue;
                        float dangerDist = Vector3.Distance(transform.position, 
                            bigFishNearby[i].transform.position);
                        if (dangerDist < closestDanger.dist) {
                            closestDanger = (bigFishNearby[i], dangerDist);
                        }
                    }
                }
                else if (mediumFishNearby.Count > 0) {
                    for (int i = 0; i < mediumFishNearby.Count; i++) {
                        if (mediumFishNearby[i] == null) continue;
                        float dangerDist = Vector3.Distance(transform.position, 
                            mediumFishNearby[i].transform.position);
                        if (dangerDist < closestDanger.dist) {
                            closestDanger = (mediumFishNearby[i], dangerDist);
                        }
                    }
                }
            }

            //make closest and biggest danger current danger
            currentDanger = closestDanger.obj;
        }
        //return true if theres a current danger otherwise return false
        if (currentDanger != null) return true;
        return false;
    }
    
    // Seek action
    private void PursueAction()
    {
        // Move towards target
        MoveTowardsTarget(currentTarget.transform.position);
    }

    // Flee action
    private void FleeAction()
    {
        // Move away from danger
        MoveAwayTarget(currentDanger.transform.position);
    }

    // --------------------------PERSUE MOVEMENT-------------------------------  //
    /// <summary>
    /// moves fish towards target at a speed determined by its type
    /// </summary>
    /// <param name="targetPos">targetpos</param>
    private void MoveTowardsTarget(Vector3 targetPos)
    {
        //determine fish speed
        switch (this.tag)
        {
            case "BigFish":
                movementSpeed = aiController.bigFishSpeed;
                break;
            case "MediumFish":
                movementSpeed = aiController.mediumFishSpeed;
                break;
            case "SmallFish":
                movementSpeed = aiController.smallFishSpeed;
                break;
        }
        // Move towards the target at the calculated velocity
        transform.LookAt(currentTarget.transform);
        transform.position = Vector3.MoveTowards(transform.position, 
            currentTarget.transform.position, movementSpeed * Time.deltaTime);
    }

    // --------------------------FLEE MOVEMENT-------------------------------  //

    /// <summary>
    /// moves fish away from target at a speed determined by its type
    /// </summary>
    /// <param name="targetPos">targetpos</param>
    private void MoveAwayTarget(Vector3 targetPos)
    {
        //determine fish speed
        switch (this.tag)
        {
            case "BigFish":
                movementSpeed = aiController.bigFishSpeed;
                break;
            case "MediumFish":
                movementSpeed = aiController.mediumFishSpeed;
                break;
            case "SmallFish":
                movementSpeed = aiController.smallFishSpeed;
                break;
        }
        Vector3 vel = (transform.position - targetPos).normalized * movementSpeed * Time.deltaTime;
        Vector3 nextPos = transform.position + vel;
        // checks if fish is near the GAmeArea bounds and changes directions if it is
        // depending on which side its at
        if (nextPos.x > gameArea.MaxVec.x - aiController.LimitPaddings.x) {
            vel = new Vector3(vel.x * -0.15f, vel.y, vel.z);
        }
        if (nextPos.y > gameArea.MaxVec.y - aiController.LimitPaddings.y) {
            vel = new Vector3(vel.x, -1f, vel.z);
        }
        if (nextPos.z > gameArea.MaxVec.z - aiController.LimitPaddings.z) {
            vel = new Vector3(vel.x, vel.y, vel.z * -0.15f);
        }
        if (nextPos.x < gameArea.MinVec.x + aiController.LimitPaddings.x) {
            vel = new Vector3(vel.x * -0.50f, vel.y, vel.z);
        }
        if (nextPos.y < gameArea.MinVec.y + aiController.LimitPaddings.y) {
            vel = new Vector3(vel.x, 1f, vel.z);
        }
        if (nextPos.z < gameArea.MinVec.z + aiController.LimitPaddings.z) {
            vel = new Vector3(vel.x, vel.y, vel.z * 0.15f);
        }
        
        // Move away from the danger at the calculated velocity
        transform.LookAt(transform.position + vel);
        transform.position = Vector3.MoveTowards(transform.position, 
            transform.position + vel, movementSpeed * Time.deltaTime);
    }

    /// <summary>
    /// The TargetInEatRange decision node works by calculating the distance between
	///the fish that calls it and its current target and if it within eating range 
	///returns true, otherwise returning false.
    /// </summary>
    /// <returns>bool</returns>
    private bool TargetInEatRange() {
        //determine distance from target
        float dist = Vector3.Distance(transform.position,
            currentTarget.transform.position);
        //check if there is a target and if its in eating range 
        if (currentTarget != null && dist < targetInEatingDistance) {
            //returns true if it can eat the target
            canEatTarget = true;
            return true;
        }
        //returns false if it cant eat the target
        canEatTarget = false;
        return false;
    }

    /// <summary>
    /// The Eat Target behaviour works by checking if the target is a fish or an algae
	/// and casting the target as IFood, then removing it from the appropriate target
	/// list and removing it from the scene.
    /// </summary>
    private void EatTarget() {
        //checks if fish can eat the target
        if (canEatTarget) {
            BasicFish tgt = null;
            Algae algaeTgt = null;
            //checks if target is a fish or an algae
            if (currentTarget.TryGetComponent<BasicFish>(out tgt) || currentTarget.TryGetComponent<Algae>(out algaeTgt)) {
                // if it is either eat the target as an IFOOD
                if (tgt != null) fishScript.Eat(tgt as IFood);
                else if (algaeTgt != null) fishScript.Eat(algaeTgt as IFood);
                //check what the target was and removes it from the right list
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
                //resets current target 
                currentTarget = null;
                //checks if target still exists in the scene and removes it
                if (tgt != null && !tgt.dying) tgt.Death();
                else if (algaeTgt != null) algaeTgt.RemoveAlgae();
                canEatTarget = false;
            }
        }
    }

    /// <summary>
    /// The CanReproduce decision node works by checking if the fish that calls it has
	///enough energy to reproduce.
    /// </summary>
    /// <returns>bool</returns>
    private bool CanReproduce() {
        // checks if the fish has enough energy to reproduce returning true if it does
        // otherwise returning false
        if (this.gameObject.activeSelf && fishScript.energy > fishScript.EnergyToReproduce) return true;
        return false;
    }
}