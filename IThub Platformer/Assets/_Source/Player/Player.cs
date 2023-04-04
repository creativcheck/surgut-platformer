using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Player
{
    [RequireComponent (typeof(Rigidbody2D))]
    [RequireComponent (typeof(Animator))]
    public class Player : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private Rigidbody2D _rigidbody2D;

        [SerializeField] private float _speed;
        [SerializeField] private float _jumpForce;

        private PlayerInput _playerInput;
        private PlayerMovement _playerMovement;

        private Vector2 _inputVector;

        void Start()
        {
            CheckAnimator();
            CheckRigidbody();

            Init();
        }

        private void Init()
        {
            _playerInput = new PlayerInput(this);
            _playerMovement = new PlayerMovement(_rigidbody2D);
        }

        private void CheckAnimator()
        {
            if(_animator == null)
            {
                _animator = GetComponent<Animator>();
            }
        }

        private void CheckRigidbody()
        {
            if(_rigidbody2D == null)
            {
                _rigidbody2D = GetComponent<Rigidbody2D>();
            }
        }

        public void SetInput(Vector2 inputVector)
        {
            _inputVector = inputVector;


        }

        void Update()
        {

        }

        private void FixedUpdate()
        {
            _playerMovement.Move(_inputVector.x * _speed);
        }
    }
}

