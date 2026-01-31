using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUIController : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] GameObject pausePanel;
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] GameObject[] stars;

    [Header("Score Panel")]
    [SerializeField] TextMeshProUGUI shotsCountText;
    [SerializeField] TextMeshProUGUI parText;

    bool isGameOver = false;

    void Start()
    {
        pausePanel.SetActive(false);
        gameOverPanel.SetActive(false);
        TargetController.OnGameOver += TargetController_OnGameOver;
    }

    private void OnDestroy()
    {
        TargetController.OnGameOver -= TargetController_OnGameOver;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    void TogglePause()
    {
        if (isGameOver) return;
        pausePanel.SetActive(!pausePanel.activeSelf);
        if (pausePanel.activeSelf)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    private void TargetController_OnGameOver()
    {
        isGameOver = true;
        pausePanel.SetActive(false);
        gameOverPanel.SetActive(true);
    }

    internal void UpdateParDisplay(int currentShot, int parLevel, int parValue)
    {
        shotsCountText.text = "Shots: " + currentShot.ToString();
        parText.text = "Par: " + parValue.ToString();
        switch(parLevel)
        {
            case 0:
                parText.color = Color.green;
                break;
            case 1:
                parText.color = Color.white;
                break;
            case 2:
                parText.color = Color.yellow;
                break;
            default:
                parText.color = Color.red;
                break;
        }
    }

    public void ShowStars(int starCount)
    {
        for (int i = 0; i < stars.Length; i++)
        {
            stars[i].SetActive(i < starCount);
        }
    }

    public void NextLevel()
    {
        SceneController.Instance.LoadScene(LevelController.Instance.NextLevel());
    }

    public void RestartGameButton()
    {
        SceneController.Instance.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenuButton()
    {
        SceneController.Instance.LoadScene("MainMenu");
    }

    public void PauseButton()
    {
        TogglePause();
    }
}