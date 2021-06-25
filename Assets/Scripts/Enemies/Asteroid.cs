using ObjectPool;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemies
{
    public class Asteroid : EnemyObject
    {
        [SerializeField] private float _minSize;
        [SerializeField] private float _maxSize;
        [SerializeField] private float _forcePower;
        [SerializeField] private float _maxLifeTime;

        private float _size = 1f;
        private float _lifeTime;

        private Rigidbody2D _rigidbody;
        private AsteroidPool _asteroidPool;

        private void OnEnable()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _asteroidPool = FindObjectOfType<AsteroidPool>();

            _size = Random.Range(_minSize, _maxSize);
            _lifeTime = 0f;

            transform.eulerAngles = new Vector3(0f, 0f, Random.value * 360f);
            transform.localScale = Vector3.one * _size;
            _rigidbody.mass = _size;
        }

        private void Update()
        {
            _lifeTime += Time.deltaTime;
            if (_lifeTime >= _maxLifeTime)
                _asteroidPool.ReturnToPool(this);
        }


        protected override void OnCollisionEnter2D(Collision2D other)
        {
            base.OnCollisionEnter2D(other);

            if (_size / 2 >= _minSize)
                CreateSplitAsteroid();

            _asteroidPool.ReturnToPool(this);
        }

        public override void DisableObject()
        {
            base.DisableObject();
            _asteroidPool.ReturnToPool(this);
        }

        private void CreateSplitAsteroid()
        {
            for (int i = 0; i < 2; i++)
            {
                Vector2 asteroidPosition = transform.position;
                asteroidPosition += Random.insideUnitCircle / 2;

                var asteroid = Instantiate(this, asteroidPosition, transform.rotation);

                asteroid._size = _size / 2;
                asteroid.SetTrajectory(Random.insideUnitCircle.normalized * _forcePower);
            }
        }

        public override void SetTrajectory(Vector2 direction) => _rigidbody.AddForce(direction * _forcePower);
    }
}