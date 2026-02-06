using Febucci.TextAnimatorForUnity;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    [SerializeField] MainMenuUIController uIController;
    [SerializeField] TypewriterComponent textAnimatorPlayer;

    [TextArea(3, 50), SerializeField]
    string[] textToShow;
    int index = 0;

    private void OnEnable()
    {
        index = 0;
        ShowText();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (textAnimatorPlayer.IsShowingText)
            {
                textAnimatorPlayer.SkipTypewriter();
            }
            else
            {
                index++;
                if (index >= textToShow.Length)
                {
                    uIController.MenuButton();
                }
                else
                {
                    ShowText();
                }
            }
        }
    }

    public void ShowText()
    {
        textAnimatorPlayer.ShowText(textToShow[index]);  
    }
}