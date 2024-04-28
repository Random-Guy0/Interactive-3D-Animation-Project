#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class RandomTransformPlacer : RandomTransformPlacerBase
{
    private ObjectField transformsToPlaceField;
    private IntegerField numberOfTransformsToPlaceField;
    
    [MenuItem("Tools/Random Transform Placer")]
    public static void ShowWindow()
    {
        ShowWindow<RandomTransformPlacer>("Random Transform Placer");
    }

    protected override void CreateGUI()
    {
        VisualElement root = rootVisualElement;
        
        root.Add(new Label());

        transformsToPlaceField = new ObjectField("Transforms to Place");
        transformsToPlaceField.objectType = typeof(TransformList);
        root.Add(transformsToPlaceField);

        numberOfTransformsToPlaceField = new IntegerField("Number of Transforms to Place");
        root.Add(numberOfTransformsToPlaceField);
        
        base.CreateGUI();
    }

    protected override void Randomize()
    {
        GameObject randomParent = new GameObject("Random Parent");
        
        int numTransformsToPlace = numberOfTransformsToPlaceField.value;
        TransformList transformList = (TransformList)transformsToPlaceField.value;
        Transform[] transforms = transformList.Transforms;

        for (int i = 0; i < numTransformsToPlace; i++)
        {
            int randomTransformIndex = Random.Range(0, transforms.Length);
            Transform randomTransform = transforms[randomTransformIndex];

            Transform newInstance = Instantiate(randomTransform, randomParent.transform);
            newInstance.RandomizeLocalPosition(MinPosition, MaxPosition);
            newInstance.RandomizeLocalRotation(MinRotation, MaxRotation);
            newInstance.RandomizeLocalScale(MinScale, MaxScale);
        }
    }
}
#endif