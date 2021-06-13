using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameArea : MonoBehaviour
{
    private BoxCollider col;
    private Bounds colBounds;
    public Vector3 MaxVec { get; private set; }
    public Vector3 MinVec { get; private set; }

    private void Awake() {
        col = GetComponent<BoxCollider>();
    }

    // Start is called before the first frame update
    void Start()
    {
        colBounds = col.bounds;
        MaxVec = new Vector3(col.bounds.max.x, col.bounds.max.y, col.bounds.max.z);
        MinVec = new Vector3(col.bounds.min.x, col.bounds.min.y, col.bounds.min.z);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
