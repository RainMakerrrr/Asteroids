using Enemies;
using Player.Collisions;
using Player.Shooting;
using UnityEngine;

namespace Player
{
    public class PlayerAudio : MonoBehaviour
    {
        [SerializeField] private AudioClip _shotAudioClip;
        [SerializeField] private AudioClip _explodeAudioClip;

        private AudioSource _audioSource;

        private void OnEnable()
        {
            _audioSource = GetComponent<AudioSource>();

            PlayerShoot.BulletShooted += OnBulletShoot;
            PlayerCollisions.Collided += OnCollided;
            EnemyObject.Collided += OnCollided;
        }

        private void OnBulletShoot()
        {
            _audioSource.clip = _shotAudioClip;
            _audioSource.Play();
        }

        private void OnCollided()
        {
            _audioSource.clip = _explodeAudioClip;
            _audioSource.Play();
        }

        private void OnDisable()
        {
            PlayerShoot.BulletShooted -= OnBulletShoot;
            PlayerCollisions.Collided -= OnCollided;
            EnemyObject.Collided -= OnCollided;
        }
    }
}