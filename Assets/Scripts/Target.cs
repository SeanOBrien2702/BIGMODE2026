using System;
using UnityEngine;

public class Target : MonoBehaviour
{
    public static event Action<int> OnTargetHit = delegate { };
    [SerializeField] int hits = 1;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        hits--;
        OnTargetHit?.Invoke(hits);
        if (hits <= 0)
        {
            Destroy(gameObject);
        }
    }
}