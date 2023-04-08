using System;
using Supporting;
using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 2f;
    [SerializeField] private float _raycastDistance = 1f;
    [SerializeField] private LayerMask _platformLayer;
    [SerializeField] private LayerMask _playerProjectileLayer;
    [SerializeField] private Transform _leftPoint, _rightPoint;
    [SerializeField] private Rigidbody2D _rigidbody2D;

    [SerializeField] private SpriteRenderer _sprite;
    [SerializeField] private int _damageToPlayer;

    private bool _movingRight;
    private RaycastHit2D _leftHit, _rightHit;

    void Start()
    {
        CheckRigidbody();
        CheckSprite();
        CheckRaycastPoints();
    }

    #region Проверка наличия неободимых объектов
    private void CheckRaycastPoints()
    {
        if (_leftPoint == null)
        {
            throw new NullReferenceException("Отсутствует ЛЕВАЯ точка спавна рейкаста у врага!");
        }
        if (_rightPoint == null)
        {
            throw new NullReferenceException("Отсутствует ПРАВАЯ точка спавна рейкаста у врага!");
        }
    }

    private void CheckSprite()
    {
        if (_sprite == null)
        {
            throw new NullReferenceException("Отсутствует ссылка на SpriteRenderer врага!");
        }
    }

    private void CheckRigidbody()
    {
        if (_rigidbody2D == null)
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }
    }

    #endregion

    void Update()
    {
        CheckMove();

    }

    public int GetDamage()
    {
        return _damageToPlayer;
    }

    private void CheckMove()
    {
        _leftHit = Physics2D.Raycast(_leftPoint.position, Vector2.down, _raycastDistance, _platformLayer);
        _rightHit = Physics2D.Raycast(_rightPoint.position, Vector2.down, _raycastDistance, _platformLayer);

        if(_leftHit.collider == null)
        {
            _movingRight = true;
            _sprite.flipX = true;
        }
        else if(_rightHit.collider == null)
        {
            _movingRight = false;
            _sprite.flipX = false;
        }

        _rigidbody2D.velocity = _movingRight ? Vector2.right * _moveSpeed : Vector2.left * _moveSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(Utils.IsInLayerMask(collision.gameObject.layer, _playerProjectileLayer))
        {
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            //collision.gameObject.SetActive(false);
            Destroy(gameObject); // здесь можно реализовать логику вычитания здоровья у врага, и убивать при падении здоровья до 0
        }
    }
}
