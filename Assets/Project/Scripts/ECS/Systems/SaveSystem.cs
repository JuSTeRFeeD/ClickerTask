using System.Collections.Generic;
using System.Linq;
using Leopotam.Ecs;
using Project.Scripts.ECS.Components;
using Project.Scripts.Save;

namespace Project.Scripts.ECS.Systems
{
    public class SaveSystem : IEcsDestroySystem
    {
        private readonly EcsEntity _balanceEntity;
        private EcsFilter<BusinessData, IncomeProgress, BusinessUnlocked> _filter;

        public SaveSystem(EcsEntity balanceEntity)
        {
            _balanceEntity = balanceEntity;
        }
        
        public void Destroy()
        {
            Save();
        }

        public void Save()
        {
            var businesses = new List<BusinessSaveData>();
            foreach (var i in _filter)
            {
                ref var businessData = ref _filter.Get1(i);
                ref var incomeProgress = ref _filter.Get2(i);
                
                var upgradesStatus = businessData.Upgrades.Select(upgrade => upgrade.IsUnlocked).ToArray();
                businesses.Add(new BusinessSaveData
                {
                    Id = businessData.BusinessId,
                    Level = businessData.Level,
                    UpgradesStatus = upgradesStatus,
                    IncomeProgress = incomeProgress.ProgressTime
                });
            }

            var saveData = new PlayerProgress
            {
                PlayerBalance = _balanceEntity.Get<PlayerBalance>().Value,
                Businesses = businesses.ToArray()
            };
            SaveLoad.Save(saveData);
        }
    }
}