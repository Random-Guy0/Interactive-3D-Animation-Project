using UnityEngine;

[CreateAssetMenu(menuName = "Transform List", fileName = "new Transform List")]
public class TransformList : ScriptableObject
{
    [field: SerializeField] public Transform[] Transforms { get; private set; }
}