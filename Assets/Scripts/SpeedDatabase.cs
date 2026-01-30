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
        for (int i = speedData.Length - 1; i > 0; i--)
        {
            if (speed >= speedData[i].SpeedThreshold)
            {
                if(!isTrail)
                {
                    return speedData[i].AimGradient;
                }
                return speedData[i].TrailGradient;
            }
        }
        if(!isTrail)
        {
            return speedData[0].AimGradient;
        }
        return speedData[0].TrailGradient;
    }
}