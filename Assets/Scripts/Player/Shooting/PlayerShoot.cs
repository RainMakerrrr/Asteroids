using System;
using Input;
using ObjectPool;
using UnityEngine;

namespace Player.Shooting
{
    public class PlayerShoot : MonoBehaviour
    {
        public static event Action BulletShooted;

        [SerializeField] private float _bulletShootRate;
        [SerializeField] private float _laserShootRate;
        [SerializeField] private Laser _laser;

        private float _nextBulletShootTime;
        private float _nextLaserShootTime;

        private InputHandler _inputHandler;
        private BulletPool _bulletPool;

        private void OnEnable()
        {
            _bulletPool = FindObjectOfType<BulletPool>();

            _inputHandler = new InputHandler();
            _inputHandler.BulletShoot += OnBulletShoot;
            _inputHandler.LaserShoot += OnLaserShoot;
        }

        private void OnBulletShoot()
        {
            if (!(Time.time > _nextBulletShootTime)) return;

            SpawnBullet();
            BulletShooted?.Invoke();

            _nextBulletShootTime = Time.time + 1 / _bulletShootRate;
        }

        private void OnLaserShoot()
        {
            if (!(Time.time > _nextLaserShootTime)) return;

            _laser.gameObject.SetActive(true);

            _nextLaserShootTime = Time.time + 1 / _laserShootRate;
        }

        private void SpawnBullet()
        {
            var bullet = _bulletPool.GetObject();

            bullet.transform.position = transform.position;
            bullet.transform.rotation = transform.rotation;

            bullet.gameObject.SetActive(true);
            bullet.AddBulletForce(transform.up);
        }

        private void OnDisable()
        {
            _inputHandler.BulletShoot -= OnBulletShoot;
            _inputHandler.LaserShoot -= OnLaserShoot;
        }
    }
}