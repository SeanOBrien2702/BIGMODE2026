using System;
using UnityEngine;
using UnityEngine.UI;

public class SettingsUIController : MonoBehaviour
{
    public static event Action<bool> OnShakeChange = delegate { };
    [SerializeField] GameObject panel;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider sfxSlider;
    [SerializeField] Toggle cameraShake;

    float musicVolume = 1f;
    float sfxVolume = 1f;
    bool isCameraShakeOn = true;

    public static SettingsUIController Instance;
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
        musicSlider.onValueChanged.AddListener(OnMusicVolumeChanged);
        sfxSlider.onValueChanged.AddListener(OnSFXVolumeChanged);
        cameraShake.onValueChanged.AddListener(OnCameraShakeToggled);

        musicVolume = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
        MusicManager.Main.SetVolume(musicVolume);
        musicSlider.value = musicVolume;

        sfxVolume = PlayerPrefs.GetFloat("SFXVolume", 0.5f);
        SFXManager.Main.SetVolume(sfxVolume);
        sfxSlider.value = sfxVolume;

        isCameraShakeOn = PlayerPrefs.GetInt("CameraShake", 1) == 1;
        cameraShake.isOn = isCameraShakeOn;
        panel.SetActive(false);
    }

    private void OnCameraShakeToggled(bool state)
    {
        isCameraShakeOn = state;
        OnShakeChange?.Invoke(isCameraShakeOn);
    }

    private void OnSFXVolumeChanged(float volume)
    {
        sfxVolume = volume;
        SFXManager.Main.SetVolume(sfxVolume);
    }

    private void OnMusicVolumeChanged(float volume)
    {
        musicVolume = volume;
        MusicManager.Main.SetVolume(musicVolume);
    }

    void OnApplicationFocus(bool focus)
    {
        Save();
    }

    void OnApplicationQuit()
    {
        Save();   
    }

    public void ResetButton()
    {
        PlayerPrefs.DeleteAll();
    }

    void Save()
    {
        PlayerPrefs.SetFloat("MusicVolume", musicVolume);
        PlayerPrefs.SetFloat("SFXVolume", sfxVolume);
        PlayerPrefs.SetInt("CameraShake", (isCameraShakeOn ? 1 : 0));
        PlayerPrefs.Save();
    }

    public bool ToggleSettings()
    {
        panel.SetActive(!panel.activeSelf);
        return panel.activeSelf;
    }

    public void ToggleSettingsButton()
    {
        panel.SetActive(!panel.activeSelf);
        Time.timeScale = panel.activeSelf ? 0f : 1f;
    }

    public void MainMenuButton()
    {
        SceneController.Instance.LoadScene("MainMenu");
    }
}