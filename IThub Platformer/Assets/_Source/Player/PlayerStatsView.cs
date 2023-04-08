using TMPro;
using UnityEngine;

namespace Player
{
    public class PlayerStatsView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _textField;
        [SerializeField] private string _mainText;

        public void SetValue(int value)
        {
            _textField.text = $"{_mainText} {value}";
        }
    }
}

