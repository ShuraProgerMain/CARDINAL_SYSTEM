using System;
using UnityEditor;
using UnityEngine;

namespace Cardinal.Editor.CARDINAL_EDITOR.Shortcuts.EditorShortcuts
{
    public class NewBehaviourScript : UnityEditor.AssetModificationProcessor
    {
        public const string PATH_TO_MY_TEMPLATE_SCRIPT =
            "Library/PackageCache/com.stupidshitcreate.cardinalsystem@6b4c5c8406/Editor/CARDINAL_EDITOR/Shortcuts/ScriptTemplates/Template.txt";
        

        [MenuItem("CARDINAL/EditorExtension/Create folder %F")]
        public static void CreateFolder()
        {
            ProjectWindowUtil.CreateFolder();
        }
        
        [MenuItem("CARDINAL/EditorExtension/Create script %G")]
        public static void CreateScript()
        {
            ProjectWindowUtil.CreateScriptAssetFromTemplateFile(PATH_TO_MY_TEMPLATE_SCRIPT, "Behaviour.cs");
        }
        
        private static void OnWillCreateAsset(string path)
        {
            path = path.Replace(".meta", "");
            
            var index = path.LastIndexOf(".", StringComparison.Ordinal);
            
            if(index < 0) return;
            
            var file = path.Substring(index);
            
            if(file != ".cs" && file != ".js" && file != ".boo") return;
            
            file = System.IO.File.ReadAllText(path);

            var lastPart = path.Substring(path.IndexOf("/", StringComparison.Ordinal));
            
            var nameSpace = lastPart.Substring(0, lastPart.LastIndexOf('/'));
            nameSpace = nameSpace.Replace('/', '.');
            nameSpace = nameSpace.TrimStart('.');
            nameSpace = nameSpace.Replace(" ", "");
            
            file = file.Replace("#NAMESPACE#", nameSpace);

            System.IO.File.WriteAllText(path, file);
            AssetDatabase.Refresh();
        }
    }
}
