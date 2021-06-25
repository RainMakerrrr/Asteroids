using UnityEngine;

namespace Player
{
    public class PlayerBounds : MonoBehaviour
    {
        private float _minX, _maxX, _minY, _maxY;
        private Camera _camera;

        private void Start()
        {
            _camera = Camera.main;

            var cameraDistance = Vector3.Distance(transform.position, _camera.transform.position);
            Vector2 bottomCorner = _camera.ViewportToWorldPoint(new Vector3(0, 0, cameraDistance));
            Vector2 topCorner = _camera.ViewportToWorldPoint(new Vector3(1, 1, cameraDistance));

            _minX = bottomCorner.x + 1f;
            _maxX = topCorner.x - 1f;
            _minY = bottomCorner.y + 1f;
            _maxY = topCorner.y - 1f;
        }

        private void Update()
        {
            var position = transform.position;

            if (position.x < _minX) position.x = _minX;
            if (position.x > _maxX) position.x = _maxX;

            if (position.y < _minY) position.y = _minY;
            if (position.y > _maxY) position.y = _maxY;

            transform.position = position;
        }
    }
}