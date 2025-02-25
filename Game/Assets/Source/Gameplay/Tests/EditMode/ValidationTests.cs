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
            bool missingComponentDetected = false;
            
            // Use the Assert class to test conditions
            foreach (Scene scene in GetAllProjectScenes())
            {
                foreach (var gameObject in GetAllGameObjects(scene))
                {
                    if (HasMissingComponent(gameObject))
                    {
                        missingComponentDetected = true;
                        Logger.LogError("💔", $"GameObject {gameObject.name} from scene {scene.name} has missing components.");     
                    }
                }
            }
            
            Assert.That(missingComponentDetected, Is.False);
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
