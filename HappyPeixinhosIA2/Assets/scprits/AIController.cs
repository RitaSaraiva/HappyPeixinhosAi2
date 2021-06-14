using UnityEngine;

public class AIController : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject smallFishPrefab;
    [SerializeField] private GameObject mediumFishPrefab;
    [SerializeField] private GameObject bigFishPrefab;

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
    [Header("Medium Fish")]
    public float mediumFishSpeed;
    public float mediumFishStartEnergy;
    public float mediumFishReproduceEnergy;
    public float mediumFishEnergeticValue;
    [Header("Big Fish")]
    public float bigFishSpeed;
    public float bigFishStartEnergy;
    public float bigFishReproduceEnergy;
    [Header("Population Stats")]
    public int amountOfBigFishes;
    public int amountOfMediumFishes;
    public int amountOfSmallFishes;
    public int amountOfAlgae;
    [Header("Initital Population")]
    public int initialSmallFishes;
    public int initialMediumFishes;
    public int initialBigFishes;
    [Header("General AI Control")]
    public float TargetInSightDistance;
    public float EnemyInSightDistance;
    public Vector3 LimitPaddings;


    // private variables
    private GameArea gameArea;
    
    private void Awake() {
        gameArea = FindObjectOfType<GameArea>();
    }

    // Start is called before the first frame update
    void Start() {
        GameObject newFish;
        for (int i = 0; i < initialSmallFishes; i++) {
            newFish = Instantiate(smallFishPrefab);
            newFish.transform.position = RandomPosition();
        }
        for (int i = 0; i < initialMediumFishes; i++) {
            newFish = Instantiate(mediumFishPrefab);
            newFish.transform.position = RandomPosition();
        }
        for (int i = 0; i < initialBigFishes; i++) {
            newFish = Instantiate(bigFishPrefab);
            newFish.transform.position = RandomPosition();
        }
    }

    // Update is called once per frame
    void Update() {
        
    }

    public Vector3 RandomPosition() {
        float targetX = Random.Range(gameArea.MinVec.x + LimitPaddings.x,
            gameArea.MaxVec.x - LimitPaddings.x);
        float targetY = Random.Range(gameArea.MinVec.y + LimitPaddings.y,
            gameArea.MaxVec.y - LimitPaddings.y);
        float targetZ = Random.Range(gameArea.MinVec.z + LimitPaddings.z,
            gameArea.MaxVec.z - LimitPaddings.z);

        return new Vector3(targetX, targetY, targetZ);
    }
}
