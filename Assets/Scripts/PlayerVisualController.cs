using System;
using System.Collections;
using UnityEngine;

public class PlayerVisualController : MonoBehaviour
{
    [SerializeField] SpriteRenderer sprite;
    [SerializeField] Sprite[] animationSprites;
    float scaleFactor = 0.02f; 
    float resizeDuration = 0f;
    float magnitude;
    LaunchController launchController;
    Vector2 lastDirection;
    float maxForce;
    float currentForce;
    Animator animator;
    //float sizeScale = 0.2f;

    void Start()
    {
        launchController = GetComponent<LaunchController>();
        LaunchController.OnLaunched += LaunchController_OnLaunched;
        LaunchController.OnCancel += LaunchController_OnCancel;
        maxForce = launchController.GetMaxForce();
        animator = GetComponent<Animator>();
    }

    private void OnDestroy()
    {
        LaunchController.OnLaunched -= LaunchController_OnLaunched;
        LaunchController.OnCancel -= LaunchController_OnCancel;
    }

    void Update()
    {
        magnitude = launchController.GetLaunchForce().magnitude;
        if(launchController.IsAiming())
        {
            float angle = Mathf.Atan2(launchController.DragVector.y, launchController.DragVector.x) * Mathf.Rad2Deg;
            sprite.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 90));
            Vector3 scale = new Vector3(1, 1 + magnitude * scaleFactor, 1 - magnitude * scaleFactor);
            sprite.transform.localScale = scale;
            currentForce = magnitude / maxForce;
        }

        if (launchController.IsMoving)
        {
            lastDirection = launchController.Rb.linearVelocity;
            float angle = Mathf.Atan2(lastDirection.y, lastDirection.x) * Mathf.Rad2Deg;
            sprite.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 90));
            animator.SetBool("IsRolling", true);
            animator.SetFloat("Speed", lastDirection.magnitude);
        }
        else
        {
            animator.SetBool("IsRolling", false);            
        }
    }

    void LaunchController_OnCancel()
    {
        StartCoroutine(ResetScale(resizeDuration));
    }

    void LaunchController_OnLaunched()
    {
        StartCoroutine(ResetScale(resizeDuration));
    }

    IEnumerator ResetScale(float duration)
    {
        float time = 0;
        Vector3 startScale = sprite.transform.localScale;
        float force = 0;
        while (time < duration)
        {
            sprite.transform.localScale = Vector3.Lerp(startScale, Vector3.one, time / duration);
            force = Mathf.Lerp(currentForce, 0, time / duration);
            animator.SetFloat("Power", force);
            time += Time.deltaTime;
            yield return null;
        }
        animator.SetFloat("Power", 0);
        sprite.transform.localScale = Vector3.one;
    }
}