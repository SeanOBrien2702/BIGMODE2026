using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUIController : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] GameObject gameOverPanel;

    [Header("Score Panel")]
    [SerializeField] TextMeshProUGUI shotsCountText;
    [SerializeField] TextMeshProUGUI parText;

    void Start()
    {
        gameOverPanel.SetActive(false);
        TargetController.OnGameOver += TargetController_OnGameOver;
    }

    private void OnDestroy()
    {
        TargetController.OnGameOver -= TargetController_OnGameOver;
    }

    private void TargetController_OnGameOver()
    {
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

    public void RestartGameButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}