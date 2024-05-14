using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienProjectile : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float timeToLive = 2.5f;
    private Rigidbody _rigidbody;

    private void Start()
    {
        Invoke(nameof(Destroy), timeToLive);
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.velocity = transform.forward * speed;
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }
}
