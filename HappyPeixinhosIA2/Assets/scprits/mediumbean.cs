using UnityEngine;

public class mediumbean : BasicFish
{
    private AIController aiController;
    public override float energyvalue { get => aiController.mediumFishEnergeticValue; }
    public float startEnergyVal { get => aiController.mediumFishStartEnergy; }
    public override float EnergyToReproduce { get => aiController.mediumFishReproduceEnergy; }

    private void Awake()
    {
        aiController = FindObjectOfType<AIController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        energy = startEnergyVal;
        //increases medium fish count 
        aiController.amountOfMediumFishes++;
    }

    protected override void OnDeath()
    {
        //decreases the medium fish count
        aiController.amountOfMediumFishes--;
    }
}
