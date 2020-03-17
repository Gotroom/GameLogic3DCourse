using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace Ermolaev_3D
{
    public sealed class ScoreUI : MonoBehaviour
    {
        private List<BaseEnemyController> _enemies;
        private Text _text;
        private int _pointsCounter;

        private void Awake()
        {
            _enemies = new List<BaseEnemyController>();
            _enemies.InsertRange(0, FindObjectsOfType<BaseEnemyController>());
            _text = GetComponent<Text>();
        }

        private void OnEnable()
        {
            foreach (var enemy in _enemies)
            {
                enemy.Killed += OnKilledEnemy;
            }
        }

        private void OnDisable()
        {
            foreach (var enemy in _enemies)
            {
                enemy.Killed -= OnKilledEnemy;
            }
        }

        private void OnKilledEnemy(BaseEnemyController sender)
        {
            var pointTxt = "очков";
            ++_pointsCounter;
            if (_pointsCounter >= 5) pointTxt = "очков";
            else if (_pointsCounter == 1) pointTxt = "очко";
            else if (_pointsCounter < 5) pointTxt = "очка";
            _text.text = $"Вы заработали {_pointsCounter} {pointTxt}";

            var deadEnemy = _enemies.Find(e => e == sender);
            deadEnemy.Killed -= OnKilledEnemy;
            deadEnemy = null;
        }
    }
}