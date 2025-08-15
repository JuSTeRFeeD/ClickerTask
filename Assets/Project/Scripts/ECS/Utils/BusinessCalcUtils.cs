using Project.Scripts.ECS.Components;

namespace Project.Scripts.ECS.Utils
{
    public static class BusinessCalcUtils
    {
        public static int CalculateIncome(in BusinessData businessData)
        {
            var upgradesMultiplier = 1f;
            foreach (var upgrade in businessData.Upgrades)
            {
                if (upgrade.IsUnlocked)
                {
                    upgradesMultiplier += upgrade.IncomeMultiplier;
                }
            }
            return (int)(businessData.Level * businessData.BaseIncome * upgradesMultiplier);
        }
    }
}