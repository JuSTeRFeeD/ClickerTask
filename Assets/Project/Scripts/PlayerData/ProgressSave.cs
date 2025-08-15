namespace Project.Scripts.PlayerData
{
    [System.Serializable]
    public class ProgressSave
    {
        public long Balance { get; set; }
        public BusinessSaveData[] Businesses { get; set; }
    }

    [System.Serializable]
    public struct BusinessSaveData
    {
        public int Id { get; set; }
        public int Level { get; set; }
        public int IncomeProgress { get; set; }
    }
}
