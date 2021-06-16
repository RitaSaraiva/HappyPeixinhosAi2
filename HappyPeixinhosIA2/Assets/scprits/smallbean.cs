using UnityEngine;

public class smallbean : BasicFish
{
    private AIController aiController;
    public override float energyvalue { get => aiController.smallFishEnergeticValue; }
    public float startEnergyVal { get => aiController.smallFishStartEnergy; }
    public override float EnergyToReproduce { get => aiController.smallFishReproduceEnergy; }


    private void Awake() {
        aiController = FindObjectOfType<AIController>();
    }

    // Start is called before the first frame update
    void Start() {
        energy = startEnergyVal;
        //increases small fish count 
        aiController.amountOfSmallFishes++;
    }

    protected override void OnDeath()
    {
        //decreases the small fish count
        aiController.amountOfSmallFishes--;
    }
}
