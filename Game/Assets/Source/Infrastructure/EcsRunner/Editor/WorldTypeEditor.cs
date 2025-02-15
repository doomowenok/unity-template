using System;
using UnityEditor;
using UnityEngine;

namespace Infrastructure.EcsRunner.Editor
{
    public sealed class WorldTypeEditor : EditorWindow
    {
        private const string WorldTypePath = "Assets/Source/Infrastructure/EcsRunner/WorldType.cs";
        private string _worldType = "";

        [MenuItem("World/New World Type")]
        public static void ShowWindow()
        {
            GetWindow<WorldTypeEditor>("World Editor");
        }

        void OnGUI()
        {
            EditorGUILayout.LabelField("Create new world type: ");
            _worldType = EditorGUILayout.TextField("Type:", _worldType);

            if (GUILayout.Button("Create"))
            {
                CreateWorldType();
            }
        }

        void CreateWorldType()
        {
            int id = Enum.GetValues(typeof(WorldType)).Length;
            int previousId = id - 1;
            string fileContent = System.IO.File.ReadAllText(WorldTypePath);
            string worldType = $"{_worldType} = {id},";
            fileContent = fileContent.Replace($"{previousId},", $"{previousId}," + "\n\t\t" + worldType);
            System.IO.File.WriteAllText(WorldTypePath, fileContent);
            AssetDatabase.Refresh();
        }
    }
}