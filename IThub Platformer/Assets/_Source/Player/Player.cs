using System;
using Supporting;
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
        [SerializeField] private float _firingDelay = 0.4f;

        [SerializeField] private int _projectilesPoolAmount;
        [SerializeField] private int _health = 3;

        [SerializeField] private LayerMask _groundLayer;
        [SerializeField] private LayerMask _enemyLayer;

        [SerializeField] private GameObject _projectilePrefab;

        [SerializeField] private Transform _groundPoint;
        [SerializeField] private Transform _firePoint;

        [SerializeField] private PlayerStatsView _HPView;

        private PlayerInput _playerInput;
        private PlayerMovement _playerMovement;
        private PlayerAnimator _playerAnimator;
        private PlayerFireAbility _playerFire;
        private PlayerHealth _playerHealth;

        private Vector2 _inputVector;
        private Vector3 _scaleForward, _scaleBackward;
        private bool _isGrounded;
        private bool _jumping;
        private bool _firing;
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
            _playerFire = new PlayerFireAbility(_projectilesPoolAmount, _projectilePrefab, _firingDelay);
            _playerHealth = new PlayerHealth(_HPView, _health);
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


        #region Сеттеры из инпута
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

        public void SetFire(bool firing)
        {
            _firing = firing;
        }

        public void SetJump(bool jumping)
        {
            _jumping = jumping;
        }
        #endregion

        private void Flip()
        {
            transform.localScale = _directionForward ? _scaleForward : _scaleBackward;
        }

        void Update()
        {
            _playerAnimator.SetGrounded(_isGrounded);
            _playerAnimator.SetSpeed(_inputVector.x);

            _playerFire.TryFiring(_firing, _firePoint.position, _directionForward);
        }

        private void FixedUpdate()
        {
            _isGrounded = Physics2D.OverlapCircle(_groundPoint.position, _groundCheckRadius, _groundLayer);

            _playerMovement.Move(_inputVector.x * _speed);
            _playerMovement.Jump(_isGrounded, _jumping);
        }

        private void OnDestroy()
        {
            _playerInput.UnBind();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if(Utils.IsInLayerMask(collision.gameObject.layer, _enemyLayer))
            {
                _playerHealth.ChangeHealth(-collision.gameObject.GetComponent<Enemy>().GetDamage());
            }
        }
    }
}

