using UnityEngine;

public class AudioController : MonoBehaviour
{
    [Header("Music")]
    [SerializeField] Track mainMenuTrack;
    [SerializeField] Track[] levelTracks;
    int currentTrack = -1;

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
            if(level % 3 == 0)
            {
                currentTrack++;
            }
            MusicManager.Main.Play(levelTracks[currentTrack], 1, 1);
        }
    }
}