using System;
using System.IO;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace CardinalSystem.Cardinal.Editor.SettingsConfigsInitializer.Interfaces
{
    public interface ISettingConfigurationBroker
    {
        public string DirectoryLevelOne { get; set; }
        public string DirectoryLeveTwo { get; set; }
        public string NameFile { get; set; }
        public string FullPath { get; set; }
        
        public Task Initialize();

        public Task InitDirectory()
        {
            DirectoryLevelOne =  DirectoryLevelOne == String.Empty ? "/Resources" : DirectoryLevelOne;
            DirectoryLeveTwo = DirectoryLeveTwo == String.Empty ? "/ConfigCreatedWithCardinal" : DirectoryLeveTwo;
            NameFile = NameFile == String.Empty ? "Settings" : NameFile;
            
            if (!Directory.Exists(Application.dataPath + "/Resources"))
            {
                Directory.CreateDirectory(Application.dataPath + "/Resources");
            }

            if (!Directory.Exists(Application.dataPath + "/Resources/GameAnalytics"))
            {
                Directory.CreateDirectory(Application.dataPath + "/Resources/GameAnalytics");
                Debug.LogWarning(
                    "GameAnalytics: Resources/GameAnalytics folder is required to store settings. it was created ");
            }

            FullPath = $"Assets/Resources/GameAnalytics/{NameFile}.asset";
            
            return Task.CompletedTask;
        }
    }

    public static class AssetInstancer
    {
        public static T Instance<T>(string path) where T : ScriptableObject
        {
            if (File.Exists(path))
            {
                AssetDatabase.DeleteAsset(path);
                AssetDatabase.Refresh();
            }

            var asset = ScriptableObject.CreateInstance<T>();
            AssetDatabase.CreateAsset(asset, path);
            AssetDatabase.Refresh();

            AssetDatabase.SaveAssets();
            // Debug.LogWarning("GameAnalytics: Settings file didn't exist and was created");
            Selection.activeObject = asset;

            return asset;
        }
    }
}