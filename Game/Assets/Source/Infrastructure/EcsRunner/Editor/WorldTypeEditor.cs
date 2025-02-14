using System;
using UnityEditor;
using UnityEngine;

namespace Infrastructure.EcsRunner.Editor
{
    public sealed class WorldTypeEditor : EditorWindow
    {
        [MenuItem("World/New World Type")]
        public static void ShowWindow()
        {
            GetWindow<WorldTypeEditor>("World Editor");
        }

        private string _worldType = "";

        void OnGUI()
        {
            EditorGUILayout.LabelField("Добавить новый тип мира: ");
            _worldType = EditorGUILayout.TextField("Новый тип:", _worldType);

            if (GUILayout.Button("Добавить"))
            {
                AddEnumValue();
            }
        }

        void AddEnumValue()
        {
            Type enumType = typeof(WorldType);
            string[] enumValues = Enum.GetNames(enumType);
            int newId = enumValues.Length;
            AddEnumValue(enumType, _worldType, newId);
        }

        //TODO::Fix parsing.
        void AddEnumValue(Type enumType, string value, int id)
        {
            string filePath = "Assets/Source/Infrastructure/EcsRunner/WorldType.cs";
            string fileContent = System.IO.File.ReadAllText(filePath);

            // Добавляем новый тип в enum
            string worldType = $"{value} = {id},";
            fileContent = fileContent.Replace("}", worldType + Environment.NewLine + "}");

            // Записываем изменения в файл
            System.IO.File.WriteAllText(filePath, fileContent);

            // Обновляем ассеты в Unity
            AssetDatabase.Refresh();
        }
    }
}