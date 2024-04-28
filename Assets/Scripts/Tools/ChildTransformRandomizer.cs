#if UNITY_EDITOR
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class ChildTransformRandomizer : RandomTransformPlacerBase
{
    private ObjectField _parentTransformField;
    
    [MenuItem("Tools/Child Transform Randomizer")]
    public static void ShowWindow()
    {
        ShowWindow<RandomTransformPlacer>("Child Transform Randomizer");
    }
    
    protected override void CreateGUI()
    {
        VisualElement root = rootVisualElement;
        
        root.Add(new Label());

        _parentTransformField = new ObjectField("Parent Transform");
        _parentTransformField.objectType = typeof(Transform);
        root.Add(_parentTransformField);
        
        base.CreateGUI();
    }

    protected override void Randomize()
    {
        Transform parent = (Transform)_parentTransformField.value;
        List<Transform> children = parent.GetComponentsInChildren<Transform>().ToList();
        children.Remove(parent);

        foreach (Transform child in children)
        {
            child.RandomizeLocalPosition(MinPosition, MaxPosition);
            child.RandomizeLocalRotation(MinRotation, MaxRotation);
            child.RandomizeLocalScale(MinScale, MaxScale);
        }
    }
}
#endif