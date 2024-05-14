using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AnimationTimeOffset : MonoBehaviour
{
    private Animator _animator;
    private void Start()
    {
        float offset = Random.value;
        _animator = GetComponent<Animator>();
        _animator.SetFloat("Offset", offset);
    }
}
