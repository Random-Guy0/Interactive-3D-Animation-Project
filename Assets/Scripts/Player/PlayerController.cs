using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private float maxSpeed = 2f;
    [SerializeField] private float rotationSpeed = 15f;

    private Transform _minPosition;
    private Transform _maxPosition;

    private Rigidbody _rigidbody;
    private Animator _animator;

    private Vector2 _moveInput;
    
    public Action EndCallback { get; set; }

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.isKinematic = false;
        _rigidbody.interpolation = RigidbodyInterpolation.Interpolate;

        _animator = GetComponent<Animator>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
    }

    private void Update()
    {
        if (_moveInput == Vector2.zero)
        {
            _animator.SetBool("Floating", true);
            _animator.SetBool("Moving", false);
        }
        else
        {
            _animator.SetBool("Moving", true);
            _animator.SetBool("Floating", false);
        }
        
        Vector3 rotation = transform.rotation.eulerAngles;
        rotation.y += rotationSpeed * Time.deltaTime * _moveInput.x;
        transform.rotation = Quaternion.Euler(rotation);
    }

    private void FixedUpdate()
    {
        Vector3 x = 0f * transform.right;
        Vector3 y = 0f * transform.up;
        Vector3 z = _moveInput.y * transform.forward;
        Vector3 velocity = x + y + z;
        velocity *= speed * Time.deltaTime;

        if (_rigidbody.velocity.magnitude < maxSpeed)
        {
            _rigidbody.AddForce(velocity, ForceMode.Impulse);
        }

        _rigidbody.position = _rigidbody.position.Clamp(_minPosition.position, _maxPosition.position);
    }

    public void SetMinAndMaxPosition(Transform minPosition, Transform maxPosition)
    {
        _minPosition = minPosition;
        _maxPosition = maxPosition;
    }
}
