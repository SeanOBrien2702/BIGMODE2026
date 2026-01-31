using System;
using UnityEngine;

public class ParController : MonoBehaviour
{
    [SerializeField] GameUIController gameUIController;
    //TODO:Have this data stored in a ScriptableObject for easy tweaking
    [SerializeField] int[] starAmountPar;
    int currentShot = 0;

    void Start()
    {
        int currentPar = GetCurrentPar();
        gameUIController.UpdateParDisplay(currentShot, currentPar, starAmountPar[currentPar]);
        LaunchController.OnLaunched += LaunchController_OnLaunched;
        TargetController.OnGameOver += TargetController_OnGameOver;
    }

    private void OnDestroy()
    {
        LaunchController.OnLaunched -= LaunchController_OnLaunched;
        TargetController.OnGameOver -= TargetController_OnGameOver;
    }

    private void TargetController_OnGameOver()
    {
        Debug.Log("Level completed in " + currentShot + " shots.");
        int stars = GetStars();
        gameUIController.ShowStars(stars);
        LevelController.Instance.SaveLevelProgress(stars);
    }

    private void LaunchController_OnLaunched()
    {
        currentShot++;
        int currentPar = GetCurrentPar();
        int parAmount = starAmountPar[starAmountPar.Length - 1];
        if (currentPar < starAmountPar.Length)
        {
            parAmount = starAmountPar[currentPar];
        }
        gameUIController.UpdateParDisplay(currentShot, currentPar, parAmount);
    }

    private int GetCurrentPar()
    {
        for (int i = 0; i < starAmountPar.Length; i++)
        {
            if (currentShot <= starAmountPar[i])
            {
                return i;
            }
        }
        return starAmountPar.Length;
    }

    private int GetStars()
    {
        int stars = starAmountPar.Length;
        for (int i = 0; i < starAmountPar.Length; i++)
        {
            if (currentShot > starAmountPar[i])
            {
                stars--;
            }
        }
        return stars;
    }
}