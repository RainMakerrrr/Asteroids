using Data;
using Enemies;
using ObjectPool;
using Player;
using Player.Shooting;
using UnityEngine;


public class PrefabReplacer : MonoBehaviour
{
    [SerializeField] private GamePrefabs _firstStylePrefabs;
    [SerializeField] private GamePrefabs _secondStylePrefabs;

    private GamePrefabs _currentPrefabs;

    private AsteroidPool _asteroidPool;
    private BulletPool _bulletPool;
    private FlyingSaucerPool _flyingSaucerPool;


    private void Awake()
    {
        _asteroidPool = FindObjectOfType<AsteroidPool>();
        _bulletPool = FindObjectOfType<BulletPool>();
        _flyingSaucerPool = FindObjectOfType<FlyingSaucerPool>();

        _currentPrefabs = _firstStylePrefabs;

        _asteroidPool.SetPrefabs(_currentPrefabs.Asteroids);
        _bulletPool.SetPrefabs(new[] {_currentPrefabs.Bullet});
        _flyingSaucerPool.SetPrefabs(new[] {_currentPrefabs.FlyingSaucer});

        Instantiate(_currentPrefabs.Player, Vector3.zero, Quaternion.identity);
    }

    public void ReplacePrefabs()
    {
        _currentPrefabs = _currentPrefabs == _firstStylePrefabs ? _secondStylePrefabs : _firstStylePrefabs;

        foreach (var asteroid in FindObjectsOfType<Asteroid>())
        {
            _asteroidPool.ReturnToPool(asteroid);
        }

        foreach (var bullet in FindObjectsOfType<Bullet>())
        {
            _bulletPool.ReturnToPool(bullet);
        }

        foreach (var flyingSaucer in FindObjectsOfType<FlyingSaucer>())
        {
            _flyingSaucerPool.ReturnToPool(flyingSaucer);
        }

        _asteroidPool.SetPrefabs(_currentPrefabs.Asteroids);
        _bulletPool.SetPrefabs(new[] {_currentPrefabs.Bullet});
        _flyingSaucerPool.SetPrefabs(new[] {_currentPrefabs.FlyingSaucer});

        var player = FindObjectOfType<PlayerMovement>();

        if (player == null) return;

        var playerPosition = player.transform.position;
        var playerRotation = player.transform.rotation;

        Destroy(player.gameObject);
        Instantiate(_currentPrefabs.Player, playerPosition, playerRotation);
    }
}