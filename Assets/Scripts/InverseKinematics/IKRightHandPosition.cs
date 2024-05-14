using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKRightHandPosition : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField, Range(0f, 1f)] private float ikWeight = 1f;
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnAnimatorIK(int layerIndex)
    {
        _animator.SetIKPosition(AvatarIKGoal.RightHand, target.position);
        _animator.SetIKPositionWeight(AvatarIKGoal.RightHand, ikWeight);
    }
}
