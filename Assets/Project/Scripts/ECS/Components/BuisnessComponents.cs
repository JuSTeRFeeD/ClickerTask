using Leopotam.Ecs;

namespace Project.Scripts.ECS.Components
{
    public struct BusinessData
    {
        public int BusinessId;
        public int Level;
        public float BaseIncome;
        public BusinessUpgradeData[] Upgrades;
    }
    
    public struct BusinessUnlocked : IEcsIgnoreInFilter { }

    public struct BusinessUpgradeData
    {
        public bool IsUnlocked;
        public int Price;
        public float IncomeMultiplier;
    }

    public struct IncomeProgress
    {
        public float ProgressTime;
        public float IncomeDelay;
    }

    public struct BuyBusinessUpgrade
    {
        public int UpgradeIndex;
    }

    public struct GiveIncomeOneFrame : IEcsIgnoreInFilter { }

    public struct BuyBusinessLevelUpOneFrame : IEcsIgnoreInFilter { }
    
    public struct LevelUpBusinessOneFrame : IEcsIgnoreInFilter { }
    
    public struct BusinessUpdateInfoOneFrame : IEcsIgnoreInFilter { }
}