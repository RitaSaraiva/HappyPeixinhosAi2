using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    // serialized private variables
    [Header("Algae")]
    [Range(0, 1)] public float algaeSpawnChance;
    public float algaeSpeed;
    public float algaeEnergeticValue;
    [Header("Small Fish")]
    public float smallFishSpeed;
    public float smallFishStartEnergy;
    public float smallFishReproduceEnergy;
    public float smallFishEnergeticValue;
    public float smallFishTargetInSightDistance;
    public float smallFishEnemyInSightDistance;
    [Header("Medium Fish")]
    public float mediumFishSpeed;
    public float mediumFishStartEnergy;
    public float mediumFishReproduceEnergy;
    public float mediumFishEnergeticValue;
    public float mediumFishTargetInSightDistance;
    public float mediumFishEnemyInSightDistance;
    [Header("Big Fish")]
    public float bigFishSpeed;
    public float bigFishStartEnergy;
    public float bigFishReproduceEnergy;
    public float bigFishTargetInSightDistance;
    public float bigFishEnemyInSightDistance;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
