using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Gameplay.Tests.EditMode
{
    public class ValidationTests
    {
        // A Test behaves as an ordinary method
        [Test]
        public void ValidationTestsSimplePasses()
        {
            IEnumerable<string> errors = 
                from scene in GetAllProjectScenes()
                from gameObject in GetAllGameObjects(scene)
                where HasMissingComponent(gameObject)
                select $"GameObject {gameObject.name} from scene {scene.name} has missing component(s).";

            Assert.That(errors, Is.Empty);
        }
        
        private static ILogger Logger => Debug.unityLogger;

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
        
        private static bool HasMissingComponent(GameObject gameObject) => 
            GameObjectUtility.GetMonoBehavioursWithMissingScriptCount(gameObject) > 0;
    }
}
