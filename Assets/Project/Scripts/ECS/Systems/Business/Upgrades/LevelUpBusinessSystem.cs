using Leopotam.Ecs;
using Project.Scripts.ECS.Components;
using Project.Scripts.Scriptable;

namespace Project.Scripts.ECS.Systems.Business.Upgrades
{
    public class LevelUpBusinessSystem : IEcsRunSystem
    {
        private readonly GameConfig _gameConfig;
        
        private EcsFilter<BusinessData, LevelUpBusinessOneFrame> _filter;

        public LevelUpBusinessSystem(GameConfig gameConfig)
        {
            _gameConfig = gameConfig;
        }
        
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var businessData = ref _filter.Get1(i);
                businessData.Level++;
                
                ref readonly var entity = ref _filter.GetEntity(i);
                entity.Get<BusinessUpdateInfoOneFrame>();
                entity.Get<BusinessUnlocked>();
            }
        }
    }
}