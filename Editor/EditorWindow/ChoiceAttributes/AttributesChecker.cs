using UnityEditor;
using UnityEngine.UIElements;

namespace CardinalSystem.Cardinal.Editor.Editor_Window.ChoiceAttributes
{
    public class AttributesChecker : EditorWindow
    {
        [MenuItem("CARDINAL/Windows/Attributes Checker")]
        public static void ShowWindow()
        {
            var window = GetWindow(typeof(AttributesChecker), false, "Attributes Checker");
            window.Show();
        }

        private void OnEnable()
        {
            var csharpField = new Toggle("C# Field")
            {
                value = false
            };
            csharpField.SetEnabled(true);
            csharpField.AddToClassList("some-styled-field");
            rootVisualElement.Add(csharpField);
        }

        private void CheckAttributes()
        {
            //Здесь проходимся по Assembly один раз. Собираем все скрипты, которые проверяют аттрибуты и пускаем их по кругу с проверкой чекбоксов
        }
    }
}
