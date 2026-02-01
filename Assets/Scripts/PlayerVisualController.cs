using System;
using System.Collections;
using UnityEngine;

public class PlayerVisualController : MonoBehaviour
{
    [SerializeField] SpriteRenderer sprite;
    [SerializeField, Range(0.01f, 0.06f)] float scaleFactor = 0.03f; 
    [SerializeField, Range(0.1f, 1f)] float resizeDuration = 0.5f;
    float magnitude;
    LaunchController launchController;
    Vector2 lastDirection;

    void Start()
    {
        launchController = GetComponent<LaunchController>();
        LaunchController.OnLaunched += LaunchController_OnLaunched;
    }

    private void OnDestroy()
    {
        LaunchController.OnLaunched -= LaunchController_OnLaunched;
    }

    void Update()
    {
        magnitude = launchController.GetLaunchForce().magnitude;
        if (launchController.IsMoving)
        {
            lastDirection = launchController.Rb.linearVelocity;
            float angle = Mathf.Atan2(lastDirection.y, lastDirection.x) * Mathf.Rad2Deg;
            sprite.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 180));
        }
        else if (magnitude > 0)
        {
            float angle = Mathf.Atan2(launchController.DragVector.y, launchController.DragVector.x) * Mathf.Rad2Deg;
            sprite.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 180));
            Vector3 scale = new Vector3(1 + magnitude * scaleFactor, 1 - magnitude * scaleFactor, 1);
            sprite.transform.localScale = scale;
        }   
    }

    void LaunchController_OnLaunched()
    {
        StartCoroutine(ResetScale(resizeDuration));
    }

    IEnumerator ResetScale(float duration)
    {
        float time = 0;
        Vector3 startScale = sprite.transform.localScale;
        while (time < duration)
        {
            sprite.transform.localScale = Vector3.Lerp(startScale, Vector3.one, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        sprite.transform.localScale = Vector3.one;;
    }
}