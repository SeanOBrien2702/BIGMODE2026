using TMPro;
using UnityEngine;

public class TooltipUI : MonoBehaviour
{
    public static TooltipUI Instance;
    [SerializeField] GameObject panel;
    [SerializeField] TextMeshProUGUI moveableText;
    [SerializeField] TextMeshProUGUI healthText;

    void Start()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        panel.SetActive(false);
    }
    
    public void ShowTooltip(Target target)
    {
        panel.SetActive(true);
        moveableText.text = target.CanMove ? "Can move" : "Can't move";
        healthText.text = $"Health: {target.Health}";
    }

    public void HideTooltip()
    {
        panel.SetActive(false);
    }
}