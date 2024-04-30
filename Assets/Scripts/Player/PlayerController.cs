using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;

    private Rigidbody _rigidbody;

    private Vector2 _moveInput;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        float x = _moveInput.x;
        float y = 0f;
        float z = _moveInput.y;
        Vector3 velocity = new Vector3(x, y, z);
        velocity *= speed * Time.deltaTime;

        if (_rigidbody.velocity.magnitude < speed)
        {
            _rigidbody.AddForce(velocity);
        }
        
        Debug.Log(_rigidbody.velocity);
    }
}
