using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
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
        [TestCase("Assets/Scenes/Boot.unity")]
        [TestCase("Assets/Scenes/Game.unity")]
        public void AllGameObjectsShouldNotHaveMissingScripts(string scenePath)
        {
            Scene scene = EditorSceneManager.OpenScene(scenePath, OpenSceneMode.Additive);
            
            IEnumerable<string> gameObjectsWithMissingScripts = 
                GetAllGameObjects(scene)
                .Where(HasMissingComponent)
                .Select(gameObject => gameObject.name)
                .ToList();
            
            EditorSceneManager.CloseScene(scene, true);

            gameObjectsWithMissingScripts.Should().BeEmpty();
        }

        private static IEnumerable<Scene> GetProjectScenes()
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
