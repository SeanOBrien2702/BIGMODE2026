using UnityEngine;

public class MainMenuUIController : MonoBehaviour
{
    [SerializeField] LevelButton[] levelButtons;

    [Header("UI Elements")]
    [SerializeField] GameObject menuPanel;
    [SerializeField] GameObject levelSelectPanel;
    [SerializeField] GameObject tutorialPanel;
    [SerializeField] GameObject creditsPanel;

    void Start()
    {
        SelectPanel(menuPanel);
        for (int i = 0; i < levelButtons.Length; i++)
        {
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