using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Dorkbots.ServiceLocatorTools;

// For anyone who reads this again, I am sorry haha. This might be the worst, most inefficient, and ugliest code I have ever written but I am
// too lazy to spend time making it better. It is super fast to do it this way and since I don't plan on adding levels its find haha. 

public class StoryCanvasManager : MonoBehaviour
{
    [SerializeField] private GameObject description_1;
    [SerializeField] private GameObject description_2;
    [SerializeField] private GameObject description_3;
    [SerializeField] private GameObject description_4;
    [SerializeField] private GameObject description_5;
    [SerializeField] private GameObject description_6;

    [SerializeField] private GameObject level1Button;
    [SerializeField] private GameObject level2Button;
    [SerializeField] private GameObject level3Button;
    [SerializeField] private GameObject level4Button;
    [SerializeField] private GameObject level5Button;
    [SerializeField] private GameObject level6Button;

    private ISaveManager saveManager;

    // Start is called before the first frame update
    void Start()
    {
        saveManager = ServiceLocator.Resolve<ISaveManager>();

        print(saveManager.saveData.levelsCompleted);

        // inits selected level
        level1Button.transform.GetChild(0).gameObject.SetActive(true);
        level2Button.transform.GetChild(0).gameObject.SetActive(false);
        level3Button.transform.GetChild(0).gameObject.SetActive(false);
        level4Button.transform.GetChild(0).gameObject.SetActive(false);
        level5Button.transform.GetChild(0).gameObject.SetActive(false);
        level6Button.transform.GetChild(0).gameObject.SetActive(false);

        // sets if the level is completed or not
        if (saveManager.saveData.levelsCompleted >= 1) {
            level1Button.transform.GetChild(1).gameObject.SetActive(true);
            level1Button.transform.GetChild(2).gameObject.SetActive(false);
        } else {
            level1Button.transform.GetChild(1).gameObject.SetActive(false);
            level1Button.transform.GetChild(2).gameObject.SetActive(true);
        }
        if (saveManager.saveData.levelsCompleted >= 2) {
            level2Button.transform.GetChild(1).gameObject.SetActive(true);
            level2Button.transform.GetChild(2).gameObject.SetActive(false);
        } else {
            level2Button.transform.GetChild(1).gameObject.SetActive(false);
            level2Button.transform.GetChild(2).gameObject.SetActive(true);
        }
        if (saveManager.saveData.levelsCompleted >= 3) {
            level3Button.transform.GetChild(1).gameObject.SetActive(true);
            level3Button.transform.GetChild(2).gameObject.SetActive(false);
        } else {
            level3Button.transform.GetChild(1).gameObject.SetActive(false);
            level3Button.transform.GetChild(2).gameObject.SetActive(true);
        }
        if (saveManager.saveData.levelsCompleted >= 4) {
            level4Button.transform.GetChild(1).gameObject.SetActive(true);
            level4Button.transform.GetChild(2).gameObject.SetActive(false);
        } else {
            level4Button.transform.GetChild(1).gameObject.SetActive(false);
            level4Button.transform.GetChild(2).gameObject.SetActive(true);
        }
        if (saveManager.saveData.levelsCompleted >= 5) {
            level5Button.transform.GetChild(1).gameObject.SetActive(true);
            level5Button.transform.GetChild(2).gameObject.SetActive(false);
        } else {
            level5Button.transform.GetChild(1).gameObject.SetActive(false);
            level5Button.transform.GetChild(2).gameObject.SetActive(true);
        }
        if (saveManager.saveData.levelsCompleted >= 6) {
            level6Button.transform.GetChild(1).gameObject.SetActive(true);
            level6Button.transform.GetChild(2).gameObject.SetActive(false);
        } else {
            level6Button.transform.GetChild(1).gameObject.SetActive(false);
            level6Button.transform.GetChild(2).gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Level_1_Selected() {
        // Have 6 different level descriptions game object that change
        description_1.SetActive(true);
        description_2.SetActive(false);
        description_3.SetActive(false);
        description_4.SetActive(false);
        description_5.SetActive(false);
        description_6.SetActive(false);

        // Sets the isSelected Ui element
        level1Button.transform.GetChild(0).gameObject.SetActive(true);
        level2Button.transform.GetChild(0).gameObject.SetActive(false);
        level3Button.transform.GetChild(0).gameObject.SetActive(false);
        level4Button.transform.GetChild(0).gameObject.SetActive(false);
        level5Button.transform.GetChild(0).gameObject.SetActive(false);
        level6Button.transform.GetChild(0).gameObject.SetActive(false);

        // set level 1 selected in save manager
        saveManager.saveData.storyLevelSelected = 1;
    }
    public void Level_2_Selected() {
        // Stops the user from playing levels out of order
        if (saveManager.saveData.levelsCompleted >= 1) {
            // Have 6 different level descriptions game object that change
            description_1.SetActive(false);
            description_2.SetActive(true);
            description_3.SetActive(false);
            description_4.SetActive(false);
            description_5.SetActive(false);
            description_6.SetActive(false);

            // Sets the isSelected Ui element
            level1Button.transform.GetChild(0).gameObject.SetActive(false);
            level2Button.transform.GetChild(0).gameObject.SetActive(true);
            level3Button.transform.GetChild(0).gameObject.SetActive(false);
            level4Button.transform.GetChild(0).gameObject.SetActive(false);
            level5Button.transform.GetChild(0).gameObject.SetActive(false);
            level6Button.transform.GetChild(0).gameObject.SetActive(false);

            // set level 1 selected in save manager
            saveManager.saveData.storyLevelSelected = 2;
        }
    }
    public void Level_3_Selected() {
        // Stops the user from playing levels out of order
        if (saveManager.saveData.levelsCompleted >= 2) {
            // Have 6 different level descriptions game object that change
            description_1.SetActive(false);
            description_2.SetActive(false);
            description_3.SetActive(true);
            description_4.SetActive(false);
            description_5.SetActive(false);
            description_6.SetActive(false);

            // Sets the isSelected Ui element
            level1Button.transform.GetChild(0).gameObject.SetActive(false);
            level2Button.transform.GetChild(0).gameObject.SetActive(false);
            level3Button.transform.GetChild(0).gameObject.SetActive(true);
            level4Button.transform.GetChild(0).gameObject.SetActive(false);
            level5Button.transform.GetChild(0).gameObject.SetActive(false);
            level6Button.transform.GetChild(0).gameObject.SetActive(false);

            // set level 1 selected in save manager
            saveManager.saveData.storyLevelSelected = 3;
        }
    }
    public void Level_4_Selected() {
        // Stops the user from playing levels out of order
        if (saveManager.saveData.levelsCompleted >= 3) {
            // Have 6 different level descriptions game object that change
            description_1.SetActive(false);
            description_2.SetActive(false);
            description_3.SetActive(false);
            description_4.SetActive(true);
            description_5.SetActive(false);
            description_6.SetActive(false);

            // Sets the isSelected Ui element
            level1Button.transform.GetChild(0).gameObject.SetActive(false);
            level2Button.transform.GetChild(0).gameObject.SetActive(false);
            level3Button.transform.GetChild(0).gameObject.SetActive(false);
            level4Button.transform.GetChild(0).gameObject.SetActive(true);
            level5Button.transform.GetChild(0).gameObject.SetActive(false);
            level6Button.transform.GetChild(0).gameObject.SetActive(false);

            // set level 1 selected in save manager
            saveManager.saveData.storyLevelSelected = 4;
        }
    }
    public void Level_5_Selected() {
        // Stops the user from playing levels out of order
        if (saveManager.saveData.levelsCompleted >= 4) {
            // Have 6 different level descriptions game object that change
            description_1.SetActive(false);
            description_2.SetActive(false);
            description_3.SetActive(false);
            description_4.SetActive(false);
            description_5.SetActive(true);
            description_6.SetActive(false);

            // Sets the isSelected Ui element
            level1Button.transform.GetChild(0).gameObject.SetActive(false);
            level2Button.transform.GetChild(0).gameObject.SetActive(false);
            level3Button.transform.GetChild(0).gameObject.SetActive(false);
            level4Button.transform.GetChild(0).gameObject.SetActive(false);
            level5Button.transform.GetChild(0).gameObject.SetActive(true);
            level6Button.transform.GetChild(0).gameObject.SetActive(false);

            // set level 1 selected in save manager
            saveManager.saveData.storyLevelSelected = 5;
        }
    }
    public void Level_6_Selected() {
        // Stops the user from playing levels out of order
        if (saveManager.saveData.levelsCompleted >= 5) {
            // Have 6 different level descriptions game object that change
            description_1.SetActive(false);
            description_2.SetActive(false);
            description_3.SetActive(false);
            description_4.SetActive(false);
            description_5.SetActive(false);
            description_6.SetActive(true);

            // Sets the isSelected Ui element
            level1Button.transform.GetChild(0).gameObject.SetActive(false);
            level2Button.transform.GetChild(0).gameObject.SetActive(false);
            level3Button.transform.GetChild(0).gameObject.SetActive(false);
            level4Button.transform.GetChild(0).gameObject.SetActive(false);
            level5Button.transform.GetChild(0).gameObject.SetActive(false);
            level6Button.transform.GetChild(0).gameObject.SetActive(true);

            // set level 1 selected in save manager
            saveManager.saveData.storyLevelSelected = 6;
        }
    }

    public void PlayButton() {
        // Sets game mode to story

        if (saveManager.saveData.storyLevelSelected == 1) {
            SceneManager.LoadScene("Lv_1_Neighborhood");
        } else if (saveManager.saveData.storyLevelSelected == 2) {
            SceneManager.LoadScene("Lv_2_Highway");
        } else if (saveManager.saveData.storyLevelSelected == 3) {
            SceneManager.LoadScene("Lv_3_Downtown");
        } else if (saveManager.saveData.storyLevelSelected == 4) {
            SceneManager.LoadScene("Lv_4_Beach");
        } else if (saveManager.saveData.storyLevelSelected == 5) {
            SceneManager.LoadScene("Lv_5_ShoppingCenter");
        } else if (saveManager.saveData.storyLevelSelected == 6) {
            SceneManager.LoadScene("Lv_6_Bomb");
        } else {
            print("Error: Level is non-existant");
        }

        saveManager.saveData.gameMode = "Story";
    }
}
