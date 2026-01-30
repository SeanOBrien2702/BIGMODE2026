using UnityEngine;

[CreateAssetMenu(fileName = "SpeedData", menuName = "Scriptable Objects/SpeedData")]
public class SpeedData : ScriptableObject
{
    public float SpeedThreshold;
    public Gradient TrailGradient;
    public Gradient AimGradient;
}
