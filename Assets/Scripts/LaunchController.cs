using Unity.Mathematics;
using UnityEngine;

public class LaunchController : MonoBehaviour
{
    Rigidbody2D rb;
    Vector3 startPos;
    Vector3 endPos;
    public Vector3 dragVector;

    LineRenderer lineRenderer;
    Vector3 lineRendererStart;
    float lineStartOffset = -0.9f;
    [SerializeField] float powerMultiplier = 1;
    [SerializeField] float maxLineLength = 5;
    [SerializeField] Transform arrow;
   
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        lineRenderer = GetComponentInChildren<LineRenderer>();
        ToggleAimVisuals(false);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startPos = PositionHelper.GetMousePosition();
            ToggleAimVisuals(true);
        }
        if (Input.GetMouseButton(0))
        {
            endPos = PositionHelper.GetMousePosition();
            dragVector = startPos - endPos;
            DrawLine();
        }
        if (Input.GetMouseButtonUp(0))
        {
            ToggleAimVisuals(false);
            rb.AddForce(Vector3.ClampMagnitude(dragVector, maxLineLength) * powerMultiplier, ForceMode2D.Impulse);
        }
    }

    void ToggleAimVisuals(bool toggle)
    {
        lineRenderer.enabled = toggle;
        arrow.gameObject.SetActive(toggle);
    }

    void DrawLine()
    {
        float angle = Mathf.Atan2(dragVector.y, dragVector.x) * Mathf.Rad2Deg - 90;
        arrow.rotation = Quaternion.Euler(0, 0, angle);
        arrow.position = transform.position + -dragVector.normalized * lineStartOffset;
        lineRendererStart = dragVector.normalized * lineStartOffset;
        lineRenderer.SetPosition(0, lineRendererStart);
        lineRenderer.SetPosition(1, lineRendererStart + lineRendererStart * Mathf.Min(maxLineLength,  dragVector.magnitude));
    }
}