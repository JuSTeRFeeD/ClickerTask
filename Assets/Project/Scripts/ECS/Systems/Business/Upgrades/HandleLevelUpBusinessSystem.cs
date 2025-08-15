using Leopotam.Ecs;
using Project.Scripts.ECS.Components;
using Project.Scripts.Scriptable;

namespace Project.Scripts.ECS.Systems.Business.Upgrades
{
    public class HandleLevelUpBusinessSystem : IEcsRunSystem
    {
        private readonly EcsEntity _balanceEntity;
        private readonly GameConfig _gameConfig;

        private EcsFilter<BusinessData, BuyBusinessLevelUpOneFrame> _filter;

        public HandleLevelUpBusinessSystem(EcsEntity balanceEntity, GameConfig gameConfig)
        {
            _balanceEntity = balanceEntity;
            _gameConfig = gameConfig;
        }
        
        public void Run()
        {
            ref var balance = ref _balanceEntity.Get<PlayerBalance>();
            foreach (var i in _filter)
            {
                ref var businessData = ref _filter.Get1(i);
                var config = _gameConfig.GetBusinessById(businessData.BusinessId);
                if (!config)
                {
                    continue;
                }

                var cost = config.GetLevelUpPrice(businessData.Level);
                if (balance.Value < cost)
                {
                    continue;
                }

                balance.Value -= cost;
                _balanceEntity.Get<BalanceChangedOneFrame>();
                
                ref var entity = ref _filter.GetEntity(i);
                entity.Get<LevelUpBusinessOneFrame>();
            }
        }
    }
}