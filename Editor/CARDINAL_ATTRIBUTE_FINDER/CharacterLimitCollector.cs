using System;
using System.Reflection;
using System.Threading.Tasks;
using Cardinal.Editor.CARDINAL_ATTRIBUTES;
using UnityEditor;
using UnityEngine;

namespace Cardinal.Editor.CARDINAL_ATTRIBUTE_FINDER
{
    public class CharacterLimitCollector : UnityEditor.Editor
    {
        [InitializeOnLoadMethod]
        public static async void Init()
        {
#if UNITY_EDITOR
            while (true)
            {
                var delay = 300;
                
                // if (Application.isPlaying)
                // {
                //     delay = 10_000;
                //     Debug.Log($"Частота опроса: {delay}");
                //     await Task.Delay(delay);
                //     continue;
                // }

                await Task.Delay(delay);

                var types = Assembly.GetExecutingAssembly().GetTypes();

                foreach (var type in types)
                {
                    Type newType = type;

                    if (newType != null)
                    {
                        var properties = newType.GetProperties();
                        var fields = newType.GetFields(BindingFlags.Public | BindingFlags.NonPublic |
                                                       BindingFlags.Instance | BindingFlags.FlattenHierarchy);

                        foreach (var propertyInfo in properties)
                        {
                            var attrs = propertyInfo.GetCustomAttributes(false);

                            foreach (var attr in attrs)
                            {
                                if (attr is CharacterLimitAttribute a)
                                {
                                    Debug.Log(propertyInfo.Name + " khm " + a.value);
                                }
                            }
                        }

                        foreach (var field in fields)
                        {
                            var attrs = field.GetCustomAttributes(false);

                            foreach (var attr in attrs)
                            {
                                if (attr is CharacterLimitAttribute a)
                                {
                                    var ad = FindObjectOfType(type);

                                    string value = field.GetValue(ad) as string;

                                    if (value != string.Empty && value.Length > 3)
                                    {
                                        var clamp = value.Length - a.value;
                                        clamp = clamp < 0 ? 0 : clamp;
                                        
                                        if(clamp > 0)
                                            value = value.Remove(a.value, clamp);

                                        field.SetValue(ad, value);
                                    }
                                }
                            }
                        }
                    }
                }
            }
#endif
            //Нужно придумать, как вырубить бесконечный цикл этой хуйни
        }
    }
}