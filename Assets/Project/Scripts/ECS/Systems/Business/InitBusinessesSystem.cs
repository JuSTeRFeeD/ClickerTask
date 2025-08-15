using System.Linq;
using Leopotam.Ecs;
using Project.Scripts.ECS.Components;
using Project.Scripts.Save;
using Project.Scripts.Scriptable;
using Project.Scripts.UI;

namespace Project.Scripts.ECS.Systems.Business
{
    public class InitBusinessesSystem : IEcsInitSystem
    {
        private readonly PlayerProgress _playerProgress;
        private readonly GameConfig _gameConfig;
        private readonly UIManager _uiManager;

        private EcsWorld _world;

        public InitBusinessesSystem(
            PlayerProgress playerProgress, 
            GameConfig gameConfig,
            UIManager uiManager
        ) {
            _playerProgress = playerProgress;
            _gameConfig = gameConfig;
            _uiManager = uiManager;
        }
        
        public void Init()
        {
            for (var index = 0; index < _gameConfig.Businesses.Length; index++)
            {
                var config = _gameConfig.Businesses[index];
                var businessEntity = _world.NewEntity();

                var loadedBusiness = _playerProgress.Businesses.FirstOrDefault(i => i.Id == config.Id);

                var upgrades = new BusinessUpgradeData[config.Upgrades.Length];
                for (var i = 0; i < upgrades.Length; i++)
                {
                    var businessUpgradeConfig = config.Upgrades[i];
                    var isUnlocked = false;
                    if (loadedBusiness != null)
                    {
                        isUnlocked = loadedBusiness.UpgradesStatus[i];
                    }
                    
                    upgrades[i] = new BusinessUpgradeData
                    {
                        Price = businessUpgradeConfig.Price,
                        IncomeMultiplier = businessUpgradeConfig.IncomeMultiplier,
                        IsUnlocked = isUnlocked
                    };
                }

                var level = index == 0 ? 1 : 0;
                var progressTime = 0f;
                if (loadedBusiness != null)
                {
                    level = loadedBusiness.Level;
                    progressTime = loadedBusiness.IncomeProgress;
                }
                
                businessEntity.Replace(new BusinessData
                {
                    BusinessId = config.Id,
                    Level = level,
                    BaseIncome = config.BaseIncome,
                    Upgrades = upgrades,
                });
                    
                businessEntity.Replace(new IncomeProgress
                {
                    IncomeDelay = config.IncomeDelaySeconds,
                    ProgressTime = progressTime
                });

                if (level > 0)
                {
                    businessEntity.Get<BusinessUnlocked>();
                }
                
                _uiManager.SpawnBusinessView(config, businessEntity);
            }
        }
    }
}