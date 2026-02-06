using System;
using System.Collections;
using TransitionsPlus;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static event Action<string> OnStartLoadNextScene = delegate { };
    public static SceneController Instance;
    [SerializeField] TransitionAnimator animator;
    [SerializeField] SFXObject transitionOutSFX;
    [SerializeField] SFXObject transitionInSFX;

    bool isFirstTime = true;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        animator.progress = 0;
        animator.profile.invert = false;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void LoadScene(string sceneName)
    {
        OnStartLoadNextScene?.Invoke(sceneName);
        animator.profile.invert = false;
        animator.Play();
        StartCoroutine(StartLoad(sceneName));
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (isFirstTime)
        {
            isFirstTime = false;
            return;
        }
        animator.profile.invert = true;
        animator.Play();
    }

    IEnumerator StartLoad(string sceneName)
    {
        SFXManager.Main.Play(transitionOutSFX);
        yield return new WaitForSecondsRealtime(animator.profile.duration);
        SFXManager.Main.Play(transitionInSFX);
        SceneManager.LoadScene(sceneName);
        Time.timeScale = 1;
    }
}