using Leopotam.Ecs;
using Project.Scripts.ECS.Components;
using Project.Scripts.UI;

namespace Project.Scripts.ECS.Systems.Player
{
    public class UIBalanceUpdateSystem : IEcsRunSystem
    {
        private readonly BalanceDisplay _balanceDisplay;

        private EcsFilter<PlayerBalance, BalanceChangedOneFrame> _filter;

        public UIBalanceUpdateSystem(BalanceDisplay balanceDisplay)
        {
            _balanceDisplay = balanceDisplay;
        }
        
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref readonly var balance = ref _filter.Get1(i);
                _balanceDisplay.Refresh(balance.Value);
            }
        }
    }
}