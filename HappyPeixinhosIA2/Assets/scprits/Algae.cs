using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Algae : MonoBehaviour, IFood
{
    [SerializeField] private float floatUpSpeed;
    public float energyvalue {get; set;}

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * floatUpSpeed * Time.deltaTime);
    }
    
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("VerticalLimits")) {
            GameObject.Destroy(this.gameObject);
        }
    }
}
