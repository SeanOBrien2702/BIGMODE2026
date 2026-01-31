using UnityEngine;

public class LevelController : MonoBehaviour
{
    public static LevelController Instance;
    [SerializeField] int totalLevels = 10;
    int currentLevel = 0;

    public int CurrentLevel { get => currentLevel; set => currentLevel = value; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SaveLevelProgress(int stars)
    {
        int currentStars = PlayerPrefs.GetInt("Level" + currentLevel + "Stars", 0);
        if (stars > currentStars)
        {
            PlayerPrefs.SetInt("Level" + currentLevel + "Stars", stars);
        }
        Debug.Log("Level " + currentLevel + " completed with " + stars + " stars.");
        PlayerPrefs.SetInt("Level" + (currentLevel + 1) + "Unlocked", 1);
        PlayerPrefs.Save();
    }

    public int GetLevelStars(int levelIndex)
    {
        return PlayerPrefs.GetInt("Level" + levelIndex + "Stars", 0);
    }

    public bool IsLevelUnlocked(int levelIndex)
    {
        if (levelIndex == 0) return true;
        Debug.Log("Level " + levelIndex + " unlocked: " + (PlayerPrefs.GetInt("Level" + levelIndex + "Unlocked", 0) == 1));
        return PlayerPrefs.GetInt("Level" + levelIndex + "Unlocked", 0) == 1;
    }

    public string NextLevel()
    {
        currentLevel++;
        if (currentLevel >= totalLevels)
        {
            return "MainMenu";
        }
        return "Level" + (currentLevel + 1).ToString();
    }
}