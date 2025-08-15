using System.Collections.Generic;
using Leopotam.Ecs;
using Project.Scripts.ECS.Views;
using Project.Scripts.Scriptable;
using UnityEngine;

namespace Project.Scripts.UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private BalanceDisplay _balanceDisplay;
        [Space]
        [SerializeField] private RectTransform _businessesContainer;
        [SerializeField] private BusinessView _businessViewPrefab;
        
        public BalanceDisplay BalanceDisplay => _balanceDisplay;
        
        private readonly Dictionary<int, BusinessView> _businesses = new();

        public void SpawnBusinessView(BusinessConfig businessConfig, EcsEntity businessEntity)
        {
            var spawned = Instantiate(_businessViewPrefab, _businessesContainer);
            spawned.Initialize(businessConfig, businessEntity);
            _businesses.Add(businessConfig.Id, spawned);
        }
        
        public BusinessView GetBusinessView(int businessId) => _businesses[businessId];
    }
}