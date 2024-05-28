using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private float maxSpeed = 2f;
    [SerializeField] private float rotationSpeed = 30f;
    [SerializeField] private float hitSpeed = 4f;

    private Transform _minPosition;
    private Transform _maxPosition;

    private Rigidbody _rigidbody;
    private Animator _animator;

    private Vector2 _moveInput;

    private bool _lockInput = false;
    private bool _hit = false;
    
    public Action EndCallback { get; set; }

    [SerializeField] private AudioSource asteroidSound;
    [SerializeField] private AudioSource hitSound;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.isKinematic = false;
        //_rigidbody.interpolation = RigidbodyInterpolation.Interpolate;

        _animator = GetComponent<Animator>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
    }

    private void Update()
    {
        if (!_hit)
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
        }

        Vector3 rotation = transform.rotation.eulerAngles;
        rotation.y += rotationSpeed * Time.deltaTime * _moveInput.x;
        if (!_lockInput)
        {
            transform.rotation = Quaternion.Euler(rotation);
        }
    }

    private void FixedUpdate()
    {
        Vector3 x = 0f * transform.right;
        Vector3 y = 0f * transform.up;
        Vector3 z = _moveInput.y * transform.forward;
        Vector3 velocity = x + y + z;
        velocity *= speed * Time.deltaTime;

        if (_rigidbody.velocity.magnitude < maxSpeed && !_lockInput)
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

    private IEnumerator Hit(Quaternion initialRotation)
    {
        _hit = true;
        _animator.SetBool("Moving", true);
        _animator.SetBool("Floating", false);
        Quaternion currentRotation = initialRotation;
        float time = 0f;
        while (time < 2f)
        {
            float amountPerFrame = -720f * Time.deltaTime * 0.5f;
            currentRotation *= Quaternion.Euler(Vector3.right * amountPerFrame);
            transform.rotation = currentRotation;
            
            yield return null;
            time += Time.deltaTime;
        }
        
        transform.localRotation = initialRotation;
        _lockInput = false;
        _hit = false;
        _animator.SetBool("Floating", true);
        _animator.SetBool("Moving", false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Asteroid") && !_lockInput)
        {
            _lockInput = true;
            StartCoroutine(Hit(transform.localRotation));
            _rigidbody.AddForce(hitSpeed * -transform.forward, ForceMode.Impulse);
            hitSound.Play();
            asteroidSound.Play();
        }
        else if (other.CompareTag("InteractiveSceneEnd"))
        {
            Destroy(_rigidbody);
            EndCallback();
        }
    }
}
