using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Algae : MonoBehaviour, IFood
{
    [SerializeField] private float energyAsFood;
    [SerializeField] private float floatUpSpeed;
    public float energyvalue {get; set;}

    // Start is called before the first frame update
    void Start()
    {
        energyvalue = energyAsFood;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * floatUpSpeed * Time.deltaTime);
    }
    
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("VerticalLimits")) {
            RemoveAlgae();
        }
    }

    public void RemoveAlgae() {
        GameObject.Destroy(this.gameObject);
    }
}
