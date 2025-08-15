using Project.Scripts.ECS.Components;

namespace Project.Scripts.ECS.Utils
{
    public static class BusinessCalcUtils
    {
        public static int CalculateIncome(in BusinessData businessData)
        {
            return (int)(businessData.Level * businessData.BaseIncome * (1 + businessData.IncomeMultiplier));
        }
    }
}