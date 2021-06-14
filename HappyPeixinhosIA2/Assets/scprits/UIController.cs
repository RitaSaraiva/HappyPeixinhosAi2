using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    private Image algaeBar;
    private Image smallFishBar;
    private Image mediumFishBar;
    private Image bigFishBar;

    private AIController aiController;

    private void Awake() {
        aiController = FindObjectOfType<AIController>();
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
                    mediumFishBar = bars[i];
                    break;
                case "bigFishBar":
                    bigFishBar = bars[i];
                    break;
            }
        }
    }

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        // calculate total amount of agents
        int totalAgents = aiController.amountOfAlgae + aiController.amountOfSmallFishes + aiController.amountOfMediumFishes + aiController.amountOfBigFishes;
        // algae scale
        float newAlgaeScale;
        if (aiController.amountOfAlgae > 0) newAlgaeScale = (float)aiController.amountOfAlgae / totalAgents;
        else newAlgaeScale = 0;
        algaeBar.transform.localScale = new Vector3(algaeBar.transform.localScale.x, newAlgaeScale, algaeBar.transform.localScale.z);
        // small fish scale
        float newSmallFishScale;
        if (aiController.amountOfSmallFishes > 0) newSmallFishScale = (float)aiController.amountOfSmallFishes / totalAgents;
        else newSmallFishScale = 0;
        smallFishBar.transform.localScale = new Vector3(smallFishBar.transform.localScale.x, newSmallFishScale, smallFishBar.transform.localScale.z);
        // medium fish scale
        float newMediumFishScale;
        if (aiController.amountOfMediumFishes > 0) newMediumFishScale = (float)aiController.amountOfMediumFishes / totalAgents;
        else newMediumFishScale = 0;
        mediumFishBar.transform.localScale = new Vector3(mediumFishBar.transform.localScale.x, newMediumFishScale, mediumFishBar.transform.localScale.z);
        // big fish scale
        float newBigFishScale;
        if (aiController.amountOfBigFishes > 0) newBigFishScale = (float)aiController.amountOfBigFishes / totalAgents;
        else newBigFishScale = 0;
        bigFishBar.transform.localScale = new Vector3(bigFishBar.transform.localScale.x, newBigFishScale, bigFishBar.transform.localScale.z);
    }
}
