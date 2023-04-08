using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    [RequireComponent (typeof(Rigidbody2D))]
    public class PlayerProjectile : MonoBehaviour
    {
        [SerializeField] private float _speed, _lifetime;
        [SerializeField] private Rigidbody2D _rigidbody2D;

        private void Awake()
        {
            if(_rigidbody2D == null)
            {
                _rigidbody2D = GetComponent<Rigidbody2D>();
            }
        }

        public void Init(Vector3 position, bool directionRight)
        {
            transform.position = position;
            transform.rotation = directionRight ? Quaternion.identity : Quaternion.Euler(0, 0, 180);
            gameObject.SetActive(true);

            ApplyStartForce();
            StartCoroutine(DeactivateCoroutine());
        }

        IEnumerator DeactivateCoroutine()
        {
            yield return new WaitForSeconds(_lifetime);

            gameObject.SetActive(false);
        }

        private void ApplyStartForce()
        {
            _rigidbody2D.AddForce(transform.right * _speed, ForceMode2D.Impulse);
            //_rigidbody2D.velocity = transform.right * _speed;
        }
        
    }
}

