using System;
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
        [SerializeField] private float _groundCheckRadius;

        [SerializeField] private LayerMask _groundLayer;

        [SerializeField] private Transform _groundPoint;

        private PlayerInput _playerInput;
        private PlayerMovement _playerMovement;
        private PlayerAnimator _playerAnimator;

        private Vector2 _inputVector;
        private Vector3 _scaleForward, _scaleBackward;
        private bool _isGrounded;
        private bool _jumping;
        private bool _directionForward;

        void Start()
        {
            CheckGroundPoint();
            CheckAnimator();
            CheckRigidbody();

            Init();
        }

        private void Init()
        {
            InitScaleDirection();
            _playerInput = new PlayerInput(this);
            _playerMovement = new PlayerMovement(_rigidbody2D, _jumpForce);
            _playerAnimator = new PlayerAnimator(_animator);
        }

        private void InitScaleDirection()
        {
            _directionForward = true;
            _scaleForward = transform.localScale;
            _scaleBackward = _scaleForward;
            _scaleBackward.x *= -1;
        }

        #region Проверка наличия необходимых объектов
        private void CheckGroundPoint()
        {
            if (_groundPoint == null)
            {
                throw new NullReferenceException("ground point не добавлен в инспекторе!!1");
            }
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
        #endregion

        public void SetInput(Vector2 inputVector)
        {
            _inputVector = inputVector;

            if(inputVector.x > 0 && !_directionForward)
            {
                _directionForward = true;
                Flip();
            }
            else if(inputVector.x < 0 &&_directionForward)
            {
                _directionForward = false;
                Flip();
            }

        }

        public void SetJump(bool jumping)
        {
            _jumping = jumping;
        }

        private void Flip()
        {
            transform.localScale = _directionForward ? _scaleForward : _scaleBackward;
        }

        void Update()
        {
            _playerAnimator.SetGrounded(_isGrounded);
            _playerAnimator.SetSpeed(_inputVector.x);

        }

        private void FixedUpdate()
        {
            _isGrounded = Physics2D.OverlapCircle(_groundPoint.position, _groundCheckRadius, _groundLayer);

            _playerMovement.Move(_inputVector.x * _speed);
            _playerMovement.Jump(_isGrounded, _jumping);
        }
    }
}

