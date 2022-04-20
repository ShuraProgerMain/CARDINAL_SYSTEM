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

            var type = typeof(SettingConfigurationBroker);
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p));

            var enumerable = types.ToList();
            foreach (Type t in enumerable)
            {
                if (t.IsAbstract) continue;

                var chm = Activator.CreateInstance(t);
                var method = type.GetMethod("Initialize");
                if (method != null) method.Invoke(chm, new object[] { });
            }
        }
    }
}