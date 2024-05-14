using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AlienFireGun : MonoBehaviour
{
    [SerializeField] private AlienProjectile firedProjectile;
    [SerializeField] private Transform firingPoint;
    [SerializeField] private float minWaitTime = 0.75f;
    [SerializeField] private float maxWaitTime = 1.75f;

    private Animator _animator;

    private bool _canFire = true;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void FireGun()
    {
        AlienProjectile projectileInstance = Instantiate(firedProjectile, firingPoint.position, Quaternion.LookRotation(firingPoint.forward));
    }

    public void WaitToFire()
    {
        float waitTime = Random.Range(minWaitTime, maxWaitTime);
        Invoke(nameof(CanFire), waitTime);
    }

    private void CanFire()
    {
        if (_canFire)
        {
            _animator.SetTrigger("WaitToFire");
        }
    }

    public void StopFiring()
    {
        _canFire = false;
    }
}
