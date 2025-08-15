using UnityEngine;

namespace Project.Scripts.Scriptable
{
    [CreateAssetMenu(menuName = "Configs/BusinessConfig")]
    public class BusinessConfig : ScriptableObject
    {
        [SerializeField] private int _id;
        [Space]
        [SerializeField] private string _title;
        [SerializeField] private int _basePrice = 1;
        [Space]
        [SerializeField] private int _baseIncome = 1;
        [SerializeField] private float _incomeDelaySeconds = 1f;
        [Space]
        [SerializeField] private BusinessUpgrade[] _upgrades;

        public int Id => _id;
        public string Title => _title;
        public int BasePrice => _basePrice;
        public int BaseIncome => _baseIncome;
        public float IncomeDelaySeconds => _incomeDelaySeconds;
        public BusinessUpgrade[] Upgrades => _upgrades;

        public int GetLevelUpPrice(int curLevel)
        {
            return _baseIncome * (curLevel + 1);
        }
    }
}