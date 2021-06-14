using UnityEngine;

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
        if (other.CompareTag("VerticalLimits")) {
            RemoveAlgae();
        }
    }

    public void RemoveAlgae() {
        GameObject.Destroy(this.gameObject);
        aiController.amountOfAlgae--;
    }
}
