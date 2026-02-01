using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class LaunchController : MonoBehaviour
{
    public static event Action OnLaunched = delegate { };
    Rigidbody2D rb;
    Vector3 startPos;
    Vector3 endPos;
    Vector3 dragVector;
    [SerializeField] float stopVelocityThreshold = 0.25f;

    LineRenderer lineRenderer;
    Vector3 lineRendererStart;
    float lineStartOffset = -0.9f;
    [SerializeField] float powerMultiplier = 1;
    [SerializeField] float maxLineLength = 5;
    [SerializeField] Transform arrow;
    bool hasCancelled = false;
    bool isGameOver = false;
    bool isMoving = false;

    TrailController trailController;
    AudioSource audio;

    public Vector3 DragVector { get => dragVector; }
    public bool IsMoving { get => isMoving; }
    public Rigidbody2D Rb { get => rb; }

    private void Start()
    {
        audio = GetComponent<AudioSource>();
        trailController = GetComponent<TrailController>();
        rb = GetComponent<Rigidbody2D>();
        lineRenderer = GetComponentInChildren<LineRenderer>();
        ToggleAimVisuals(false);
        TargetController.OnGameOver += TargetController_OnGameOver;
    }

    private void OnDestroy()
    {
        TargetController.OnGameOver -= TargetController_OnGameOver;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            CancelShot();
        }
        if (isMoving && rb.linearVelocity.magnitude < stopVelocityThreshold)
        {
            Stop();
        }
        if (!CanShoot()) return;
        if (Input.GetMouseButtonDown(0))
        {
            StartlaunchDrag();
        }
        if (Input.GetMouseButton(0) )
        {         
            endPos = PositionHelper.GetMousePosition();
            dragVector = startPos - endPos;
            
            DrawLine();
        }
        if (Input.GetMouseButtonUp(0) && !hasCancelled)
        {
            Launch();
        }
    }

    void StartlaunchDrag()
    {
        audio.Play();
        hasCancelled = false;
        startPos = PositionHelper.GetMousePosition();
        ToggleAimVisuals(true);
    }

    void CancelShot()
    {
        startPos = Vector3.zero;
        endPos = Vector3.zero;
        ToggleAimVisuals(false);
        hasCancelled = true;
    }

    void Stop()
    {
        rb.linearVelocity = Vector3.zero;
        isMoving = false;       
        if (Input.GetMouseButton(0))
        {
            StartlaunchDrag();
        }
    }

    public bool CanShoot()
    {       
        return Time.timeScale == 1 &&
               rb.linearVelocity.magnitude == 0 &&
               !isGameOver &&
               !isMoving &&
               !EventSystem.current.IsPointerOverGameObject(); 
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
        lineRenderer.colorGradient = SpeedDatabase.instance.GetGradientForSpeed(GetLaunchForce().magnitude, false);
    }

    void Launch()
    {       
        ToggleAimVisuals(false);
        Vector3 lauchForce = GetLaunchForce();
        rb.AddForce(lauchForce, ForceMode2D.Impulse);
        trailController.SetSpeed(lauchForce.magnitude);
        isMoving = true;
        dragVector = Vector3.zero;
        OnLaunched?.Invoke();
    }

    private void TargetController_OnGameOver()
    {
        isGameOver = true;
    }

    public Vector3 GetLaunchForce()
    {
        return Vector3.ClampMagnitude(dragVector, maxLineLength) * powerMultiplier;
    }
}