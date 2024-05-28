using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class InteractiveSceneManager : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera cinemachineCamera;
    [SerializeField] private GameObject astronaut;
    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform endPoint;
    [SerializeField] private Transform minPosition;
    [SerializeField] private Transform maxPosition;
    [SerializeField] private float asteroidDelay = 1f;
    [SerializeField] private float asteroidMinSpeed = 2f;
    [SerializeField] private float asteroidMaxSpeed = 20f;
    [SerializeField] private Transform asteroidMinPosition;
    [SerializeField] private Transform asteroidMaxPosition;
    [SerializeField] private GameObject[] asteroids;
    private PlayerController _playerController;
    
    public void StartInteractiveScene()
    {
        _playerController = astronaut.GetComponent<PlayerController>();
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
        Invoke(nameof(CreateAsteroid), asteroidDelay);
    }

    public void EndInteractiveScene()
    {
        Destroy(_playerController);
        cinemachineCamera.Follow = null;
        cinemachineCamera.LookAt = null;
        SceneManager.LoadScene("AfterInteractiveScene");
    }

    private void Start()
    {
        StartInteractiveScene();
    }

    private void CreateAsteroid()
    {
        if (!gameObject.IsDestroyed())
        {
            int asteroidIndex = Random.Range(0, asteroids.Length);
            Vector3 position = RandomUtils.RandomVector3(asteroidMinPosition.position, asteroidMaxPosition.position);
            Quaternion rotation = Quaternion.Euler(RandomUtils.RandomVector3(Vector3.zero, Vector3.one * 360f));
            GameObject asteroid = Instantiate(asteroids[asteroidIndex], position, rotation);
            float speed = Random.Range(asteroidMinSpeed, asteroidMaxSpeed);
            Rigidbody rb = asteroid.GetComponent<Rigidbody>();
            rb.AddForce(Vector3.right * speed, ForceMode.Impulse);
            rb.AddTorque(Vector3.right * speed, ForceMode.Impulse);
            Invoke(nameof(CreateAsteroid), asteroidDelay);
        }
    }
}
