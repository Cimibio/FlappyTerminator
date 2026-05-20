using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyAnimator : MonoBehaviour
{
    private Animator _animator;

    private readonly int _dieHash = Animator.StringToHash("explosion");

    public event Action EnemyExplosionAnimationCompleted;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void PlayExplosionAnimation()
    {
        _animator.SetTrigger(_dieHash);
    }

    public void NotifyExplosionAnimationComplete()
    {
        EnemyExplosionAnimationCompleted?.Invoke();
    }
}
