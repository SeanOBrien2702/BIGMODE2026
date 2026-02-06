using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonAudio : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] SFXObject hoverSound;
    [SerializeField] SFXObject pressedSound;
    Button button;

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        if (button.interactable)
        {
            SFXManager.Main.Play(pressedSound);
        }
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        if (button.interactable)
        {
            SFXManager.Main.Play(hoverSound);
        }
    }
}