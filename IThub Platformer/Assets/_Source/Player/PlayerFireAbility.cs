using UnityEngine;
using Supporting;
using System;

namespace Player
{
    public class PlayerFireAbility
    {
        private float _fireTimer;
        private float _fireDelay;
        private ObjectPool _objectPool;

        public PlayerFireAbility(int projectilesPoolAmount, GameObject projectilePrefab, float fireDelay)
        {
            _fireDelay = fireDelay;
            _objectPool = new ObjectPool(projectilesPoolAmount, projectilePrefab);
            
            _fireTimer = _fireDelay;
        }

        public void TryFiring(bool firing, Vector3 firePosition, bool direction)
        {
            if(_fireTimer > 0)
            {
                _fireTimer -= Time.deltaTime;
            }
            else if (firing)
            {
                GameObject bullet = _objectPool.GetPooledObject();

                if(bullet != null)
                {
                    PlayerProjectile playerProjectile = bullet.TryGetComponent(out PlayerProjectile projectile)
                        ? projectile
                        : throw new NullReferenceException("PlayerProjectail отсутствует в ObjectPool");

                    projectile.Init(firePosition, direction);
                    _fireTimer = _fireDelay;
                }
            }


        }

    }
}

