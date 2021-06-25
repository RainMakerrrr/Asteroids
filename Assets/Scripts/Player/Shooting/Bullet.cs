using System;
using ObjectPool;
using UnityEngine;

namespace Player.Shooting
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float _forcePower;
        [SerializeField] private float _maxLifeTime;
        private float _lifeTime;

        private Rigidbody2D _rigidbody;
        private BulletPool _bulletPool;

        private void OnEnable()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _bulletPool = FindObjectOfType<BulletPool>();

            _lifeTime = 0f;
        }

        private void Update()
        {
            _lifeTime += Time.deltaTime;
            if (_lifeTime >= _maxLifeTime)
                _bulletPool.ReturnToPool(this);
        }

        public void AddBulletForce(Vector2 direction) => _rigidbody.AddForce(direction * _forcePower);

        private void OnCollisionEnter2D(Collision2D other)
        {
            _bulletPool.ReturnToPool(this);
        }
    }
}