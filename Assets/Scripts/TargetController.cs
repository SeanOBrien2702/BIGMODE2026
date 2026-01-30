using System;
using UnityEngine;

public class TargetController : MonoBehaviour
{
    public static event Action OnGameOver = delegate { };
    int targetsRemaining = 0;
    void Start()
    {
        targetsRemaining = FindObjectsByType<Target>(FindObjectsSortMode.None).Length;
        Target.OnTargetHit += Target_OnTargetHit;
    }

    private void OnDestroy()
    {
        Target.OnTargetHit -= Target_OnTargetHit;       
    }

    private void Target_OnTargetHit(int hitsRemaining)
    {
        if(hitsRemaining > 0) return;        
        targetsRemaining--;
        if (targetsRemaining <= 0)
        {
            OnGameOver?.Invoke();
        }
    }
}
