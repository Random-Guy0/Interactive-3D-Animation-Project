using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndAnimation : MonoBehaviour
{
    public void Stop()
    {
        UnityEditor.EditorApplication.isPaused = false;
    }
}
