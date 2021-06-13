using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bigbean : BasicFish
{
    [SerializeField] private float energyAsFood;
    [SerializeField] private float startEnergyVal;

    [SerializeField] private List<FishType> dangerList;
    [SerializeField] private List<FishType> foodList;


    // Start is called before the first frame update
    void Start()
    {
        energyvalue = energyAsFood;
        energy = startEnergyVal;
    }

    // Update is called once per frame
    void Update()
    {
        print($"Current Energy: {energy}");
        if (energy > energyToReproduce && Input.GetKeyDown(KeyCode.Q))
        {
            Reproduce();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            Death();
        }
    }
}
