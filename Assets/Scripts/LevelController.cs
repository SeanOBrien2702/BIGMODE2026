using UnityEngine;

public class LevelController : MonoBehaviour
{
    public static LevelController Instance;
    int currentLevel = 0;

    void Awake()
    {
        Debug.Log("LevelController Awake");
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    
    // Call this when a player finishes a level
    public void SaveLevelProgress(int levelIndex, int stars)
    {
        // Save stars if the new score is higher than the old one
        int currentStars = PlayerPrefs.GetInt("Level_" + levelIndex + "_Stars", 0);
        if (stars > currentStars)
        {
            PlayerPrefs.SetInt("Level_" + levelIndex + "_Stars", stars);
        }

        // Unlock the next level
        PlayerPrefs.SetInt("Level_" + (levelIndex + 1) + "_Unlocked", 1);
        PlayerPrefs.Save();
    }

    public int GetLevelStars(int levelIndex)
    {
        return PlayerPrefs.GetInt("Level_" + levelIndex + "_Stars", 0);
    }

    public bool IsLevelUnlocked(int levelIndex)
    {
        // Level 1 is always unlocked by default
        if (levelIndex == 0) return true;
        return PlayerPrefs.GetInt("Level_" + levelIndex + "_Unlocked", 0) == 1;
    }
}
