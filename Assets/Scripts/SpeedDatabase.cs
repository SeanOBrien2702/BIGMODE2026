using UnityEngine;

public class SpeedDatabase : MonoBehaviour
{
    public static SpeedDatabase instance;
    [SerializeField] SpeedData[] speedData;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public Gradient GetGradientForSpeed(float speed, bool isTrail)
    {
        int speedLevel = GetSpeedLevel(speed);
        if (!isTrail)
        {
            return speedData[speedLevel].AimGradient;
        }
        return speedData[speedLevel].TrailGradient;
    }

    public int GetDamageForSpeed(float speed)
    {
        int speedLevel = GetSpeedLevel(speed);
        return speedData[speedLevel].SpeedDamage;
    }

    public int GetSpeedLevel(float speed)
    {
        for (int i = speedData.Length - 1; i > 0; i--)
        {
            if (speed >= speedData[i].SpeedThreshold)
            {
                return i;
            }
        }
        return 0;
    }
}