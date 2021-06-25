using System.Collections;
using ObjectPool;
using Player;
using UnityEngine;

namespace Enemies
{
    public class FlyingSaucer : EnemyObject
    {
        [SerializeField] private float _moveSpeed;

        private PlayerMovement _playerMovement;
        private FlyingSaucerPool _flyingSaucerPool;

        private void OnEnable()
        {
            _playerMovement = FindObjectOfType<PlayerMovement>();
            _flyingSaucerPool = FindObjectOfType<FlyingSaucerPool>();
        }

        private IEnumerator ChasePlayer()
        {
            if (_playerMovement == null) yield break;

            var distance = Vector2.Distance(transform.position, _playerMovement.transform.position);
            while (distance >= 0.1f)
            {
                if (_playerMovement == null) yield break;
                distance = Vector2.Distance(transform.position, _playerMovement.transform.position);

                transform.position = Vector3.MoveTowards(transform.position, _playerMovement.transform.position,
                    _moveSpeed * Time.deltaTime);
                yield return null;
            }
        }


        protected override void OnCollisionEnter2D(Collision2D other)
        {
            base.OnCollisionEnter2D(other);
            _flyingSaucerPool.ReturnToPool(this);
        }

        public override void DisableObject()
        {
            base.DisableObject();
            _flyingSaucerPool.ReturnToPool(this);
        }

        public override void SetTrajectory(Vector2 direction) => StartCoroutine(ChasePlayer());
    }
}