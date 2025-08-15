using Leopotam.Ecs;
using Project.Scripts.ECS.Components;
using Project.Scripts.ECS.Utils;
using UnityEngine;

namespace Project.Scripts.ECS.Systems.Business
{
    public class GiveIncomeSystem : IEcsRunSystem
    {
        private readonly EcsEntity _balanceEntity;
        
        private EcsFilter<BusinessData, GiveIncomeOneFrame> _filter;

        public GiveIncomeSystem(EcsEntity balanceEntity)
        {
            _balanceEntity = balanceEntity;
        }
        
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref readonly var businessData = ref _filter.Get1(i);
                var income = BusinessCalcUtils.CalculateIncome(businessData);

                ref var balance = ref _balanceEntity.Get<PlayerBalance>();
                balance.Value += income;
                
                _balanceEntity.Get<BalanceChangedOneFrame>();
            }
        }
    }
}