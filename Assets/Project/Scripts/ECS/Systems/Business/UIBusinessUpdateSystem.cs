using Leopotam.Ecs;
using Project.Scripts.ECS.Components;
using Project.Scripts.UI;

namespace Project.Scripts.ECS.Systems.Business
{
    public class UIBusinessUpdateSystem : IEcsRunSystem
    {
        private readonly UIManager _uiManager;
        
        private EcsFilter<BusinessData, BusinessUpdateInfoOneFrame> _filter;

        public UIBusinessUpdateSystem(UIManager uiManager)
        {
            _uiManager = uiManager;
        }
        
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref readonly var data = ref _filter.Get1(i);
                var view = _uiManager.GetBusinessView(data.BusinessId);
                if (!view) continue;
                
                view.RefreshInfo();
            }
        }
    }
}