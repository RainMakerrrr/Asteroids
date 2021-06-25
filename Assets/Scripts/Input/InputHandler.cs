using System;
using UnityEngine;

namespace Input
{
    public class InputHandler
    {
        public event Action BulletShoot;
        public event Action LaserShoot;

        private readonly PlayerControls _playerControls;

        public Vector2 Movement => _playerControls.Player.Movement.ReadValue<Vector2>();


        public InputHandler()
        {
            _playerControls = new PlayerControls();
            _playerControls.Enable();

            _playerControls.Player.BulletShoot.started += context => BulletShoot?.Invoke();
            _playerControls.Player.LaserShoot.started += context => LaserShoot?.Invoke();
        }
    }
}