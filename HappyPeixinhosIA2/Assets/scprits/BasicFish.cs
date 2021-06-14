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
        EnergyDecay();
        if (energy < 0 && !dying) StartCoroutine(Death());
    }

    //Reproduzir
    public virtual void Reproduce() {
        energy /= 2;
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
    public virtual IEnumerator Death() {
        dying = true;
        this.gameObject.SetActive(false);
        yield return new WaitForEndOfFrame();
        GameObject.Destroy(this.gameObject);
        OnDeath();
    }

    protected abstract void OnDeath();
}
