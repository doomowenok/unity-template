using UnityEngine;

namespace Gameplay.Services.SaveLoad
{
    public sealed class PlayerPrefsSavingService : ISavingService
    {
        public bool TryLoad<TData>(string key, out TData save)
        {
            if (!PlayerPrefs.HasKey(key))
            {
                save = default;
                return false;
            }

            string jsonWithSaveData = PlayerPrefs.GetString(key);
            save = JsonUtility.FromJson<TData>(jsonWithSaveData);

            return save != null;
        }

        public void Save<TData>(string key, TData data)
        {
            string jsonWithData = JsonUtility.ToJson(data);
            PlayerPrefs.SetString(key, jsonWithData);
            PlayerPrefs.Save();
        }
    }
}