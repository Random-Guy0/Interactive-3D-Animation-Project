#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class RandomTransformPlacerBase : EditorWindow
{
    protected Vector3 MinPosition { get => _minPositionField.value; }
    protected Vector3 MaxPosition { get => _maxPositionField.value; }
    protected Vector3 MinRotation { get => _minRotationField.value; }
    protected Vector3 MaxRotation { get => _maxRotationField.value; }
    protected float MinScale { get => _minScaleField.value; }
    protected float MaxScale { get => _maxScaleField.value; }
    
    private Vector3Field _minPositionField;
    private Vector3Field _maxPositionField;
    private Vector3Field _minRotationField;
    private Vector3Field _maxRotationField;
    private FloatField _minScaleField;
    private FloatField _maxScaleField;
    
    protected static void ShowWindow<T>(string windowName) where T : RandomTransformPlacerBase
    {
        T newWindow = GetWindow<T>();
        newWindow.titleContent = new GUIContent(windowName);
    }
    
    protected virtual void CreateGUI()
    {
        VisualElement root = rootVisualElement;
        
        root.Add(new Label());
        
        _minPositionField = new Vector3Field("Minimum Position");
        root.Add(_minPositionField);
        _maxPositionField = new Vector3Field("Maximum Position");
        root.Add(_maxPositionField);
        
        root.Add(new Label());
        
        _minRotationField = new Vector3Field("Minimum Rotation");
        root.Add(_minRotationField);
        _maxRotationField = new Vector3Field("Maximum Rotation");
        root.Add(_maxRotationField);
        
        root.Add(new Label());
        
        _minScaleField = new FloatField("Minimum Scale");
        root.Add(_minScaleField);
        _maxScaleField = new FloatField("Maximum Scale");
        root.Add(_maxScaleField);

        root.Add(new Label());
        
        Button randomizeButton = new Button(Randomize);
        randomizeButton.text = "Randomize";
        root.Add(randomizeButton);
    }

    protected abstract void Randomize();
}
#endif