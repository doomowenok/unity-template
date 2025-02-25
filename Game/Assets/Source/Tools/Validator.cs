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
            foreach (Scene scene in GetAllProjectScenes())
            {
                FindMissingComponents(scene);
            }
        }

        private static void FindMissingComponents(Scene scene)
        {
            foreach (var gameObject in GetAllGameObjects(scene))
            {
                FindMissingComponent(gameObject, scene);
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

        private static IEnumerable<Scene> GetAllProjectScenes()
        {
            string[] scenePaths = AssetDatabase
                .FindAssets("t:Scene", new[] { "Assets" })
                .Select(AssetDatabase.GUIDToAssetPath)
                .ToArray();
            
            foreach (string sceneName in scenePaths)
            {
                var scene = SceneManager.GetSceneByPath(sceneName);
                
                if (scene.isLoaded)
                {
                    yield return scene;
                }
                else
                {
                    Scene openScene = EditorSceneManager.OpenScene(sceneName, OpenSceneMode.Additive);
                    yield return openScene;
                    EditorSceneManager.CloseScene(openScene, true);
                }
            }
        }

        private static IEnumerable<GameObject> GetAllGameObjects(Scene scene)
        {
            Queue<GameObject> gameObjectsQueue = new Queue<GameObject>(scene.GetRootGameObjects());

            while (gameObjectsQueue.Count > 0)
            {
                GameObject gameObject = gameObjectsQueue.Dequeue();
                
                yield return gameObject;

                foreach (Transform child in gameObject.transform)
                {
                    gameObjectsQueue.Enqueue(child.gameObject);
                }
            }
        }
    } 
}