using UnityEngine;

namespace Player
{
    public class PlayerMovement
    {
        private Rigidbody2D _rigidbody2D;
        private float _jumpForce;

        public PlayerMovement(Rigidbody2D rigidbody2D, float jumpForce)
        {
            _rigidbody2D = rigidbody2D;
            _jumpForce = jumpForce;
        }

        public void Move(float inputX)
        {
            _rigidbody2D.velocity = new Vector2(inputX, _rigidbody2D.velocity.y);
        }

        public void Jump(bool grounded, bool jump)
        {
            if(jump && grounded)
            {
                _rigidbody2D.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
            }
        }
    }


}
