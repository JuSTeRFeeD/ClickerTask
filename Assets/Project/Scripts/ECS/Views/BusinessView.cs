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
        
        [Header("Buttons")]
        [SerializeField] private Button _lvlUpButton;
        [SerializeField] private TextMeshProUGUI _lvlUpButtonText;

        private EcsEntity _entity;
        private BusinessConfig _businessConfig;

        private void Start()
        {
            _progressFill.fillAmount = 0f;
            _lvlUpButton.onClick.AddListener(HandleLvlUpClick);
        }

        public void Initialize(BusinessConfig config, EcsEntity businessEntity)
        {
            _businessConfig = config;
            _entity = businessEntity;
            _titleText.text = config.Title;
            RefreshInfo();
        }

        private void HandleLvlUpClick()
        {
            _entity.Get<BuyBusinessLevelUpOneFrame>();
        }

        public void RefreshInfo()
        {
            ref readonly var businessData = ref _entity.Get<BusinessData>();
            
            var income = BusinessCalcUtils.CalculateIncome(businessData);
            _incomeText.text = $"Доход\n{income}$";
            
            _levelText.text = $"LVL\n{_entity.Get<BusinessData>().Level}";
            _lvlUpButtonText.text = $"LVL UP\nЦена: {_businessConfig.GetLevelUpPrice(businessData.Level)}$";
        }

        private void Update()
        {
            if (_entity.Has<IncomeProgress>())
            {
                ref readonly var progress = ref _entity.Get<IncomeProgress>();
                _progressFill.fillAmount = progress.ProgressTime / progress.IncomeDelay;
            }
        }
    }
}