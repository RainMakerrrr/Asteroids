using System;
using UnityEngine;

namespace Enemies
{
    public abstract class EnemyObject : MonoBehaviour
    {
        public static event Action Collided;

        public abstract void SetTrajectory(Vector2 direction);

        public virtual void DisableObject() => Collided?.Invoke();

        protected virtual void OnCollisionEnter2D(Collision2D other) => Collided?.Invoke();
    }
}