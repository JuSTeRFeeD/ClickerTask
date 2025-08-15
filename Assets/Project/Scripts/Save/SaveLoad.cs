using UnityEngine;

namespace Project.Scripts.Save
{
    public class SaveLoad
    {
        public static void Save(PlayerProgress data)
        {
            var json = JsonUtility.ToJson(data);
            PlayerPrefs.SetString("save", json);
        }

        public static PlayerProgress Load()
        {
            if (PlayerPrefs.HasKey("save"))
            {
                var json = PlayerPrefs.GetString("save");
                return JsonUtility.FromJson<PlayerProgress>(json);
            }
            return new PlayerProgress();
        }
    }
}