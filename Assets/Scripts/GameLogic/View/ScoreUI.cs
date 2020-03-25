using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace Ermolaev_3D
{
    public sealed class ScoreUI : MonoBehaviour
    {
        private List<BaseEnemyModel> _enemies;
        private Text _text;
        private int _pointsCounter;

        private void Awake()
        {
            _enemies = new List<BaseEnemyModel>();
            _enemies.InsertRange(0, FindObjectsOfType<BaseEnemyModel>());
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

        public void AddEnemyReference(BaseEnemyModel enemy)
        {
            _enemies.Add(enemy);
            enemy.Killed += OnKilledEnemy;
        }

        public void RemoveEnemyReference(BaseEnemyModel enemy)
        {
            var deadEnemy = _enemies.Find(e => e == enemy);
            if (deadEnemy)
            {
                deadEnemy.Killed -= OnKilledEnemy;
                deadEnemy = null;
            }
        }

        private void OnKilledEnemy(BaseEnemyModel sender)
        {
            var pointTxt = "очков";
            ++_pointsCounter;
            if (_pointsCounter >= 5) pointTxt = "очков";
            else if (_pointsCounter == 1) pointTxt = "очко";
            else if (_pointsCounter < 5) pointTxt = "очка";
            _text.text = $"Вы заработали {_pointsCounter} {pointTxt}";

            RemoveEnemyReference(sender);
        }
    }
}