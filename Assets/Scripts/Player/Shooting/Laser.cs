using System.Collections;
using Enemies;
using UnityEngine;


namespace Player.Shooting
{
    public class Laser : MonoBehaviour
    {
        [SerializeField] private float _rayDistance = 100f;
        private LineRenderer _lineRenderer;

        private void OnEnable()
        {
            _lineRenderer = GetComponent<LineRenderer>();
            StartCoroutine(DisableLaserWithDelay());
        }

        private IEnumerator DisableLaserWithDelay()
        {
            yield return new WaitForSeconds(3f);

            gameObject.SetActive(false);
        }

        private void Update() => ShootLaser();

        private void ShootLaser()
        {
            var hit = Physics2D.Raycast(transform.position, transform.up);
            _lineRenderer.SetPosition(0, transform.position);

            if (hit)
            {
                _lineRenderer.SetPosition(1, hit.point);
                var enemyObject = hit.collider.GetComponent<EnemyObject>();
                if (enemyObject == null) return;

                enemyObject.DisableObject();
            }
            else _lineRenderer.SetPosition(1, transform.up * _rayDistance);
        }
    }
}