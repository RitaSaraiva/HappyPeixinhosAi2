using UnityEngine;
using System.Collections;

public class Algae : MonoBehaviour, IFood
{
    private AIController aiController;

    public float energyvalue { get => aiController.algaeEnergeticValue; }

    private void Awake() {
        aiController = FindObjectOfType<AIController>();
    }

    // Update is called once per frame
    void Update() {
        transform.Translate(Vector3.up * aiController.algaeSpeed * Time.deltaTime);
    }
    
    private void OnTriggerEnter(Collider other) {
        //check if algae entered the upper limit 
        if (other.CompareTag("VerticalLimits")) {
            RemoveAlgae();
        }
    }

    //removes algae from scene 
    public void RemoveAlgae() {
        //decreases the algae count and disables it
        aiController.amountOfAlgae--;
        this.gameObject.SetActive(false);
    }

    private void OnDisable() {
        //destroy algae when disabled 
        GameObject.Destroy(this.gameObject);
    }
}
