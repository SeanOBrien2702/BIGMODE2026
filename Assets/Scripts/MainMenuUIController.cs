using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUIController : MonoBehaviour
{
    [SerializeField] LevelButton[] levelButtons;

    [Header("UI Elements")]
    [SerializeField] GameObject menuPanel;
    [SerializeField] GameObject levelSelectPanel;
    [SerializeField] GameObject tutorialPanel;
    [SerializeField] GameObject creditsPanel;

    [Header("Score Panel")]
    [SerializeField] TextMeshProUGUI shotsCountText;
    [SerializeField] TextMeshProUGUI parText;

    bool isGameOver = false;

    void Start()
    {
        Debug.Log("MainMenuUIController Start");
        SelectPanel(menuPanel);
        for (int i = 0; i < levelButtons.Length; i++)
        {
            //int stars = LevelController.instance.GetLevelStars(i + 1);
            //levelButtons[i].SetStars(stars);
            //bool isUnlocked = LevelController.instance.IsLevelUnlocked(i + 1);
            levelButtons[i].Initialize(i);
        }
    }

    void SelectPanel(GameObject panel)
    {
        menuPanel.SetActive(false);
        levelSelectPanel.SetActive(false);
        tutorialPanel.SetActive(false);
        creditsPanel.SetActive(false);

        panel.SetActive(true);
    }

    public void MenuButton()
    {
        SelectPanel(menuPanel);
    }

    public void LevelButton()
    {
        SelectPanel(levelSelectPanel);
    }

    public void TutorialButton()
    {
        SelectPanel(tutorialPanel);
    }

    public void CreditsButton()
    {
        SelectPanel(creditsPanel);
    }
}
