using Player.Collisions;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class GameState : MonoBehaviour
    {
        [SerializeField] private Image _losePanel;

        private void OnEnable() => PlayerCollisions.Collided += OnCollided;

        private void OnCollided() => _losePanel.gameObject.SetActive(true);

        private void OnDisable() => PlayerCollisions.Collided -= OnCollided;

        public void RestartLevel() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}