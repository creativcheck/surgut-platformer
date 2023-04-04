using UnityEngine;

namespace Player
{
    public class PlayerMovement
    {
        private Rigidbody2D _rigidbody2D;

        public PlayerMovement(Rigidbody2D rigidbody2D)
        {
            _rigidbody2D = rigidbody2D;
        }

        public void Move(float inputX)
        {
            _rigidbody2D.velocity = new Vector2(inputX, _rigidbody2D.velocity.y);
        }
    }


}
