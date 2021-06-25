using Enemies;
using Player;
using Player.Shooting;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu]
    public class GamePrefabs : ScriptableObject
    {
        [SerializeField] private PlayerMovement _player;
        [SerializeField] private Asteroid[] _asteroids;
        [SerializeField] private FlyingSaucer _flyingSaucer;
        [SerializeField] private Bullet _bullet;

        public PlayerMovement Player => _player;
        public Asteroid[] Asteroids => _asteroids;
        public FlyingSaucer FlyingSaucer => _flyingSaucer;
        public Bullet Bullet => _bullet;
    }
}