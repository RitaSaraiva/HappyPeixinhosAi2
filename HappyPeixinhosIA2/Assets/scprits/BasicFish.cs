using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasicFish : MonoBehaviour, IFish, IFood
{

    //Energia para reproduzir
    [SerializeField] protected float energyToReproduce;

    //Energia que perde por segundo
    [SerializeField] private float energyPerSec;

    //Tempo para perder energia
    private float decayTimer;

    //Energia que dá quando é comido
    public float energyvalue {get; set;}

    //energia
    public float energy {get; set;}
    public float EnergyToReproduce { get => energyToReproduce; }

    private void Update() {
        EnergyDecay();
        if (energy < 0) Death();
    }

    //Reproduzir
    public void Reproduce() {
        energy /= 2;
        print(energy);
        Instantiate(this);
    }

    //perda de energia por segundo
    protected void EnergyDecay() {
        if (decayTimer > 0)
        {
            decayTimer -= Time.deltaTime;
        }
        else
        {
            energy -= energyPerSec;
            decayTimer = 1f;
        }
    }

    //comer
    public void Eat(IFood target) {
        energy += target.energyvalue;
    }

    //morrer
    public virtual void Death() {
        GameObject.Destroy(this.gameObject);
    }
}
