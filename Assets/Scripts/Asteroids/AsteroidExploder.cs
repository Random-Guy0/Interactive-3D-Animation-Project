using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidExploder : MonoBehaviour
{
    [SerializeField] private float explosionForce = 10f;
    [SerializeField] private float upwardsModifier = 5f;
    private List<Rigidbody> _allContacts;

    private SphereCollider _collider;

    private void Awake()
    {
        _allContacts = new List<Rigidbody>();
        _collider = GetComponent<SphereCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.attachedRigidbody != null)
        {
            _allContacts.Add(other.attachedRigidbody);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        _allContacts.Remove(other.attachedRigidbody);
    }

    public void Explode()
    {
        Vector3 center = transform.TransformPoint(_collider.center);
        
        foreach (Rigidbody contact in _allContacts)
        {
            contact.AddExplosionForce(explosionForce, center, _collider.radius, upwardsModifier, ForceMode.Impulse);
        }
    }
}

