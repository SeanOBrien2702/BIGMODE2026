using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    [SerializeField] SFXGroup slapSFX;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        SFXManager.Main.Play(slapSFX);
    }
}