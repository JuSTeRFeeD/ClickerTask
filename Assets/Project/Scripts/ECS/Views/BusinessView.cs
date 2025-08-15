using System.Text;
using Leopotam.Ecs;
using Project.Scripts.ECS.Components;
using Project.Scripts.ECS.Utils;
using Project.Scripts.Scriptable;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Scripts.ECS.Views
{
    public class BusinessView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _titleText;
        [SerializeField] private TextMeshProUGUI _levelText;
        [SerializeField] private TextMeshProUGUI _incomeText;
        [Space]
        [SerializeField] private Image _progressFill;
        
        [Header("Level Up")]
        [SerializeField] private Button _levelUpButton;
        [SerializeField] private TextMeshProUGUI _levelUpButtonText;
        
        [Header("Upgrades")]
        [SerializeField] private UpgradeButtonTuple[] _upgradeButtons;

        [System.Serializable]
        public struct UpgradeButtonTuple
        {
            public Button Button;
            public TextMeshProUGUI ButtonText;
        }
        
        private EcsEntity _entity;
        private BusinessConfig _businessConfig;

        private void Start()
        {
            _progressFill.fillAmount = 0f;
            _levelUpButton.onClick.AddListener(HandleLevelUpClick);
            
            for (var i = 0; i < _upgradeButtons.Length; i++)
            {
                var upgradeIndex = i;
                _upgradeButtons[i].Button.onClick.AddListener(() => HandleUpgradeClick(upgradeIndex));
            }
        }

        private void HandleUpgradeClick(int upgradeIndex)
        {
            if (upgradeIndex < 0 || upgradeIndex > _businessConfig.Upgrades.Length) return;
            _entity.Get<BuyBusinessUpgrade>().UpgradeIndex = upgradeIndex;
        }

        public void Initialize(BusinessConfig config, EcsEntity businessEntity)
        {
            _businessConfig = config;
            _entity = businessEntity;
            _titleText.text = config.Title;
            RefreshInfo();
        }

        private void HandleLevelUpClick()
        {
            _entity.Get<BuyBusinessLevelUpOneFrame>();
        }

        public void RefreshInfo()
        {
            ref readonly var businessData = ref _entity.Get<BusinessData>();
            
            var income = BusinessCalcUtils.CalculateIncome(businessData);
            _incomeText.text = $"Доход\n{income}$";
            _levelText.text = $"LVL\n{_entity.Get<BusinessData>().Level}";
            _levelUpButtonText.text = $"LVL UP\nЦена: {_businessConfig.GetLevelUpPrice(businessData.Level)}$";

            // Upgrade buttons
            var sb = new StringBuilder();
            for (var index = 0; index < businessData.Upgrades.Length; index++)
            {
                ref readonly var upgrade = ref businessData.Upgrades[index];
                var upgradeConfig = _businessConfig.Upgrades[index];

                sb.Clear();
                sb.Append(upgradeConfig.Title);
                sb.Append("\nДоход: +");
                sb.Append((int)(upgradeConfig.IncomeMultiplier * 100));
                if (upgrade.IsUnlocked)
                {
                    sb.Append("%\nКуплено");
                }
                else
                {
                    sb.Append("%\nЦена: ");
                    sb.Append(upgradeConfig.Price);
                    sb.Append("$");
                }
                _upgradeButtons[index].ButtonText.text = sb.ToString();
            }

        }

        private void Update()
        {
            if (_entity.Has<BusinessUnlocked>())
            {
                ref readonly var progress = ref _entity.Get<IncomeProgress>();
                _progressFill.fillAmount = progress.ProgressTime / progress.IncomeDelay;
            }
        }
    }
}