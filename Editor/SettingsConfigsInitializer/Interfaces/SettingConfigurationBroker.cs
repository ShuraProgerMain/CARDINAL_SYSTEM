using System;
using System.IO;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace CardinalSystem.Cardinal.Editor.SettingsConfigsInitializer.Interfaces
{
    public abstract class SettingConfigurationBroker
    {
        protected string DirectoryLevelOne { get; set; }
        protected string DirectoryLevelTwo { get; set; }
        protected string NameFile { get; set; }
        protected string FullPath { get; set; }
        
        public abstract Task Initialize();

        protected Task InitDirectory()
        {
            DirectoryLevelOne =  string.IsNullOrEmpty(DirectoryLevelOne) ? "/Resources" : DirectoryLevelOne;
            DirectoryLevelTwo = string.IsNullOrEmpty(DirectoryLevelTwo) ? "/ConfigCreatedWithCardinal" : DirectoryLevelTwo;
            NameFile = string.IsNullOrEmpty(NameFile) ? "Settings" : NameFile;
            
            if (!Directory.Exists(Application.dataPath + DirectoryLevelOne))
            {
                Directory.CreateDirectory(Application.dataPath + DirectoryLevelOne);
            }

            if (!Directory.Exists(Application.dataPath + DirectoryLevelOne + DirectoryLevelTwo))
            {
                Directory.CreateDirectory(Application.dataPath + DirectoryLevelOne + DirectoryLevelTwo);
                Debug.LogWarning(
                    "GameAnalytics: Resources/GameAnalytics folder is required to store settings. it was created ");
            }

            FullPath = $"Assets{DirectoryLevelOne}{DirectoryLevelTwo}/{NameFile}.asset";
            
            return Task.CompletedTask;
        }
    }

    public static class AssetInstancer
    {
        public static T Instance<T>(string path) where T : ScriptableObject
        {
            if (File.Exists(path))
            {
                return (T)AssetDatabase.LoadAssetAtPath(path, typeof(T));
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