using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    [SerializeField] Button button;
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] GameObject[] stars;

    internal void Initialize(int level)
    {
        bool unlocked = LevelController.Instance.IsLevelUnlocked(level);
        button.interactable = unlocked;
        levelText.text = "Level " + (level + 1).ToString();
        if (unlocked)
        {
            int starCount = LevelController.Instance.GetLevelStars(level);
            for (int i = 0; i < stars.Length; i++)
            {
                // Activate the star image if the index is less than stars earned
                stars[i].SetActive(i < starCount);
            }
        }
        else
        {
            // Hide stars if level is locked
            foreach (var star in stars)
            {
                star.SetActive(false);
            }
        }
    }

    void Start()
    {
        foreach (var item in stars)
        {
            item.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
