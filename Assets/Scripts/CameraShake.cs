using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] LaunchController launchController;
    [SerializeField] float shakeDuration = 0.5f;
    [SerializeField] float shakeMagnitude = 0.1f;
    bool shakeEnabled = true;

    void Start()
    {
        LaunchController.OnLaunched += LaunchController_OnLaunched;
        SettingsUIController.OnShakeChange += SettingsUIController_OnShakeChange;
    }

    private void OnDestroy()
    {
        LaunchController.OnLaunched -= LaunchController_OnLaunched;
        SettingsUIController.OnShakeChange -= SettingsUIController_OnShakeChange;
    }

    private void LaunchController_OnLaunched()
    {
        if (shakeEnabled)
        {
            StartCoroutine(Shake(shakeDuration, shakeMagnitude));
        }    
    }

    private void SettingsUIController_OnShakeChange(bool isEnabled)
    {
        shakeEnabled = isEnabled;
    }

    private IEnumerator Shake(float duration, float magnitude)
    {
        yield return new WaitForFixedUpdate();
        magnitude *= launchController.GetSpeedLevel();
        Vector3 originalPos = transform.localPosition;
        float elapsed = 0.0f;
        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(originalPos.x + x, originalPos.y + y, originalPos.z);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = originalPos;
    }
}