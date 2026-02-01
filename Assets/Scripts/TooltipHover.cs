using UnityEngine;

public class TooltipHover : MonoBehaviour
{
    Target target;
    private void Awake()
    {
        target = GetComponent<Target>();
    }

    private void OnMouseEnter()
    {
        TooltipUI.Instance.ShowTooltip(target);
    }

    private void OnMouseExit()
    {
        TooltipUI.Instance.HideTooltip();
    }
}