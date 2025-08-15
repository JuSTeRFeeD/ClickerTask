using UnityEngine;

namespace Project.Scripts.Scriptable
{
    [System.Serializable]
    public class BusinessUpgrade
    {
        [SerializeField] private string _title;
        [SerializeField] private int _price;
        [SerializeField] private float _incomeMultiplierPercent = 0f;
        
        public string Title => _title;
        public int Price => _price;
        public float IncomeMultiplierPercent => _incomeMultiplierPercent;
    }
}