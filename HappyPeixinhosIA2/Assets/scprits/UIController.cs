using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    // populaçao
    private Image algaeBar;
    private Image smallFishBar;
    private Image mediumFishBar;
    private Image bigFishBar;
    private Text algaeText;
    private Text smallFishText;
    private Text mediumFishText;
    private Text bigFishText;

    // mostrar valores
    private Text algaeSpeedDisplay;

    private AIController aiController;

    private Image[] bars;
    private Text[] texts;

    private void Awake() {
        aiController = FindObjectOfType<AIController>();
        bars = GetComponentsInChildren<Image>();
        texts = GetComponentsInChildren<Text>();
    }

    // Start is called before the first frame update
    void Start() {
        //for cycle that assigns the necessary bars from the images found
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

        //for cycle that assigns the necessary texts from the texts found
        for (int i = 0; i < texts.Length; i++) {
            switch (texts[i].name) {
                case "algaeText":
                    algaeText = texts[i];
                    break;
                case "smallFishText":
                    smallFishText = texts[i];
                    break;
                case "mediumFishText":
                    mediumFishText = texts[i];
                    break;
                case "bigFishText":
                    bigFishText = texts[i];
                    break;
            }
        }
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

        algaeText.text = $"Algae: {aiController.amountOfAlgae}";
        smallFishText.text = $"Small fish: {aiController.amountOfSmallFishes}";
        mediumFishText.text = $"Medium fish: {aiController.amountOfMediumFishes}";
        bigFishText.text = $"Big fish: {aiController.amountOfBigFishes}";
    }

    public void AlgaeSpeedChange() {
        float newSpeed;
        //for cycle that finds the algae speed change input and changes the value
        //according to it if its bigger that 0
        for (int i = 0; i < texts.Length; i++) {
            if (texts[i].transform.parent.name == "algaeSpeedInput" && texts[i].name == "Text") {
                if (float.TryParse(texts[i].text, out newSpeed)) {
                    if (newSpeed < 0) {
                        texts[i].text = $"{aiController.algaeSpeed}";
                        return;
                    }
                    aiController.algaeSpeed = newSpeed;
                    break;
                }
            }
        }
    }

    public void AlgaeSpawnChanceChange() {
        float newChance;
        //for cycle that finds the algae chance input and changes the value
        //according to it if its bigger that 0 and small that 1
        for (int i = 0; i < texts.Length; i++) {
            if (texts[i].transform.parent.name == "algaeSpawnChanceInput" && texts[i].name == "Text") {
                char[] inputArray = texts[i].text.ToCharArray();
                string input = "";
                for (int j = 0; j < texts[i].text.Length; j++) {
                    if (texts[i].text[j] == '.') inputArray[j] = ',';
                    input = new string(inputArray);
                }
                if (float.TryParse(input, out newChance)) {
                    if (newChance < 0) {
                        newChance = 0;
                        texts[i].text = $"{newChance}";
                    }
                    else if (newChance > 1) {
                        newChance = 1;
                        texts[i].text = $"{newChance}";
                    }
                    aiController.algaeSpawnChance = newChance;
                    break;
                }
            }
        }
    }

    public void SmallFishSpeedChange() {
        float newSpeed;
        //for cycle that finds the small fish speed change input and changes the value
        //according to it if its bigger that 0
        for (int i = 0; i < texts.Length; i++) {
            if (texts[i].transform.parent.name == "smallFishSpeedInput" && texts[i].name == "Text") {
                if (float.TryParse(texts[i].text, out newSpeed)) {
                    if (newSpeed < 0) {
                        texts[i].text = $"{aiController.smallFishSpeed}";
                        return;
                    }
                    aiController.smallFishSpeed = newSpeed;
                    break;
                }
            }
        }
    }

    public void MediumFishSpeedChange() {
        float newSpeed;
        //for cycle that finds the medium fish speed change input and changes the value
        //according to it if its bigger that 0
        for (int i = 0; i < texts.Length; i++) {
            if (texts[i].transform.parent.name == "mediumFishSpeedInput" && texts[i].name == "Text") {
                if (float.TryParse(texts[i].text, out newSpeed)) {
                    if (newSpeed < 0) {
                        texts[i].text = $"{aiController.mediumFishSpeed}";
                        return;
                    }
                    aiController.mediumFishSpeed = newSpeed;
                    break;
                }
            }
        }
    }

    public void BigFishSpeedChange() {
        float newSpeed;
        //for cycle that finds the big fish speed change input and changes the value
        //according to it if its bigger that 0
        for (int i = 0; i < texts.Length; i++) {
            if (texts[i].transform.parent.name == "bigFishSpeedInput" && texts[i].name == "Text") {
                if (float.TryParse(texts[i].text, out newSpeed)) {
                    if (newSpeed < 0) {
                        texts[i].text = $"{aiController.bigFishSpeed}";
                        return;
                    }
                    aiController.bigFishSpeed = newSpeed;
                    break;
                }
            }
        }
    }

    public void AlgaeEnergyValueChange() {
        float newEnergyValue;
        //for cycle that finds the algae energy value change input and changes the value
        //according to it if its bigger that 0
        for (int i = 0; i < texts.Length; i++) {
            if (texts[i].transform.parent.name == "algaeEnergyValueInput" && texts[i].name == "Text") {
                if (float.TryParse(texts[i].text, out newEnergyValue)) {
                    if (newEnergyValue < 0) {
                        texts[i].text = $"{aiController.algaeEnergeticValue}";
                        return;
                    }
                    aiController.algaeEnergeticValue = newEnergyValue;
                    break;
                }
            }
        }
    }

    public void SmallFishEnergyValueChange() {
        float newEnergyValue;
        //for cycle that finds the small fish energy value change input and changes the value
        //according to it if its bigger that 0
        for (int i = 0; i < texts.Length; i++) {
            if (texts[i].transform.parent.name == "smallFishEnergyValueInput" && texts[i].name == "Text") {
                if (float.TryParse(texts[i].text, out newEnergyValue)) {
                    if (newEnergyValue < 0) {
                        texts[i].text = $"{aiController.smallFishEnergeticValue}";
                        return;
                    }
                    aiController.smallFishEnergeticValue = newEnergyValue;
                    break;
                }
            }
        }
    }

    public void MediumFishEnergyValueChange() {
        float newEnergyValue;
        //for cycle that finds the medium fish energy value change input and changes the value
        //according to it if its bigger that 0
        for (int i = 0; i < texts.Length; i++) {
            if (texts[i].transform.parent.name == "mediumFishEnergyValueInput" && texts[i].name == "Text") {
                if (float.TryParse(texts[i].text, out newEnergyValue)) {
                    if (newEnergyValue < 0) {
                        texts[i].text = $"{aiController.mediumFishEnergeticValue}";
                        return;
                    }
                    aiController.mediumFishEnergeticValue = newEnergyValue;
                    break;
                }
            }
        }
    }

    public void SmallFishStartEnergyChange() {
        float newEnergyValue;
        //for cycle that finds the small fish energy start value change input and changes the value
        //according to it if its bigger that 0
        for (int i = 0; i < texts.Length; i++) {
            if (texts[i].transform.parent.name == "smallFishStartEnergyInput" && texts[i].name == "Text") {
                if (float.TryParse(texts[i].text, out newEnergyValue)) {
                    if (newEnergyValue < 0) {
                        texts[i].text = $"{aiController.smallFishStartEnergy}";
                        return;
                    }
                    aiController.smallFishStartEnergy = newEnergyValue;
                    break;
                }
            }
        }
    }

    public void MediumFishStartEnergyChange() {
        float newEnergyValue;
        //for cycle that finds the medium fish energy start value change input and changes the value
        //according to it if its bigger that 0
        for (int i = 0; i < texts.Length; i++) {
            if (texts[i].transform.parent.name == "mediumFishStartEnergyInput" && texts[i].name == "Text") {
                if (float.TryParse(texts[i].text, out newEnergyValue)) {
                    if (newEnergyValue < 0) {
                        texts[i].text = $"{aiController.mediumFishStartEnergy}";
                        return;
                    }
                    aiController.mediumFishStartEnergy = newEnergyValue;
                    break;
                }
            }
        }
    }

    public void BigFishStartEnergyChange() {
        float newEnergyValue;
        //for cycle that finds the big fish energy start value change input and changes the value
        //according to it if its bigger that 0
        for (int i = 0; i < texts.Length; i++) {
            if (texts[i].transform.parent.name == "bigFishStartEnergyInput" && texts[i].name == "Text") {
                if (float.TryParse(texts[i].text, out newEnergyValue)) {
                    if (newEnergyValue < 0) {
                        texts[i].text = $"{aiController.bigFishStartEnergy}";
                        return;
                    }
                    aiController.bigFishStartEnergy = newEnergyValue;
                    break;
                }
            }
        }
    }

    public void SmallFishReproduceEnergyChange() {
        float newEnergyValue;
        //for cycle that finds the small fish reproduce energy value change input and changes the value
        //according to it if its bigger that 0
        for (int i = 0; i < texts.Length; i++) {
            if (texts[i].transform.parent.name == "smallFishReproduceEnergyInput" && texts[i].name == "Text") {
                if (float.TryParse(texts[i].text, out newEnergyValue)) {
                    if (newEnergyValue < 0) {
                        texts[i].text = $"{aiController.smallFishReproduceEnergy}";
                        return;
                    }
                    aiController.smallFishReproduceEnergy = newEnergyValue;
                    break;
                }
            }
        }
    }

    public void MediumFishReproduceEnergyChange() {
        float newEnergyValue;
        //for cycle that finds the medium fish reproduce energy value change input and changes the value
        //according to it if its bigger that 0
        for (int i = 0; i < texts.Length; i++) {
            if (texts[i].transform.parent.name == "mediumFishReproduceEnergyInput" && texts[i].name == "Text") {
                if (float.TryParse(texts[i].text, out newEnergyValue)) {
                    if (newEnergyValue < 0) {
                        texts[i].text = $"{aiController.mediumFishReproduceEnergy}";
                        return;
                    }
                    aiController.mediumFishReproduceEnergy = newEnergyValue;
                    break;
                }
            }
        }
    }

    public void BigFishReproduceEnergyChange() {
        float newEnergyValue;
        //for cycle that finds the big fish reproduce energy value change input and changes the value
        //according to it if its bigger that 0
        for (int i = 0; i < texts.Length; i++) {
            if (texts[i].transform.parent.name == "bigFishReproduceEnergyInput" && texts[i].name == "Text") {
                if (float.TryParse(texts[i].text, out newEnergyValue)) {
                    if (newEnergyValue < 0) {
                        texts[i].text = $"{aiController.bigFishReproduceEnergy}";
                        return;
                    }
                    aiController.bigFishReproduceEnergy = newEnergyValue;
                    break;
                }
            }
        }
    }
}
