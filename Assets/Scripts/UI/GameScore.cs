using Enemies;
using TMPro;
using UnityEngine;

namespace UI
{
    public class GameScore : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _scoreText;
        private int _score;

        private void OnEnable() => EnemyObject.Collided += OnCollided;

        private void OnCollided()
        {
            _score += Random.Range(10, 40);
            _scoreText.text = _score.ToString();
        }

        private void OnDisable() => EnemyObject.Collided -= OnCollided;
    }
}