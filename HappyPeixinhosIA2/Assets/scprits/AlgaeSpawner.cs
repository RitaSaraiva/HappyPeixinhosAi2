using UnityEngine;

public class AlgaeSpawner : MonoBehaviour
{
    [SerializeField] private GameObject algaePrefab;
    private float spawnChance;
    private BoxCollider boxCol;
    private float spawnTimer;
    private Vector3 LimitsMin;
    private Vector3 LimitsMax;
    private AIController aiController;
    // Start is called before the first frame update

    private void Awake() {
        boxCol = GetComponent<BoxCollider>();
        aiController = FindObjectOfType<AIController>();
    }

    void Start()
    {
        spawnChance = aiController.algaeSpawnChance;
        LimitsMin = boxCol.bounds.min;
        LimitsMax = boxCol.bounds.max;
        spawnTimer = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        //timer that spawns an algae with a chance each second at a random position
        //in the tile
        if (spawnTimer > 0) {
            spawnTimer -= Time.deltaTime;
        }
        else {
            if (Random.Range(0f, 1f) < spawnChance) {
                GameObject spawnedAlgae = Instantiate(algaePrefab);
                spawnedAlgae.transform.position = RandomTilePosition();
                aiController.amountOfAlgae++;
            }
            spawnTimer = 1;
        }
    }

    //return random position within tile that calls it
    private Vector3 RandomTilePosition() {
        return new Vector3(Random.Range(LimitsMin.x, LimitsMax.x), LimitsMin.y, Random.Range(LimitsMin.z, LimitsMax.z));
    }
}
