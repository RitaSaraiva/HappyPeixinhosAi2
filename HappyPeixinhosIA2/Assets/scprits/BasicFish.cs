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

    //Saber que comida
    public List<FishType> foodlist {get; set;}

    //Saber quais os inimigos
    public List<FishType> dangerfish {get; set;}

    [SerializeField] private LayerMask targetLayers;
    [SerializeField] private LayerMask dangerLayers;

    //Reproduzir
    protected void Reproduce() {
        energy /= 2;
        print(energy);
        Instantiate(this);
    }

    //perda de energia por segundo
    protected void EnergyDecay() {
        decayTimer = Time.deltaTime;
        if (decayTimer > 1f)
        {
            energy -= energyPerSec;
            decayTimer = 0f;
        }
    }

<<<<<<< HEAD
=======
    [SerializeField] private LayerMask targetLayers;
    [SerializeField] private LayerMask dangerLayers;

    //Reproduzir
    protected void Reproduce() {
        energy /= 2;
        print(energy);
        Instantiate(this);
    }

    //perda de energia por segundo
    protected void EnergyDecay() {
        decayTimer = Time.deltaTime;
        if (decayTimer > 1f)
        {
            energy -= energyPerSec;
            decayTimer = 0f;
        }
    }

>>>>>>> 1693c6a32c26015ac7f769788a7070a3c55fea52
    //morrer
    protected virtual void Death() {
        GameObject.Destroy(this);
    }
}
