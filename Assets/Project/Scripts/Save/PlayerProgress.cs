using System;

namespace Project.Scripts.Save
{
    [Serializable]
    public class PlayerProgress
    {
        public int PlayerBalance = 0;
        public BusinessSaveData[] Businesses = Array.Empty<BusinessSaveData>();
    }

    [Serializable]
    public class BusinessSaveData
    {
        public int Id;
        public int Level;
        public float IncomeProgress;
        public bool[] UpgradesStatus;
    }
}
