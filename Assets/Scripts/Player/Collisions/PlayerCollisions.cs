using System;
using System.Collections;
using UnityEngine;

namespace Player.Collisions
{
    public class PlayerCollisions : MonoBehaviour
    {
        public static event Action Collided;

        private void OnCollisionEnter2D(Collision2D other)
        {
            Collided?.Invoke();
            Destroy(gameObject);
        }
    }
}