using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlgaeSpawner : MonoBehaviour
{
    [SerializeField] private GameObject algaePrefab;
    [Range(0,1)]
    [SerializeField] private float spawnChance;
    private BoxCollider boxCol;
    private float spawnTimer;
    private Vector3 LimitsMin;
    private Vector3 LimitsMax;
    // Start is called before the first frame update

    private void Awake() {
        boxCol = GetComponent<BoxCollider>();
    }

    void Start()
    {
        LimitsMin = boxCol.bounds.min;
        LimitsMax = boxCol.bounds.max;
        spawnTimer = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnTimer > 0) {
            spawnTimer -= Time.deltaTime;
        }
        else {
            if (Random.Range(0f, 1f) < spawnChance) {
                GameObject spawnedAlgae = Instantiate(algaePrefab);
                spawnedAlgae.transform.position = RandomTilePosition();
            }
            spawnTimer = 1;
        }
    }

    private Vector3 RandomTilePosition() {
        return new Vector3(Random.Range(LimitsMin.x, LimitsMax.x), LimitsMin.y, Random.Range(LimitsMin.z, LimitsMax.z));
    }
}
