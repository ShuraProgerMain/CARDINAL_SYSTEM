using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using UnityEditor;
using Debug = UnityEngine.Debug;

namespace Cardinal.Editor.CARDINAL_EDITOR.Shortcuts.EditorShortcuts
{
    public class CustomShortcuts : UnityEditor.AssetModificationProcessor
    {
        public const string PATH_TO_MY_TEMPLATE_SCRIPT =
            "Packages/CARDINAL/Editor/CARDINAL_EDITOR/Shortcuts/ScriptTemplates/Template.txt";
        

        [MenuItem("CARDINAL/EditorExtension/C %#F")]
        public static void Test()
        {
            Debug.Log(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
        }
        
        [MenuItem("CARDINAL/EditorExtension/Create folder %F")]
        public static void CreateFolder()
        {
            ProjectWindowUtil.CreateFolder();
        }
        
        [MenuItem("CARDINAL/EditorExtension/Create script %G")]
        public static void CreateScript()
        {
            ProjectWindowUtil.CreateScriptAssetFromTemplateFile( PATH_TO_MY_TEMPLATE_SCRIPT, "Behaviour.cs");
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
