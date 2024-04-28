using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RandomUtils
{
    public static Vector3 RandomVector3(Vector3 minVector, Vector3 maxVector)
    {
        float x = Random.Range(minVector.x, maxVector.x);
        float y = Random.Range(minVector.y, maxVector.y);
        float z = Random.Range(minVector.z, maxVector.z);

        return new Vector3(x, y, z);
    }
}
