using Leopotam.Ecs;
using Project.Scripts.ECS.Components;
using Project.Scripts.ECS.Systems;
using Project.Scripts.ECS.Systems.Business;
using Project.Scripts.ECS.Systems.Business.Upgrades;
using Project.Scripts.ECS.Systems.Player;
using Project.Scripts.Save;
using Project.Scripts.Scriptable;
using Project.Scripts.UI;
using UnityEngine;

namespace Project.Scripts.ECS
{
    public class EcsBootstrapper : MonoBehaviour
    {
        [SerializeField] private GameConfig _gameConfig;
        [SerializeField] private UIManager _uiManager;

        private EcsWorld _world;
        private EcsSystems _systems;

        private PlayerProgress _playerProgress;

        private void Start()
        {
            _playerProgress = SaveLoad.Load();
            _world = new EcsWorld();
            _systems = new EcsSystems(_world);

            var balanceEntity = InitBalanceEntity(_playerProgress?.PlayerBalance ?? 0);

            _systems
                .Add(new InitBusinessesSystem(_playerProgress, _gameConfig, _uiManager))
                .Add(new BusinessProgressSystem())
                .Add(new GiveIncomeSystem(balanceEntity))
                
                .Add(new HandleLevelUpBusinessSystem(balanceEntity, _gameConfig))
                .Add(new LevelUpBusinessSystem(_gameConfig))
                .Add(new HandleUpgradeBusinessSystem(balanceEntity))
                
                .Add(new UIBalanceUpdateSystem(_uiManager.BalanceDisplay))
                .Add(new UIBusinessUpdateSystem(_uiManager))
                
                .Add(new SaveSystem(balanceEntity))
            ;

            _systems
                .OneFrame<GiveIncomeOneFrame>()
                .OneFrame<BalanceChangedOneFrame>()
                .OneFrame<BuyBusinessLevelUpOneFrame>()
                .OneFrame<LevelUpBusinessOneFrame>()
            ;
                
            _systems.Init();
        }

        private EcsEntity InitBalanceEntity(int initialBalance)
        {
            var balanceEntity = _world.NewEntity();
            balanceEntity.Get<PlayerBalance>().Value = initialBalance;
            balanceEntity.Get<BalanceChangedOneFrame>();
            return balanceEntity;
        }

        private void Update()
        {
            _systems.Run();
        }

        private void OnDestroy()
        {
            _systems.Destroy();
            _world.Destroy();
        }
    }
}