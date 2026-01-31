using UnityEngine;

public class DragIndicator : MonoBehaviour
{
    [SerializeField] LaunchController launchController;
    LineRenderer lineRenderer;
    SpriteRenderer indicator;
    Vector3 endPos;

    void Start()
    {
        lineRenderer = GetComponentInChildren<LineRenderer>();  
        indicator = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            ToggleAimVisuals(false);
        }
        if (!launchController.CanShoot()) return;
        if (Input.GetMouseButtonDown(0))
        {
            transform.position = PositionHelper.GetMousePosition();
            ToggleAimVisuals(true);
        }
        if (Input.GetMouseButton(0))
        {
            endPos = PositionHelper.GetMousePosition();
            DrawLine();
        }
        if (Input.GetMouseButtonUp(0))
        {
            ToggleAimVisuals(false);
        }
    }

    void ToggleAimVisuals(bool toggle)
    {
        lineRenderer.enabled = toggle;
        indicator.enabled = toggle;
    }

    void DrawLine()
    {
        lineRenderer.SetPosition(0, Vector3.zero);
        lineRenderer.SetPosition(1, endPos - transform.localPosition);
    }
}
