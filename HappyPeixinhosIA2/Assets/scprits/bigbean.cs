using UnityEngine;
using System.Collections;

public class bigbean : BasicFish
{
    private AIController aiController;

    public float startEnergyVal { get => aiController.bigFishStartEnergy; }
    public override float EnergyToReproduce { get => aiController.bigFishReproduceEnergy; }

    private void Awake() {
        aiController = FindObjectOfType<AIController>();
    }

    // Start is called before the first frame update
    void Start() {
        energy = startEnergyVal;
        aiController.amountOfBigFishes++;
    }

    protected override void OnDeath()
    {
        aiController.amountOfBigFishes--;
    }
}
