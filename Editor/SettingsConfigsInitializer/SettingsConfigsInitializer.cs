using System;
using System.Linq;
using System.Reflection;
using CardinalSystem.Cardinal.Editor.SettingsConfigsInitializer.Interfaces;
using UnityEditor;
using UnityEngine;

namespace CardinalSystem.Cardinal.Editor.SettingsConfigsInitializer
{
    internal static class SettingsConfigsInitializer
    {
        [MenuItem("CARDINAL/Configs/Initialize")]
        public static async void InitializeAllConfigs()
        {
            var settingsConfigurators = Assembly.Load("Assembly-CSharp").GetTypes();

            var type = typeof(ISettingConfigurationBroker);
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p));

            var enumerable = types.ToList();
            Debug.Log(enumerable.Count());
            foreach (Type t in enumerable)
            {
                if (t.GetInterface(nameof(ISettingConfigurationBroker)) == null) continue;

                var temp = (ISettingConfigurationBroker)Activator.CreateInstance(t);
                await temp.InitDirectory();
                await temp.Initialize();
            }
        }
    }
}