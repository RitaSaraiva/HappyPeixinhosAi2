using UnityEngine;
using System.Collections;

public abstract class BasicFish : MonoBehaviour, IFish, IFood
{
    //Energia que perde por segundo
    [SerializeField] private float energyPerSec;

    //Tempo para perder energia
    private float decayTimer;

    //Energia que dá quando é comido
    public virtual float energyvalue { get; set; }

    //energia
    public float energy { get; set; }
    public virtual float EnergyToReproduce { get; }
    public bool dying;

    protected void Update() {
        //loses energy each second and dies if it reaches 0
        EnergyDecay();
        if (energy < 0 && !dying) Death();
    }

    /// <summary>
    /// The Reproduce behaviour works by dividing the fish's energy by two and 
	/// instantiating a copy of the fish.
    /// </summary>
    public virtual void Reproduce() {
        energy /= 2;
        Instantiate(this);
    }

    //Loses energy each second
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

    //Eats target and gains energy
    public void Eat(IFood target) {
        energy += target.energyvalue;
    }

    //dies
    public virtual void Death() {
        dying = true;
        OnDeath();
        this.gameObject.SetActive(false);
    }

    /// <summary>
    /// abstract method that is called when the target dies 
    /// </summary>
    protected abstract void OnDeath();

    private void OnDisable()
    {
        //destroy fish when disabled 
        GameObject.Destroy(this.gameObject);
    }

}
