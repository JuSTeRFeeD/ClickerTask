using UnityEngine;

namespace Project.Scripts.Save
{
    public static class SaveLoad
    {
        private const string SavePath = "save";
        
        public static void Save(PlayerProgress data)
        {
            var json = JsonUtility.ToJson(data);
            PlayerPrefs.SetString(SavePath, json);
        }

        public static PlayerProgress Load()
        {
            if (PlayerPrefs.HasKey(SavePath))
            {
                var json = PlayerPrefs.GetString(SavePath);
                return JsonUtility.FromJson<PlayerProgress>(json);
            }
            return new PlayerProgress();
        }
    }
}