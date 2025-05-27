using UnityEngine;

public static class MyMath
{
    public static Vector3 RandomVector(Vector3 v, float range)
    {
        Vector3 newV = new Vector3(v.x + Random.Range(-range, range) , v.y + Random.Range(-range, range), v.z);
        return newV;
    }
    public static Vector2 RandomVector(Vector2 v, float range)
    {
        Vector2 newV = new Vector2(v.x + Random.Range(-range, range), v.y + Random.Range(-range, range));
        return newV;
    }
    public static Vector2 RandomVector(Vector2 v, float rangeX, float rangeY, float offsetX = 0, float offsetY = 0)
    {
        Vector2 newV = new Vector2(v.x + Random.Range(-rangeX + offsetX, rangeX + offsetX), v.y + Random.Range(-rangeY + offsetY, rangeY + offsetY));
        return newV;
    }

    public static float GetAngleBetweenObjects(Transform from, Transform to)
    {
        //Caculate angle from Ox
        Vector2 direction = to.position - from.position;
        return Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
    }
    public static float GetAngleBetweenObjects(Vector3 from, Vector3 to)
    {
        //Caculate angle from Ox
        Vector2 direction = to - from;
        return Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
    }
    public static float RoundToDecimals(float x, int m)
    {
        float factor = Mathf.Pow(10, m);
        return Mathf.Round(x * factor) / factor;
    }

    public static Vector2 GetSymmetricOX(Vector2 point)
    {
        return new Vector2(point.x, -point.y);
    }
    public static Vector2 GetSymmetricOY(Vector2 point)
    {
        return new Vector2(-point.x, point.y);
    }
    public static Vector2 GetSymmetricPoint(Vector2 point, Vector2 center)
    {
        return new Vector2(2 * center.x - point.x, 2 * center.y - point.y);
    }

    public static Vector2 GetMiddleVector(Vector2 pointA, Vector2 pointB) 
    {
        return (pointA + pointB)/2;
    }
    public static string FormatTime(float timeInSeconds)
    {
        int minutes = Mathf.FloorToInt(timeInSeconds / 60);
        int seconds = Mathf.RoundToInt(timeInSeconds % 60);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    public static Vector2 GetNormal(Vector2 pointA, Vector2 pointB)
    {
        Vector2 direction = (pointB - pointA).normalized;
        return new Vector2(-direction.y, direction.x); 
    }

}
