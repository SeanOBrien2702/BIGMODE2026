using UnityEngine;

public static class PositionHelper
{
    public static Vector3 GetMousePosition()
    {     
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldPoint.z = 0;
        return worldPoint;
    }
}
