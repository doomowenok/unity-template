using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Tools
{
    public static class Validator
    {
        private static ILogger Logger => Debug.unityLogger;
        
        [MenuItem("Tools/Scenes Validation/Missing Components")]
        public static void FindMissingComponents()
        {
            string[] scenePaths = AssetDatabase
                .FindAssets("t:Scene", new[] { "Assets" })
                .Select(AssetDatabase.GUIDToAssetPath)
                .ToArray();

            foreach (string sceneName in scenePaths)
            {
                if (SceneManager.GetSceneByPath(sceneName).isLoaded)
                {
                    continue;
                }
                
                Scene openScene = EditorSceneManager.OpenScene(sceneName, OpenSceneMode.Additive);
                FindMissingComponents(openScene);
                EditorSceneManager.CloseScene(openScene, true);
            }
        }

        private static void FindMissingComponents(Scene scene)
        {
            Queue<GameObject> gameObjectsQueue = new Queue<GameObject>(scene.GetRootGameObjects());

            while (gameObjectsQueue.Count > 0)
            {
                GameObject gameObject = gameObjectsQueue.Dequeue();
                FindMissingComponent(gameObject, scene);

                foreach (Transform child in gameObject.transform)
                {
                    gameObjectsQueue.Enqueue(child.gameObject);
                }
            }
        }

        private static void FindMissingComponent(GameObject gameObject, Scene scene)
        {
            bool hasMissingScript = GameObjectUtility.GetMonoBehavioursWithMissingScriptCount(gameObject) > 0;

            if (hasMissingScript)
            {
                Logger.LogWarning("💔", $"GameObject {gameObject.name} from scene {scene.name} has missing components.");     
            }
        }
    } 
}