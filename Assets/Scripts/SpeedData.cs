using UnityEngine;

[CreateAssetMenu(fileName = "SpeedData", menuName = "Scriptable Objects/SpeedData")]
public class SpeedData : ScriptableObject
{
    public int SpeedDamage;
    public float SpeedThreshold;
    public Gradient TrailGradient;
    public Gradient AimGradient;
}
