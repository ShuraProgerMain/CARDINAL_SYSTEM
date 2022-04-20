using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace CardinalSystem.Cardinal.Editor.EditorExtensions.HierarhyCreator
{
    public class HierarhyInitialize : EditorWindow
    {
        private static readonly List<GameObject> Objects = new();
    
        [MenuItem("CARDINAL/EditorExtension/Create hierarhy %#H")]
        public static void CreateHierarhy()
        {
            NewHierarhyObjects();
        }
    
        [MenuItem("CARDINAL/EditorExtension/Delete hierarhy %&H")]
        public static void DeleteHierarhy()
        {
            if(Objects.Count == 0) return;

            for (int i = 0; i < Objects.Count; i++)
            {
                DestroyImmediate(Objects[i]);
            }
        
            Objects.Clear();
        }

        private static void NewHierarhyObjects()
        {
            //Переписать эту хуету под что-то нормальное
            
            //Первый порядок
            var globalObject = new GameObject("[GLOBAL]");
            var renderingObject = new GameObject("[RENDERING]");
            var eventsObject = new GameObject("[EVENTS]");
            var uiObject = new GameObject("[UI]");
        
            Objects.Add(globalObject);
            Objects.Add(renderingObject);
            Objects.Add(eventsObject);
            Objects.Add(uiObject);
        
            //Второй порядок
            CreateSubobjectInGameObject(globalObject.transform);
            CreateSubobjectInGameObject(eventsObject.transform);
            CreateSubobjectInRendering(renderingObject.transform);
        
        }

        private static void CreateSubobjectInGameObject(Transform parent)
        {
            new GameObject("{Static}").transform.parent = parent;
            new GameObject("{Active}").transform.parent = parent;
        }
    
        private static void CreateSubobjectInRendering(Transform parent)
        {
            new GameObject("{Main}").transform.parent = parent;
            new GameObject("{Virtual}").transform.parent = parent;
            new GameObject("{Light}").transform.parent = parent;
        }
    }
}
