using UnityEngine;

public class AudioController : MonoBehaviour
{
    [Header("Music")]
    [SerializeField] Track mainMenuTrack;
    [SerializeField] Track[] levelTracks;
    int currentTrack = 0;

    void Start()
    {
        MusicManager.Main.Play(mainMenuTrack);
        SceneController.OnStartLoadNextScene += SceneController_OnStartLoadNextScene;
    }

    private void OnDestroy()
    {
        SceneController.OnStartLoadNextScene -= SceneController_OnStartLoadNextScene;
    }

    private void SceneController_OnStartLoadNextScene(string sceneName)
    {
        int level = LevelController.Instance.CurrentLevel;
        if (sceneName == "MainMenu")
        {
            MusicManager.Main.Play(mainMenuTrack, 1, 1);
        }
        else 
        {
            if (level < 4)
            {
                currentTrack = 0;
            }
            else if (level < 7)
            {
                currentTrack = 1;
            }
            else if (level < 10)
            { 
                currentTrack = 2;
            }
            else
            {
                currentTrack = 3;
            }
            MusicManager.Main.Play(levelTracks[currentTrack], 1, 1);
        }
    }
}