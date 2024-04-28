using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TransformExtensions
{
    public static void RandomizeLocalPosition(this Transform transform, Vector3 minPosition, Vector3 maxPosition)
    {
        transform.localPosition = RandomUtils.RandomVector3(minPosition, maxPosition);
    }

    public static void RandomizeLocalRotation(this Transform transform, Vector3 minRotation, Vector3 maxRotation)
    {
        Vector3 randomRotation = RandomUtils.RandomVector3(minRotation, maxRotation);
        transform.localRotation = Quaternion.Euler(randomRotation);
    }

    public static void RandomizeLocalScale(this Transform transform, float minScale, float maxScale)
    {
        Vector3 randomScale = Vector3.one * Random.Range(minScale, maxScale);
        transform.localScale = randomScale;
    }
}
