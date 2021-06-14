using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    private Image algaeBar;
    private Image smallFishBar;
    private Image MediumFishBar;
    private Image BigFishBar;

    private void Awake() {
        Image[] bars = GetComponentsInChildren<Image>();
        for (int i = 0; i < bars.Length; i++) {
            switch (bars[i].name) {
                case "algaeBar":
                    algaeBar = bars[i];
                    break;
                case "smallFishBar":
                    smallFishBar = bars[i];
                    break;
                case "mediumFishBar":
                    MediumFishBar = bars[i];
                    break;
                case "bigFishBar":
                    BigFishBar = bars[i];
                    break;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
