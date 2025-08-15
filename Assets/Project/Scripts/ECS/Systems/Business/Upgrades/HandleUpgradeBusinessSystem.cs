using Leopotam.Ecs;
using Project.Scripts.ECS.Components;

namespace Project.Scripts.ECS.Systems.Business.Upgrades
{
    public class HandleUpgradeBusinessSystem : IEcsRunSystem
    {
        private readonly EcsEntity _balanceEntity;
        
        private EcsFilter<BusinessData, BuyBusinessUpgrade> _filter;

        public HandleUpgradeBusinessSystem(EcsEntity balanceEntity)
        {
            _balanceEntity = balanceEntity;
        }
        
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref readonly var entity = ref _filter.GetEntity(i);
                ref var data = ref _filter.Get1(i);
                ref var upgradeIndex = ref _filter.Get2(i).UpgradeIndex;
                ref var balance = ref _balanceEntity.Get<PlayerBalance>();
                
                ref var upgrade = ref data.Upgrades[upgradeIndex];
                
                if (upgrade.IsUnlocked || balance.Value < upgrade.Price)
                {
                    entity.Del<BuyBusinessUpgrade>();
                    continue;
                }
                
                balance.Value -= upgrade.Price;
                _balanceEntity.Get<BalanceChangedOneFrame>();
                
                upgrade.IsUnlocked = true;
                entity.Get<BusinessUpdateInfoOneFrame>();
                entity.Del<BuyBusinessUpgrade>();
            }
        }
    }
}