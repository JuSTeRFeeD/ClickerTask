using Leopotam.Ecs;
using Project.Scripts.ECS.Components;
using UnityEngine;

namespace Project.Scripts.ECS.Systems.Business
{
    public class BusinessProgressSystem : IEcsRunSystem
    {
        private EcsFilter<IncomeProgress, BusinessUnlocked> _filter;

        public void Run()
        {
            var dt = Time.deltaTime;
            foreach (var i in _filter)
            {
                ref var progress = ref _filter.Get1(i);

                progress.ProgressTime += dt;
                if (progress.ProgressTime < progress.IncomeDelay)
                {
                    continue;
                }
                
                progress.ProgressTime = 0;

                ref readonly var entity = ref _filter.GetEntity(i);
                entity.Get<GiveIncomeOneFrame>();
            }
        }
    }
}