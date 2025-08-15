using Leopotam.Ecs;
using Project.Scripts.ECS.Components;
using Project.Scripts.PlayerData;
using Project.Scripts.Scriptable;
using Project.Scripts.UI;

namespace Project.Scripts.ECS.Systems.Business
{
    public class InitBusinessesSystem : IEcsInitSystem
    {
        private readonly ProgressSave _progressSave;
        private readonly GameConfig _gameConfig;
        private readonly UIManager _uiManager;

        private EcsWorld _world;

        public InitBusinessesSystem(
            ProgressSave progressSave, 
            GameConfig gameConfig,
            UIManager uiManager
        ) {
            _progressSave = progressSave;
            _gameConfig = gameConfig;
            _uiManager = uiManager;
        }
        
        public void Init()
        {
            for (var index = 0; index < _gameConfig.Businesses.Length; index++)
            {
                var config = _gameConfig.Businesses[index];
                var businessEntity = _world.NewEntity();

                var upgrades = new BusinessUpgradeData[config.Upgrades.Length];
                for (var i = 0; i < upgrades.Length; i++)
                {
                    var businessUpgradeConfig = config.Upgrades[i];
                    upgrades[i] = new BusinessUpgradeData
                    {
                        Price = businessUpgradeConfig.Price,
                        IncomeMultiplier = businessUpgradeConfig.IncomeMultiplier,
                        IsUnlocked = false
                    };
                }

                if (index == 0)
                {
                    businessEntity.Replace(new BusinessData
                    {
                        BusinessId = config.Id,
                        Level = 1,
                        BaseIncome = config.BaseIncome,
                        Upgrades = upgrades,
                    });
                    
                    businessEntity.Replace(new IncomeProgress
                    {
                        IncomeDelay = config.IncomeDelaySeconds,
                        ProgressTime = 0
                    });
                }
                else
                {
                    businessEntity.Replace(new BusinessData
                    {
                        BusinessId = config.Id,
                        Level = 0,
                        BaseIncome = config.BaseIncome,
                        Upgrades = upgrades
                    });
                }
                
                _uiManager.SpawnBusinessView(config, businessEntity);
            }
        }
    }
}