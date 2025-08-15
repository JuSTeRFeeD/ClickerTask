using System.Text;
using Leopotam.Ecs;
using TMPro;
using UnityEngine;

namespace Project.Scripts.UI
{
    public class BalanceDisplay : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _balanceText;
        
        private EcsEntity _balanceEntity;
        private readonly StringBuilder _sb = new();
        
        public void Refresh(int balanceValue)
        {
            _sb.Clear();
            _sb.Append("Баланс: ");
            _sb.Append(balanceValue);
            _sb.Append("$");
            _balanceText.SetText(_sb.ToString());
        }
    }
}