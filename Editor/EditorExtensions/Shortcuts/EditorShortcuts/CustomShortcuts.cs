using System;
using System.IO;
using UnityEditor;

namespace CardinalSystem.Cardinal.Editor.EditorExtensions.Shortcuts.EditorShortcuts
{
    public class CustomShortcuts : AssetModificationProcessor
    {
        private static string _pathToMyTemplateScript =
            "/Editor/EditorWindow/Shortcuts/ScriptTemplates/Template.txt";

        [MenuItem("CARDINAL/EditorExtension/Create folder %F")]
        public static void CreateFolder()
        {
            ProjectWindowUtil.CreateFolder();
        }
        
        [MenuItem("CARDINAL/EditorExtension/Create script %G")]
        public static void CreateScript()
        {
            var a = Directory.GetDirectories("Library/PackageCache");

            foreach (var name in a)
            {
                if (name.Contains("cardinal"))
                {
                    _pathToMyTemplateScript = name.Replace('\\', '/') + _pathToMyTemplateScript;
                    
                }
            }
            
            ProjectWindowUtil.CreateScriptAssetFromTemplateFile( _pathToMyTemplateScript, "Behaviour.cs");
        }
        
        private static void OnWillCreateAsset(string path)
        {
            path = path.Replace(".meta", "");
            
            var index = path.LastIndexOf(".", StringComparison.Ordinal);
            
            if(index < 0) return;
            
            var file = path.Substring(index);
            
            if(file != ".cs" && file != ".js" && file != ".boo") return;
            
            file = File.ReadAllText(path);

            var lastPart = path.Substring(path.IndexOf("/", StringComparison.Ordinal));
            
            var nameSpace = lastPart.Substring(0, lastPart.LastIndexOf('/'));
            nameSpace = nameSpace.Replace('/', '.');
            nameSpace = nameSpace.TrimStart('.');
            nameSpace = nameSpace.Replace(" ", "");
            
            file = file.Replace("#NAMESPACE#", nameSpace);

            File.WriteAllText(path, file);
            AssetDatabase.Refresh();
        }
    }
}
