using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class InteractiveSceneManager : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera cinemachineCamera;
    [SerializeField] private GameObject astronaut;
    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform endPoint;
    [SerializeField] private Transform minPosition;
    [SerializeField] private Transform maxPosition;
    private PlayerController _playerController;
    
    public void StartInteractiveScene()
    {
        _playerController = astronaut.AddComponent<PlayerController>();
        _playerController.EndCallback = EndInteractiveScene;
        astronaut.transform.position = startPoint.position;
        astronaut.transform.LookAt(endPoint);
        cinemachineCamera.Follow = astronaut.transform;
        cinemachineCamera.LookAt = astronaut.transform;

        PlayerInput playerInput = astronaut.GetComponent<PlayerInput>();
        ReadOnlyArray<PlayerInput.ActionEvent> actionEvents = playerInput.actionEvents;
        foreach (PlayerInput.ActionEvent actionEvent in actionEvents)
        {
            if (actionEvent.actionName.Substring(0, 11).Equals("Player/Move"))
            {
                actionEvent.AddListener(_playerController.OnMove);
            }
        }
        
        _playerController.SetMinAndMaxPosition(minPosition, maxPosition);
    }

    public void EndInteractiveScene()
    {
        Destroy(_playerController);
        cinemachineCamera.Follow = null;
        cinemachineCamera.LookAt = null;
    }

    private void Start()
    {
        StartInteractiveScene();
    }
}
