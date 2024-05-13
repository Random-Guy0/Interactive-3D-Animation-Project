using UnityEngine;

public static class Vector3Extensions
{
    public static Vector3 Clamp(this Vector3 vector, Vector3 min, Vector3 max)
    {
        float x = Mathf.Clamp(vector.x, min.x, max.x);
        float y = Mathf.Clamp(vector.y, min.y, max.y);
        float z = Mathf.Clamp(vector.z, min.z, max.z);

        return new Vector3(x, y, z);
    }
}