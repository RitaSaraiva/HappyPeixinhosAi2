using System.Collections;
using System.Collections.Generic;
using System;

public interface IFish
{
    //energia
    float energy {get; set;}

    //Saber que comida
    List<IFood> foodlist {get; set;}

    //Saber quais os inimigos
    List<IFish> dangerfish {get;set;}

}
