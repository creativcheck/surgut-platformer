using System.Data;
using UnityEngine;

namespace Player
{
    public class PlayerAnimator
    {
        private const string SPEED = "Speed";
        private const string GROUNDED = "Grounded";

        private readonly int _speedHash = Animator.StringToHash(SPEED);
        private readonly int _groundedHash = Animator.StringToHash(GROUNDED);

        private Animator _animator;

        public PlayerAnimator(Animator animator)
        {
            if(animator == null)
            {
                throw new NoNullAllowedException();
            }
            
            _animator = animator;
        }

        public void SetGrounded(bool grounded)
        {
            _animator.SetBool(_groundedHash, grounded);
        }

        public void SetSpeed(float speed)
        {
            _animator.SetFloat(_speedHash, Mathf.Abs(speed));
        }
    }

}
