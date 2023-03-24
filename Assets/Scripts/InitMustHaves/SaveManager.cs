using UnityEngine;

public class SaveManager : MonoBehaviour, ISaveManager
{
    public SaveData saveData {get; set;}

    // Save the whole state of this saveState script to the player
    public void Save() {
        Debug.Log("Saving Data Now");
        PlayerPrefs.SetString("save", SaveHelper.Serialize<SaveData>(saveData));
    }

    // Load the previous saved state from the player prefs
    public void Load() {
        if (PlayerPrefs.HasKey("save")) {
            Debug.Log("already saved");
            saveData = SaveHelper.Deserialize<SaveData>(PlayerPrefs.GetString("save"));
        } else {
            saveData = new SaveData();
            Debug.Log("No save file found, creating new one");
        }
    }

    public void DeleteSavedData() {
        if (PlayerPrefs.HasKey("save")) {
            Debug.Log("Key Found, Deleteing Now");
            PlayerPrefs.DeleteKey("save");
        }
    }
}
