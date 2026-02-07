using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    [SerializeField] Button button;
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] GameObject[] stars;

    internal void Initialize(int level)
    {
        for (int i = 0; i < stars.Length; i++)
        {
            stars[i].SetActive(false);
        }
        bool unlocked = LevelController.Instance.IsLevelUnlocked(level);
        button.interactable = unlocked;
        levelText.text = "Level " + (level + 1).ToString();
        if (unlocked)
        {
            int starCount = LevelController.Instance.GetLevelStars(level);
            for (int i = 0; i < stars.Length; i++)
            {
                stars[i].SetActive(i < starCount);
            }
            button.onClick.AddListener(() => LoadLevel(level));
        }
    }

    void LoadLevel(int level)
    {
        LevelController.Instance.CurrentLevel = level;
        SceneController.Instance.LoadScene("Level" + (level + 1).ToString());
    }
}