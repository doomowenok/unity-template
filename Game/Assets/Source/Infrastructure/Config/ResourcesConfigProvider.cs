using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Infrastructure.Config
{
    public sealed class ResourcesConfigProvider : IConfigProvider
    {
        private const string BaseConfigPath = "Configs";
        
        private readonly Dictionary<Type, List<ScriptableObject>> _cachedConfigs = new Dictionary<Type, List<ScriptableObject>>();

        public TConfig GetConfig<TConfig>() where TConfig : ScriptableObject
        {
            if (_cachedConfigs.TryGetValue(typeof(TConfig), out var collection) && collection.Count > 0)
            {
                Debug.Log($"Get cached config for {typeof(TConfig)}.");
                return collection.First() as TConfig;
            }

            TConfig[] configs = Resources.LoadAll<TConfig>($"{BaseConfigPath}/{typeof(TConfig).Name}");

            if (configs.Length == 0)
            {
                Debug.LogError($"No config found with type {typeof(TConfig).Name}!");
                return null;
            }

            _cachedConfigs.Add(typeof(TConfig), configs.Cast<ScriptableObject>().ToList());

            Debug.Log($"Get {typeof(TConfig).Name} config and add to cache.");
            return configs.First();
        }

        public TConfig GetConfig<TConfig>(string folder) where TConfig : ScriptableObject
        {
            if (_cachedConfigs.TryGetValue(typeof(TConfig), out var collection) && collection.Count > 0)
            {
                Debug.Log($"Get cached config for {typeof(TConfig)}.");
                return collection.First() as TConfig;
            }
            
            TConfig[] configs = Resources.LoadAll<TConfig>($"{BaseConfigPath}/{folder}/{typeof(TConfig).Name}");

            if (configs.Length == 0)
            {
                Debug.LogError($"No config found with type {typeof(TConfig).Name}!");
                return null;
            }
            
            _cachedConfigs.Add(typeof(TConfig), configs.Cast<ScriptableObject>().ToList());

            Debug.Log($"Get {typeof(TConfig).Name} config and add to cache.");
            return configs.First();
        }

        public IEnumerable<TConfig> GetConfigs<TConfig>() where TConfig : ScriptableObject
        {
            if (_cachedConfigs.TryGetValue(typeof(TConfig), out var collection) && collection.Count > 0)
            {
                Debug.Log($"Get cached configs for {typeof(TConfig)}.");
                return collection.Cast<TConfig>();
            }
            
            TConfig[] configs = Resources.LoadAll<TConfig>($"{BaseConfigPath}/{typeof(TConfig).Name}");

            if (configs.Length == 0)
            {
                Debug.LogError($"No config found with type {typeof(TConfig).Name}!");
                return null;
            }
            
            _cachedConfigs.Add(typeof(TConfig), configs.Cast<ScriptableObject>().ToList());

            Debug.Log($"Get {typeof(TConfig).Name} config and add to cache.");
            return configs;
        }

        public IEnumerable<TConfig> GetConfigs<TConfig>(string folder) where TConfig : ScriptableObject
        {
            if (_cachedConfigs.TryGetValue(typeof(TConfig), out var collection) && collection.Count > 0)
            {
                Debug.Log($"Get cached configs for {typeof(TConfig)}.");
                return collection.Cast<TConfig>();
            }
            
            TConfig[] configs = Resources.LoadAll<TConfig>($"{BaseConfigPath}/{folder}/{typeof(TConfig).Name}");

            if (configs.Length == 0)
            {
                Debug.LogError($"No config found with type {typeof(TConfig).Name}!");
                return null;
            }
            
            _cachedConfigs.Add(typeof(TConfig), configs.Cast<ScriptableObject>().ToList());

            Debug.Log($"Get {typeof(TConfig).Name} config and add to cache.");
            return configs;
        }
    }
}