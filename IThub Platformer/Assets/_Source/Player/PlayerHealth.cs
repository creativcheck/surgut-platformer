using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Supporting;

namespace Player
{
    public class PlayerHealth
    {
        private int _health;
        private PlayerStatsView _healthView;
        
        public PlayerHealth(PlayerStatsView view, int defaultHealth = 5)
        {
            _healthView = view;
            
            Health = defaultHealth;
        }

        private int Health
        {
            get => _health;
            set
            {
                _health = value;

                if(_health <= 0)
                {
                    SceneLoader.ReloadLevel(); // ���� ������ ������ �� ����� � ��������� ������, ������� ������ � �.�.

                    return;
                }

                _healthView.SetValue(_health);
            }
        }

        public void ChangeHealth(int value = 1) => Health += value;

    }

}
