using ObjectPool;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemies
{
    public abstract class ObjectSpawner<T> : MonoBehaviour where T : EnemyObject
    {
        [SerializeField] private float _firstTimeSpawn;
        [SerializeField] private float _spawnRate;
        [SerializeField] private float _spawnDistance;
        [SerializeField] private float _trajectoryVariance;

        private GenericObjectPool<T> _objectPool;

        private void OnEnable()
        {
            _objectPool = FindObjectOfType<GenericObjectPool<T>>();
            InvokeRepeating(nameof(SpawnObject), _firstTimeSpawn, _spawnRate);
        }

        private void SpawnObject()
        {
            Vector3 spawnDirection = Random.insideUnitCircle.normalized * _spawnDistance;
            var spawnPoint = transform.position + spawnDirection;

            var variance = Random.Range(-_trajectoryVariance, _trajectoryVariance);
            var rotation = Quaternion.AngleAxis(variance, Vector3.forward);

            var enemyObject = _objectPool.GetObject();
            enemyObject.transform.position = spawnPoint;
            enemyObject.transform.rotation = rotation;

            enemyObject.gameObject.SetActive(true);

            enemyObject.SetTrajectory(rotation * -spawnDirection);
        }
    }
}