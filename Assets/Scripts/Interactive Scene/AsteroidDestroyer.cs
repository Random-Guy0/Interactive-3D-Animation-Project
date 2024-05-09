using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidDestroyer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Asteroid"))
        {
            Destroy(other.gameObject);
            Debug.Log("Asteroid Destroyed");
        }
    }
}
