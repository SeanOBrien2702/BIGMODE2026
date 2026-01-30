using UnityEngine;

public class TrailController : MonoBehaviour
{
    ParticleSystem particleSystem;
    TrailRenderer trailRenderer;
    private void Start()
    {
        particleSystem = GetComponentInChildren<ParticleSystem>();
        trailRenderer = GetComponentInChildren<TrailRenderer>();
    }

    internal void SetSpeed(float speed)
    {
        Gradient gradient = SpeedDatabase.instance.GetGradientForSpeed(speed, true);
        trailRenderer.colorGradient = gradient;
        ParticleSystem.ColorOverLifetimeModule col = particleSystem.colorOverLifetime;
        col.color = gradient;
    }
}