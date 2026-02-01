using UnityEngine;

public class MainMenuUIController : MonoBehaviour
{
    [SerializeField] LevelButton[] levelButtons;

    [Header("UI Elements")]
    [SerializeField] GameObject menuPanel;
    [SerializeField] GameObject levelSelectPanel;
    [SerializeField] GameObject tutorialPanel;
    [SerializeField] GameObject settingsPanel;
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
        settingsPanel.SetActive(false);
        creditsPanel.SetActive(false);

        panel.SetActive(true);
    }

    public void ResetButton()
    {
        PlayerPrefs.DeleteAll();
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

    public void SettingsButton()
    {
        SelectPanel(settingsPanel);
    }

    public void CreditsButton()
    {
        SelectPanel(creditsPanel);
    }
}