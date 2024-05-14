using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartInteractiveScene : MonoBehaviour
{
    public void SwitchScene()
    {
        SceneManager.LoadScene("InteractiveScene");
    }
}
