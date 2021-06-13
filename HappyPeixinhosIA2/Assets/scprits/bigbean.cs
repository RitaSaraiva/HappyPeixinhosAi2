using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bigbean : BasicFish
{
    [SerializeField] private float energyAsFood;
    [SerializeField] private float startEnergyVal;


    // Start is called before the first frame update
    void Start()
    {
        energyvalue = energyAsFood;
        energy = startEnergyVal;
    }
}
