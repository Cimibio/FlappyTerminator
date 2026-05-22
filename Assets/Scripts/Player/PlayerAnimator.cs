using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimator : MonoBehaviour
{
    private readonly int _dieHash = Animator.StringToHash("explosion");

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void PlayExplosionAnimation()
    {
        _animator.SetTrigger(_dieHash);
    }

    public void ResetAnimation()
    {
        if (_animator != null)
        {
            _animator.Rebind();
            _animator.Update(0f);
        }
    }
}
