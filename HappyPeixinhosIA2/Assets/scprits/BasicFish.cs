using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasicFish : MonoBehaviour, IFish, IFood
{
    
    public float energyvalue {get; set;}

    //energia
    public float energy {get; set;}

    //Saber que comida
    public List<IFood> foodlist {get; set;}

    //Saber quais os inimigos
    public List<IFish> dangerfish {get; set;}

}
