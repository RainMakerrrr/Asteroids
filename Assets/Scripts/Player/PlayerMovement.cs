using Input;
using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float _movementSpeed;
        [SerializeField] private float _rotationSpeed;

        private InputHandler _inputHandler;
        private Rigidbody2D _rigidbody;

        private void Start()
        {
            _inputHandler = new InputHandler();
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            _rigidbody.AddForce(transform.up * (_inputHandler.Movement.y * _movementSpeed));

            _rigidbody.AddTorque(-_inputHandler.Movement.x * _rotationSpeed);
        }
    }
}